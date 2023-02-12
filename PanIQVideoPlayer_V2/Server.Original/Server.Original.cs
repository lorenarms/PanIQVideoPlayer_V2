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
using System.Globalization;


namespace Server.Original
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private SimpleTcpServer _server;

        // dictionary for client ip addresses [keys] and names [value]
        private Dictionary<string, string> _table;

       

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            textServerIp.Text = GetLocalIpAddress() + @":9001";

            // initialize the dictionary upon load
            _table = new Dictionary<string, string>();
        }




        // server events
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                listMessages.Text += $@"{e.IpPort} connected.{Environment.NewLine}";

                // ask for the name of the computer that just connected
                // send "REQUESTNAME+" header with the socket of the client computer

                _server.Send(e.IpPort, "REQUESTNAME+");
                
            });
        }
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            var ipAddressWithPort = e.IpPort;
            var computerToRemove = string.Empty;
            this.Invoke((MethodInvoker)delegate
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

            // convert incoming data to string
            // check "header" for keywords
            // proper format is [HEADER],[ipAddress]+[MESSAGE]

            string messageReceived = Encoding.UTF8.GetString(e.Data.ToArray());

            if (messageReceived.Contains("NAMEREQUEST"))
            {
                char[] splitter = { '+' };
                string[] messageSplit = messageReceived.Split(splitter, StringSplitOptions.RemoveEmptyEntries);

                // NAMEREQUEST+COMPUTERNAME

                foreach (var item in messageSplit)
                {
                    if (item.Equals("NAMEREQUEST"))
                    {
                        continue;
                    }
                    var clientComputerName = item;

                    // COMPUTERNAME

                    _table.Add(e.IpPort, clientComputerName);

                    this.Invoke((MethodInvoker)delegate
                    {
                        listClient.Items.Add(clientComputerName);
                    });


                }
            }

            else
            {
                this.Invoke((MethodInvoker)delegate
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
                    // loop through table of connected devices
                    // look for a name match
                    // use the corresponding key as the ip address to send message
                    foreach (var item in _table)
                    {
                        if (item.Value == listClient.SelectedItem.ToString())
                        {
                            ipConnection = item.Key;
                        }
                    }

                    _server.Send(ipConnection, textMessage.Text);
                    
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


