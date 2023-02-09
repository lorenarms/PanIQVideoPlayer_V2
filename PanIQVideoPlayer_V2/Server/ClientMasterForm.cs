
// https://www.youtube.com/watch?v=QrdfegS3iDg

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;

namespace ClientMaster
{
    public partial class ClientMasterForm : Form
    {

        private SimpleTcpClient _client;
        private Dictionary<string, string> ClientSlaveList;
        
        private static string _localComputerName;

        public ClientMasterForm()
        {
            InitializeComponent();
        }


        private void ClientMasterForm_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            btnCommand.Enabled = false;
            textServerIp.Text = GetLocalIpAddress() + @":9001";
            _localComputerName = GetLocalComputerName();
            ClientSlaveList = new Dictionary<string, string>();
        }

        


        // master events
        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort} connected.{Environment.NewLine}";
                //_client.Send("REQUESTNAMEMASTER+" + e.IpPort + "," + GetLocalComputerName());
                
                
            });
        }
        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            var ipAddressWithPort = e.IpPort;
            var computerToRemove = string.Empty;
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort} disconnected.{Environment.NewLine}";
                foreach (var item in ClientSlaveList)
                {
                    if (item.Key.Equals(ipAddressWithPort))
                    {
                        computerToRemove = item.Value;
                    }
                }
                listClient.Items.Remove(computerToRemove);
                ClientSlaveList.Remove(ipAddressWithPort);
            });
        }


        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            string messageReceived = Encoding.UTF8.GetString(e.Data.ToArray());
            

            if (messageReceived.Contains("REQUESTNAME+"))
            {
                string clientIpAddressWithPort = "REQUESTNAMEMASTER+";
                string[] messageSplit = messageReceived.Split('+');
                foreach (var item in messageSplit)
                {
                    if (item.Equals("REQUESTNAME"))
                    {
                        continue;
                    }

                    clientIpAddressWithPort += item;

                }
                clientIpAddressWithPort += ",";
                clientIpAddressWithPort += GetLocalComputerName();
                clientIpAddressWithPort.Trim();

                _client.Send(clientIpAddressWithPort);
            }

            else if (messageReceived.Contains("SLAVE+"))
            {
                ClientSlaveList.Clear();
                char[] splitterMessage = {'+'};
                string[] messageSplit = messageReceived.Split(splitterMessage, StringSplitOptions.RemoveEmptyEntries);
                foreach (var VARIABLE in messageSplit)
                {
                    if (VARIABLE.Equals("SLAVE"))
                    {
                        continue;
                    }

                    char[] splitterEntry = {','};
                    string[] messageEntries = VARIABLE.Split(splitterEntry, StringSplitOptions.RemoveEmptyEntries);
                    ClientSlaveList.Add(messageEntries[0], messageEntries[1]);

                    this.Invoke((MethodInvoker) delegate
                    {
                        listClient.Items.Add(messageEntries[1]);

                    });


                }
            }

            else if (messageReceived.Contains("REFRESHLIST"))
            {
                ClientSlaveList.Clear();
                this.Invoke((MethodInvoker) delegate
                {
                    listClient.Items.Clear();

                });
                _client.Send("REQUESTSLAVELIST+" + GetLocalIpAddress());
            }
            else
            {
                this.Invoke((MethodInvoker) delegate
                {
                    listMessages.Text += $@"{e.IpPort}: {Encoding.UTF8.GetString(e.Data.ToArray())}{Environment.NewLine}";
                });

            }

        }





        // buttons
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_client.IsConnected && listClient.SelectedItem == null)
            {
                // send message to server

                if (!string.IsNullOrEmpty(textMessage.Text))
                {
                    _client.Send(textMessage.Text);
                    listMessages.Text += $@"Me: {textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = string.Empty;
                }
            }
            else if (_client.IsConnected && listClient.SelectedItem != null)
            {
                // send message to selected client
                string ipConnection = string.Empty;
                foreach (var item in ClientSlaveList)
                {
                    if (item.Value == listClient.SelectedItem.ToString())
                    {
                        ipConnection = item.Key;
                    }
                }

                // append header "COMMAND" to message
                _client.Send("COMMAND+" + ipConnection + "," + textMessage.Text);
                listMessages.Text += $@"Me: {textMessage.Text}{Environment.NewLine}";
                textMessage.Text = string.Empty;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _client = new SimpleTcpClient(textServerIp.Text);
                _client.Events.Connected += Events_Connected;
                _client.Events.Disconnected += Events_Disconnected;
                _client.Events.DataReceived += Events_DataReceived;
                _client.Connect();
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
                btnCommand.Enabled = true;



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, @"Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listMessages.Text += $@"Could not connect... retrying now.{Environment.NewLine}";
            }
            
            
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            ClientSlaveList.Clear();
            this.Invoke((MethodInvoker)delegate
            {
                listClient.Items.Clear();

            });

            _client.Send("REQUESTSLAVELIST+" + GetLocalIpAddress());

        }



        // miscellaneous methods
        public string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var name = Dns.GetHostName();
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public string GetLocalComputerName()    // return the name of the local computer
        {
            var name = Dns.GetHostName();
            return Dns.GetHostName();
        }


    }
}
