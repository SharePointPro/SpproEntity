using SpproFramework.Migrate.Builders;
using SpproFramework.Migrate.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpproFramework.Migrate
{
    internal partial class CreateCs : Form
    {
        #region Private Members

        private SelectedLists SelectedLists { get; set; }

        #endregion

        internal CreateCs(SelectedLists selectedLists)
        {
            InitializeComponent();
            this.SelectedLists = selectedLists;
            textNamespace.Text = SpContext.NamespaceString;
            textProjectFolder.Text = SpContext.ModelFolder;
            textContextName.Text = SpContext.ContextName;
        }

        private void btnProjectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textProjectFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;            
            var csBuilder = new CsBuilder();
            csBuilder.Create(this.SelectedLists, textNamespace.Text, textProjectFolder.Text, textContextName.Text);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Model's Created - You will now need to include them into your project");
            this.Close();
        }

        private void textNamespace_TextChanged(object sender, EventArgs e)
        {
            SpContext.NamespaceString = textNamespace.Text;
        }

        private void textContextName_TextChanged(object sender, EventArgs e)
        {
            SpContext.ContextName = textContextName.Text;
        }

        private void textProjectFolder_TextChanged(object sender, EventArgs e)
        {
            SpContext.ModelFolder = textProjectFolder.Text;
        }



    }
}
