using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpproFramework.Extensions;
using Microsoft.SharePoint.Client;
using System.Net;

namespace SpproFramework.Migrate
{
    public partial class Connect : System.Windows.Forms.Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                SpContext.ClientContext = new Microsoft.SharePoint.Client.ClientContext(textUrl.Text);
                if (radioSpOnPrem.Checked)
                {
                    SpContext.ClientContext.Credentials = new NetworkCredential(textUsername.Text, textPassword.Text, txtDomain.Text);
                }
                else
                {
                    var password = textPassword.Text.ToSecureString();
                    SpContext.SpUrl = textUrl.Text;
                    SpContext.ClientContext.Credentials = new SharePointOnlineCredentials(textUsername.Text, password);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cbSpOnline_CheckedChanged(object sender, EventArgs e)
        {
            var checkedBox = (RadioButton)sender;
            if (!checkedBox.Checked)
            {
                txtDomain.Enabled = true;
            }
            else
            {
                txtDomain.Enabled = false;
            }

        }

    }
}
