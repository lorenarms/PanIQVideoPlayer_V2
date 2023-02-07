


// https://www.youtube.com/watch?v=QrdfegS3iDg



using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;

namespace Server
{
    public partial class ClientMasterForm : Form
    {

        public ClientMasterForm()
        {
            InitializeComponent();
        }

        private SimpleTcpServer _server;
        private Dictionary<string, string> _table;

        private void ServerForm_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            textServerIp.Text = GetLocalIpAddress() + @":9001";
            _table = new Dictionary<string, string>();
        }

        


        // server events
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort} connected.{Environment.NewLine}";
                _server.Send(e.IpPort, "REQUESTNAME+" + e.IpPort);
                //listClient.Items.Add(e.IpPort);
            });
        }
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            var ipAddressWithPort = e.IpPort;
            var computerToRemove = string.Empty;
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort} disconnected.{Environment.NewLine}";
                foreach (var item in _table)
                {
                    if (item.Key.Equals(ipAddressWithPort))
                    {
                        computerToRemove = item.Value;
                    }
                }
                listClient.Items.Remove(computerToRemove);
                _table.Remove(ipAddressWithPort);
            });
        }
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (Encoding.UTF8.GetString(e.Data.ToArray()).Contains("NAMEREQUEST"))
            {

                this.Invoke((MethodInvoker) delegate
                {
                    string table = Encoding.UTF8.GetString(e.Data.ToArray());
                    char[] splitter = {','};
                    string[] entries = table.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var entry in entries)
                    {
                        if (entry.Equals("NAMEREQUEST"))
                        {
                            continue;
                        }

                        char[] singleEntryChars = {'+'};
                        string[] singleEntry = entry.Split(singleEntryChars, StringSplitOptions.None);

                        _table.Add(singleEntry[0], singleEntry[1]);

                        listClient.Items.Add(singleEntry[1]);
                        
                    }

                });

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
        private void btnStart_Click(object sender, EventArgs e)
        {
            _server = new SimpleTcpServer(textServerIp.Text);
            _server.Events.ClientDisconnected += Events_ClientDisconnected;
            _server.Events.ClientConnected += Events_ClientConnected;
            _server.Events.DataReceived += Events_DataReceived;
            _server.Start();
            listMessages.Text += $@"Starting...{Environment.NewLine}";
            btnStart.Enabled = false;
            btnSend.Enabled = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string ipConnection = string.Empty;

            if (_server.IsListening)
            {
                if (!string.IsNullOrEmpty(textMessage.Text) && listClient.SelectedItem != null)
                {
                    foreach (var item in _table)
                    {
                        if (item.Value == listClient.SelectedItem.ToString())
                        {
                            ipConnection = item.Key;
                        }
                    }

                    _server.Send(ipConnection, textMessage.Text);
                    //_server.Send( listClient.SelectedItem.ToString(), textMessage.Text);

                    listMessages.Text += $@"Server: {textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = string.Empty;
                }
            }
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
    }
}
