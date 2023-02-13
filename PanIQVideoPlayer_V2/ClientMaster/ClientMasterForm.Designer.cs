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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientMasterForm));
            this.listMessages = new System.Windows.Forms.TextBox();
            this.textServerIp = new System.Windows.Forms.TextBox();
            this.listRoom = new System.Windows.Forms.ListBox();
            this.labelInformation = new System.Windows.Forms.Label();
            this.roomListLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new Server.RoundedButton();
            this.btnConnect = new Server.RoundedButton();
            this.btnStop = new Server.RoundedButton();
            this.btnPlay = new Server.RoundedButton();
            this.SuspendLayout();
            // 
            // listMessages
            // 
            this.listMessages.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.listMessages.Location = new System.Drawing.Point(334, 29);
            this.listMessages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listMessages.Multiline = true;
            this.listMessages.Name = "listMessages";
            this.listMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.listMessages.Size = new System.Drawing.Size(132, 221);
            this.listMessages.TabIndex = 13;
            // 
            // textServerIp
            // 
            this.textServerIp.Location = new System.Drawing.Point(141, 198);
            this.textServerIp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textServerIp.Name = "textServerIp";
            this.textServerIp.Size = new System.Drawing.Size(160, 20);
            this.textServerIp.TabIndex = 9;
            this.textServerIp.Text = "Not connected";
            // 
            // listRoom
            // 
            this.listRoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listRoom.FormattingEnabled = true;
            this.listRoom.ItemHeight = 16;
            this.listRoom.Location = new System.Drawing.Point(150, 29);
            this.listRoom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listRoom.Name = "listRoom";
            this.listRoom.Size = new System.Drawing.Size(146, 144);
            this.listRoom.TabIndex = 14;
            // 
            // labelInformation
            // 
            this.labelInformation.AutoSize = true;
            this.labelInformation.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInformation.Location = new System.Drawing.Point(138, 220);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(163, 16);
            this.labelInformation.TabIndex = 23;
            this.labelInformation.Text = "Click \"connect\" to start...";
            // 
            // roomListLabel
            // 
            this.roomListLabel.AutoSize = true;
            this.roomListLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomListLabel.Location = new System.Drawing.Point(138, 11);
            this.roomListLabel.Name = "roomListLabel";
            this.roomListLabel.Size = new System.Drawing.Size(112, 16);
            this.roomListLabel.TabIndex = 24;
            this.roomListLabel.Text = "Room Selection:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Log";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRefresh.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.btnRefresh.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnRefresh.BorderRadius = 40;
            this.btnRefresh.BorderSize = 0;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(11, 12);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(122, 47);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.TextColor = System.Drawing.Color.White;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnConnect.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnConnect.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConnect.BorderRadius = 40;
            this.btnConnect.BorderSize = 0;
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(11, 204);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(122, 47);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextColor = System.Drawing.Color.White;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.PaleVioletRed;
            this.btnStop.BackgroundColor = System.Drawing.Color.PaleVioletRed;
            this.btnStop.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnStop.BorderRadius = 40;
            this.btnStop.BorderSize = 0;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(11, 114);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(122, 47);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = " Stop";
            this.btnStop.TextColor = System.Drawing.Color.White;
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPlay.BackgroundColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPlay.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnPlay.BorderRadius = 40;
            this.btnPlay.BorderSize = 0;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.Location = new System.Drawing.Point(11, 63);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(122, 47);
            this.btnPlay.TabIndex = 19;
            this.btnPlay.Text = "Play";
            this.btnPlay.TextColor = System.Drawing.Color.White;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // ClientMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(477, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.roomListLabel);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.listRoom);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.textServerIp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(493, 300);
            this.MinimumSize = new System.Drawing.Size(493, 300);
            this.Name = "ClientMasterForm";
            this.Text = "Video Player";
            this.Load += new System.EventHandler(this.ClientMasterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox listMessages;
        private System.Windows.Forms.TextBox textServerIp;
        private System.Windows.Forms.ListBox listRoom;
        private Server.RoundedButton btnPlay;
        private Server.RoundedButton btnStop;
        private Server.RoundedButton btnConnect;
        private Server.RoundedButton btnRefresh;
        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.Label roomListLabel;
        private System.Windows.Forms.Label label1;
    }
}

