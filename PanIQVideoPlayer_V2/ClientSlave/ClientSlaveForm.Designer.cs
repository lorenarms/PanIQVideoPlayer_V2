namespace Client
{
    partial class ClientSlaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSlaveForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textServerIp = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listMessages = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // textServerIp
            // 
            this.textServerIp.Location = new System.Drawing.Point(66, 12);
            this.textServerIp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textServerIp.Name = "textServerIp";
            this.textServerIp.Size = new System.Drawing.Size(228, 20);
            this.textServerIp.TabIndex = 2;
            this.textServerIp.Text = "192.168.0.34:9001";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(66, 276);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 42);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listMessages
            // 
            this.listMessages.Location = new System.Drawing.Point(66, 40);
            this.listMessages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listMessages.Multiline = true;
            this.listMessages.Name = "listMessages";
            this.listMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.listMessages.Size = new System.Drawing.Size(228, 228);
            this.listMessages.TabIndex = 6;
            // 
            // ClientSlaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 344);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textServerIp);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(328, 383);
            this.MinimumSize = new System.Drawing.Size(328, 383);
            this.Name = "ClientSlaveForm";
            this.Text = "Kiosk Client";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textServerIp;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox listMessages;
    }
}

