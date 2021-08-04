namespace locationserver
{
    partial class ServerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.StartServer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ReadTimeoutBox1 = new System.Windows.Forms.TextBox();
            this.WriteTimeoutBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(71, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location Server";
            // 
            // StartServer
            // 
            this.StartServer.BackColor = System.Drawing.Color.Tomato;
            this.StartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartServer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StartServer.Location = new System.Drawing.Point(132, 275);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(234, 61);
            this.StartServer.TabIndex = 1;
            this.StartServer.Text = "Start Server";
            this.StartServer.UseVisualStyleBackColor = false;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(117, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 80);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(227, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 80);
            this.label3.TabIndex = 4;
            this.label3.Text = "OFFLINE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(108, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Read Timout:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(110, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Write Timout:";
            // 
            // ReadTimeoutBox1
            // 
            this.ReadTimeoutBox1.Location = new System.Drawing.Point(242, 134);
            this.ReadTimeoutBox1.Name = "ReadTimeoutBox1";
            this.ReadTimeoutBox1.Size = new System.Drawing.Size(145, 20);
            this.ReadTimeoutBox1.TabIndex = 7;
            this.ReadTimeoutBox1.TextChanged += new System.EventHandler(this.ReadTimeoutBox1_TextChanged);
            // 
            // WriteTimeoutBox2
            // 
            this.WriteTimeoutBox2.Location = new System.Drawing.Point(242, 188);
            this.WriteTimeoutBox2.Name = "WriteTimeoutBox2";
            this.WriteTimeoutBox2.Size = new System.Drawing.Size(145, 20);
            this.WriteTimeoutBox2.TabIndex = 8;
            this.WriteTimeoutBox2.TextChanged += new System.EventHandler(this.WriteTimeoutBox2_TextChanged);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::locationserver.Properties.Resources.space;
            this.ClientSize = new System.Drawing.Size(487, 450);
            this.Controls.Add(this.WriteTimeoutBox2);
            this.Controls.Add(this.ReadTimeoutBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StartServer);
            this.Controls.Add(this.label1);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ReadTimeoutBox1;
        private System.Windows.Forms.TextBox WriteTimeoutBox2;
    }
}