using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperSimpleTcp;

namespace Server
{
    public partial class ServerForm : Form
    {

        public ServerForm()
        {
            InitializeComponent();
        }

        private SimpleTcpServer server;

        private void ServerForm_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            server = new SimpleTcpServer(textServerIp.Text);
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.DataReceived += Events_DataReceived;
        }

        


        // server methods
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort} connected.{Environment.NewLine}";
                listClient.Items.Add(e.IpPort);
            });
        }
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort} disconnected.{Environment.NewLine}";
                listClient.Items.Remove(e.IpPort);
            });
        }
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                listMessages.Text += $@"{e.IpPort}: {Encoding.UTF8.GetString(e.Data.ToArray())}{Environment.NewLine}";
            });
        }


        // buttons
        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
            listMessages.Text += $@"Starting...{Environment.NewLine}";
            btnStart.Enabled = false;
            btnSend.Enabled = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                if (!string.IsNullOrEmpty(textMessage.Text) && listClient.SelectedItem != null)
                {
                    server.Send(listClient.SelectedItem.ToString(), textMessage.Text);
                    listMessages.Text += $@"Server: {textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = string.Empty;
                }
            }
        }
    }
}
