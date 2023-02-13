
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
            btnRefresh.Enabled = false;
            btnPlay.Enabled = false;
            btnStop.Enabled = false;

            btnPlay.Visible = false;
            btnStop.Visible = false;
            btnRefresh.Visible = false;
            //textServerIp.Text = GetLocalIpAddress() + @":9001";
            _localComputerName = GetLocalComputerName();
            ClientSlaveList = new Dictionary<string, string>();
        }

        


        // master events
        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"Server connected.{Environment.NewLine}";
                //_client.Send("REQUESTNAMEMASTER+" + e.IpPort + "," + GetLocalComputerName());
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();


                textServerIp.Text = string.Empty;
                textServerIp.Text = "Connected!";



            });
        }
        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            var ipAddressWithPort = e.IpPort;
            var computerToRemove = string.Empty;
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"Server disconnected.{Environment.NewLine}";
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();
                foreach (var item in ClientSlaveList)
                {
                    if (item.Key.Equals(ipAddressWithPort))
                    {
                        computerToRemove = item.Value;
                    }
                }
                listRoom.Items.Remove(computerToRemove);
                ClientSlaveList.Remove(ipAddressWithPort);

                textServerIp.Text = "Disconnected";
                btnPlay.Enabled = false;
                btnStop.Enabled = false;
                btnRefresh.Enabled = false;
                btnConnect.Enabled = true;

                btnPlay.Visible = false;
                btnStop.Visible = false;
                btnRefresh.Visible = false;
                btnConnect.Visible = true;
            });

            
        }


        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            string messageReceived = Encoding.UTF8.GetString(e.Data.ToArray());
            

            if (messageReceived.Contains("REQUESTNAME+"))
            {
                _client.Send("REQUESTNAMEMASTER+" + GetLocalComputerName());
            }

            else if (messageReceived.Contains("SLAVE+"))
            {
                // adding a slave client to the list of clients connected
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
                        listRoom.Items.Add(messageEntries[1]);

                    });


                }
            }

            else if (messageReceived.Contains("REFRESHLIST"))
            {
                ClientSlaveList.Clear();
                this.Invoke((MethodInvoker) delegate
                {
                    listRoom.Items.Clear();

                });
                _client.Send("REQUESTSLAVELIST+");
            }

            else if (messageReceived.Equals(""))
            {
                // when server disconnects, it sends an empty string
                // don't do anything when this happens
            }

            else
            {
                this.Invoke((MethodInvoker) delegate
                {
                    listMessages.Text += $@"Server: {Encoding.UTF8.GetString(e.Data.ToArray())}{Environment.NewLine}";
                    listMessages.SelectionStart = listMessages.Text.Length;
                    listMessages.ScrollToCaret();
                });

            }

        }



        // buttons


        // ********* PLAY VIDEO *********
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_client.IsConnected && listRoom.SelectedItem == null)
            {
                // send message to server
                listMessages.Text += $@"Select a room first{Environment.NewLine}";
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();
                
            }
            else if (_client.IsConnected && listRoom.SelectedItem != null)
            {
                // send message to selected client
                string ipConnection = string.Empty;
                foreach (var item in ClientSlaveList)
                {
                    if (item.Value == listRoom.SelectedItem.ToString())
                    {
                        ipConnection = item.Key;
                    }
                }

                // append header "COMMAND" to message
                _client.Send("COMMAND+" + ipConnection + "," + "PLAY VIDEO COMMAND");
                listMessages.Text += $@"Me: Playing video on {listRoom.SelectedItem}{Environment.NewLine}";
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();
            }
        }


        // ********* STOP VIDEO *********
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_client.IsConnected && listRoom.SelectedItem == null)
            {
                // send message to server
                listMessages.Text += $@"Select a room first{Environment.NewLine}";
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();

            }
            else if (_client.IsConnected && listRoom.SelectedItem != null)
            {
                // send message to selected client
                string ipConnection = string.Empty;
                foreach (var item in ClientSlaveList)
                {
                    if (item.Value == listRoom.SelectedItem.ToString())
                    {
                        ipConnection = item.Key;
                    }
                }

                // append header "COMMAND" to message
                _client.Send("COMMAND+" + ipConnection + "," + "STOP VIDEO COMMAND");
                listMessages.Text += $@"Me: Stopping video on {listRoom.SelectedItem}{Environment.NewLine}";
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();
            }
        }



        // ********* CONNECT TO SERVER *********
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                textServerIp.Text = string.Empty;
                textServerIp.Text = "Connecting...";
                _client = new SimpleTcpClient(GetLocalIpAddress() + ":9001");
                _client.Events.Connected += Events_Connected;
                _client.Events.Disconnected += Events_Disconnected;
                _client.Events.DataReceived += Events_DataReceived;
                _client.Connect();
                btnConnect.Enabled = false;
                btnRefresh.Enabled = true;
                btnPlay.Enabled = true;
                btnStop.Enabled = true;

                btnPlay.Visible = true;
                btnStop.Visible = true;
                btnRefresh.Visible = true;
                btnConnect.Visible = false;

                _client.Send("REQUESTSLAVELIST+");



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, @"Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listMessages.Text += $@"Could not connect... Check server status.{Environment.NewLine}";
                listMessages.SelectionStart = listMessages.Text.Length;
                listMessages.ScrollToCaret();
                textServerIp.Text = "Disconnected";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClientSlaveList.Clear();
            this.Invoke((MethodInvoker)delegate
            {
                listRoom.Items.Clear();

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
