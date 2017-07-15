namespace SpproFramework.Migrate
{
    partial class Connect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connect));
            this.labelUrl = new System.Windows.Forms.Label();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.textUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioSpOnline = new System.Windows.Forms.RadioButton();
            this.radioSpOnPrem = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUrl.Location = new System.Drawing.Point(4, 152);
            this.labelUrl.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(143, 24);
            this.labelUrl.TabIndex = 0;
            this.labelUrl.Text = "SharePoint URL";
            // 
            // pictureLogo
            // 
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.Location = new System.Drawing.Point(2, 4);
            this.pictureLogo.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.pictureLogo.MaximumSize = new System.Drawing.Size(474, 140);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(474, 140);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureLogo.TabIndex = 1;
            this.pictureLogo.TabStop = false;
            // 
            // textUrl
            // 
            this.textUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUrl.Location = new System.Drawing.Point(7, 180);
            this.textUrl.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textUrl.Name = "textUrl";
            this.textUrl.Size = new System.Drawing.Size(465, 26);
            this.textUrl.TabIndex = 2;
            this.textUrl.Text = "https://seymour100.sharepoint.com";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDomain);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textPassword);
            this.groupBox2.Controls.Add(this.textUsername);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 278);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox2.Size = new System.Drawing.Size(468, 151);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Specify credentials";
            // 
            // txtDomain
            // 
            this.txtDomain.Enabled = false;
            this.txtDomain.Location = new System.Drawing.Point(86, 107);
            this.txtDomain.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(379, 26);
            this.txtDomain.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Domain:";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(86, 66);
            this.textPassword.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(379, 26);
            this.textPassword.TabIndex = 3;
            // 
            // textUsername
            // 
            this.textUsername.Location = new System.Drawing.Point(86, 29);
            this.textUsername.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(379, 26);
            this.textUsername.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(346, 431);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(61, 33);
            this.btnConnect.TabIndex = 5;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(415, 431);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 33);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // radioSpOnline
            // 
            this.radioSpOnline.AutoSize = true;
            this.radioSpOnline.Checked = true;
            this.radioSpOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.1F);
            this.radioSpOnline.Location = new System.Drawing.Point(7, 214);
            this.radioSpOnline.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.radioSpOnline.Name = "radioSpOnline";
            this.radioSpOnline.Size = new System.Drawing.Size(162, 24);
            this.radioSpOnline.TabIndex = 7;
            this.radioSpOnline.TabStop = true;
            this.radioSpOnline.Text = "SharePoint Online";
            this.radioSpOnline.UseVisualStyleBackColor = true;
            this.radioSpOnline.CheckedChanged += new System.EventHandler(this.cbSpOnline_CheckedChanged);
            // 
            // radioSpOnPrem
            // 
            this.radioSpOnPrem.AutoSize = true;
            this.radioSpOnPrem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.1F);
            this.radioSpOnPrem.Location = new System.Drawing.Point(7, 244);
            this.radioSpOnPrem.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.radioSpOnPrem.Name = "radioSpOnPrem";
            this.radioSpOnPrem.Size = new System.Drawing.Size(203, 24);
            this.radioSpOnPrem.TabIndex = 8;
            this.radioSpOnPrem.Text = "SharePoint On Premsis";
            this.radioSpOnPrem.UseVisualStyleBackColor = true;
            // 
            // Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(476, 415);
            this.Controls.Add(this.radioSpOnPrem);
            this.Controls.Add(this.radioSpOnline);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textUrl);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.labelUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(505, 530);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(489, 434);
            this.Name = "Connect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect";
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.PictureBox pictureLogo;
        private System.Windows.Forms.TextBox textUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioSpOnline;
        private System.Windows.Forms.RadioButton radioSpOnPrem;
    }
}