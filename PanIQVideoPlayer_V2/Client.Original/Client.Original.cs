using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;
using DataReceivedEventArgs = SuperSimpleTcp.DataReceivedEventArgs;
using System.Globalization;

namespace Client.Original
{
    public partial class Form1 : Form
    {
        private SimpleTcpClient _client;
        private string _localComputerName;

        // for a separate, outiside project
        private Command_Runner _runner;
        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            _localComputerName = GetLocalComputerName();
            _runner = new Command_Runner();
            
        }

        
        // client methods
        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                listMessages.Text += $@"Server connected.{Environment.NewLine}";
            });
        }


        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                listMessages.Text += $@"Server disconnected.{Environment.NewLine}";
                btnSend.Enabled = false;
                btnConnect.Enabled = true;
            });
        }



        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            var messageReceived = Encoding.UTF8.GetString(e.Data.ToArray());


            // server is requesting the computer name
            // create a message to send that contains that name and proper 'header'
            // proper format is [HEADER],[ipAddress]+[NAME]

            if (messageReceived.Contains("REQUESTNAME"))
            {
                _client.Send("NAMEREQUEST+" + _localComputerName);
            }
            else if (messageReceived.Contains("START"))
            {
                _runner.StartIntroVideo();
            }
            else if (messageReceived.Contains("STOP"))
            {
                _runner.StopIntroVideo();
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    listMessages.Text +=
                        $@"{e.IpPort}: {messageReceived}{Environment.NewLine}";
                });
            }


            

        }


        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (_client.IsConnected)
            {
                if (!string.IsNullOrEmpty(textMessage.Text))
                {
                    _client.Send(textMessage.Text);
                    listMessages.Text += $@"Me: {textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = string.Empty;
                }
            }
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
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



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // miscellaneous methods
        public string GetLocalComputerName()    // return the name of the local computer
        {
            var name = Dns.GetHostName();
            return Dns.GetHostName();
        }

        public string GetLocalComputerIp()
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
