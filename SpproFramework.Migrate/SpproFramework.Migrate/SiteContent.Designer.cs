namespace SpproFramework.Migrate
{
    partial class SiteContent
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
            this.textContentType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textGroup = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboParentContent = new System.Windows.Forms.ComboBox();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateContentType = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textContentType
            // 
            this.textContentType.Location = new System.Drawing.Point(26, 161);
            this.textContentType.Margin = new System.Windows.Forms.Padding(4);
            this.textContentType.Name = "textContentType";
            this.textContentType.Size = new System.Drawing.Size(955, 44);
            this.textContentType.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 37);
            this.label3.TabIndex = 14;
            this.label3.Text = "Content Type Name";
            // 
            // textGroup
            // 
            this.textGroup.Location = new System.Drawing.Point(26, 273);
            this.textGroup.Margin = new System.Windows.Forms.Padding(4);
            this.textGroup.Name = "textGroup";
            this.textGroup.Size = new System.Drawing.Size(955, 44);
            this.textGroup.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 231);
            this.label2.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 37);
            this.label2.TabIndex = 10;
            this.label2.Text = "Group";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(311, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "Parent Content Type";
            // 
            // comboParentContent
            // 
            this.comboParentContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboParentContent.FormattingEnabled = true;
            this.comboParentContent.Location = new System.Drawing.Point(26, 49);
            this.comboParentContent.Name = "comboParentContent";
            this.comboParentContent.Size = new System.Drawing.Size(955, 45);
            this.comboParentContent.TabIndex = 16;
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(26, 377);
            this.textDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textDescription.Multiline = true;
            this.textDescription.Name = "textDescription";
            this.textDescription.Size = new System.Drawing.Size(955, 323);
            this.textDescription.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 335);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 37);
            this.label4.TabIndex = 17;
            this.label4.Text = "Description";
            // 
            // btnCreateContentType
            // 
            this.btnCreateContentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateContentType.Location = new System.Drawing.Point(25, 721);
            this.btnCreateContentType.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateContentType.Name = "btnCreateContentType";
            this.btnCreateContentType.Size = new System.Drawing.Size(956, 112);
            this.btnCreateContentType.TabIndex = 19;
            this.btnCreateContentType.Text = "Create Content Type";
            this.btnCreateContentType.UseVisualStyleBackColor = true;
            this.btnCreateContentType.Click += new System.EventHandler(this.btnCreateContentType_Click);
            // 
            // SiteContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1010, 863);
            this.Controls.Add(this.btnCreateContentType);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboParentContent);
            this.Controls.Add(this.textContentType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textGroup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiteContent";
            this.Text = "Create Content Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textContentType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboParentContent;
        private System.Windows.Forms.TextBox textDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateContentType;
    }
}