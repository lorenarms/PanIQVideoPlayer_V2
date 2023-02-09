namespace ClientMaster
{
    partial class ClientMasterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listMessages = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.textServerIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listClient = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCommand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listMessages
            // 
            this.listMessages.Location = new System.Drawing.Point(84, 50);
            this.listMessages.Margin = new System.Windows.Forms.Padding(2);
            this.listMessages.Multiline = true;
            this.listMessages.Name = "listMessages";
            this.listMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.listMessages.Size = new System.Drawing.Size(411, 228);
            this.listMessages.TabIndex = 13;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(325, 317);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 42);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(413, 317);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 42);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(84, 288);
            this.textMessage.Margin = new System.Windows.Forms.Padding(2);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(411, 20);
            this.textMessage.TabIndex = 10;
            // 
            // textServerIp
            // 
            this.textServerIp.Location = new System.Drawing.Point(84, 22);
            this.textServerIp.Margin = new System.Windows.Forms.Padding(2);
            this.textServerIp.Name = "textServerIp";
            this.textServerIp.Size = new System.Drawing.Size(411, 20);
            this.textServerIp.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 291);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Message:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server:";
            // 
            // listClient
            // 
            this.listClient.FormattingEnabled = true;
            this.listClient.Location = new System.Drawing.Point(504, 77);
            this.listClient.Margin = new System.Windows.Forms.Padding(2);
            this.listClient.Name = "listClient";
            this.listClient.Size = new System.Drawing.Size(118, 277);
            this.listClient.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(501, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Client:";
            // 
            // btnCommand
            // 
            this.btnCommand.Location = new System.Drawing.Point(504, 44);
            this.btnCommand.Margin = new System.Windows.Forms.Padding(2);
            this.btnCommand.Name = "btnCommand";
            this.btnCommand.Size = new System.Drawing.Size(116, 24);
            this.btnCommand.TabIndex = 16;
            this.btnCommand.Text = "Refresh";
            this.btnCommand.UseVisualStyleBackColor = true;
            this.btnCommand.Click += new System.EventHandler(this.btnCommand_Click);
            // 
            // ClientMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 394);
            this.Controls.Add(this.btnCommand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listClient);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.textServerIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(658, 433);
            this.MinimumSize = new System.Drawing.Size(658, 433);
            this.Name = "ClientMasterForm";
            this.Text = "Client Master";
            this.Load += new System.EventHandler(this.ClientMasterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox listMessages;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.TextBox textServerIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listClient;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCommand;
    }
}

