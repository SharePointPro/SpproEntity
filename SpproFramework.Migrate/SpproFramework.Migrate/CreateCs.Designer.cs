namespace SpproFramework.Migrate
{
    partial class CreateCs
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
            this.textNamespace = new System.Windows.Forms.TextBox();
            this.textProjectFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProjectFolder = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.textContextName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Namespace";
            // 
            // textNamespace
            // 
            this.textNamespace.Location = new System.Drawing.Point(41, 66);
            this.textNamespace.Name = "textNamespace";
            this.textNamespace.Size = new System.Drawing.Size(805, 38);
            this.textNamespace.TabIndex = 1;
            this.textNamespace.TextChanged += new System.EventHandler(this.textNamespace_TextChanged);
            // 
            // textProjectFolder
            // 
            this.textProjectFolder.Location = new System.Drawing.Point(41, 252);
            this.textProjectFolder.Name = "textProjectFolder";
            this.textProjectFolder.Size = new System.Drawing.Size(805, 38);
            this.textProjectFolder.TabIndex = 3;
            this.textProjectFolder.TextChanged += new System.EventHandler(this.textProjectFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 217);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project Folder";
            // 
            // btnProjectFolder
            // 
            this.btnProjectFolder.Location = new System.Drawing.Point(41, 307);
            this.btnProjectFolder.Name = "btnProjectFolder";
            this.btnProjectFolder.Size = new System.Drawing.Size(222, 54);
            this.btnProjectFolder.TabIndex = 4;
            this.btnProjectFolder.Text = "Choose Folder";
            this.btnProjectFolder.UseVisualStyleBackColor = true;
            this.btnProjectFolder.Click += new System.EventHandler(this.btnProjectFolder_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(41, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(805, 94);
            this.button1.TabIndex = 5;
            this.button1.Text = "Create SharePoint Model";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textContextName
            // 
            this.textContextName.Location = new System.Drawing.Point(41, 158);
            this.textContextName.Name = "textContextName";
            this.textContextName.Size = new System.Drawing.Size(805, 38);
            this.textContextName.TabIndex = 2;
            this.textContextName.TextChanged += new System.EventHandler(this.textContextName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 123);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(273, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Context Class Name";
            // 
            // CreateCs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 518);
            this.Controls.Add(this.textContextName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnProjectFolder);
            this.Controls.Add(this.textProjectFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textNamespace);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateCs";
            this.Text = "CreateCs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textNamespace;
        private System.Windows.Forms.TextBox textProjectFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnProjectFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textContextName;
        private System.Windows.Forms.Label label3;
    }
}