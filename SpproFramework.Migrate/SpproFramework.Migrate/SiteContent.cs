using Microsoft.SharePoint.Client;
using SpproFramework.Migrate.Model;
using SpproFramework.Utilities;
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
    public partial class SiteContent : System.Windows.Forms.Form
    {
        #region Private Members

        private List<Field> Fields { get; set; }

        #endregion

        #region Private Methods

        private static int SelectByValue(ComboBox comboBox, string value)
        {
            int i = 0;
            for (i = 0; i <= comboBox.Items.Count - 1; i++)
            {
                ComboboxItem cb = (ComboboxItem)comboBox.Items[i];
                if (cb.ToString() == value)// Change the 0 index if your want to Select by Text as 1 Index
                {
                    return i;
                }
            }
            return -1;
        }

        private void loadContentTypes()
        {
            var contentList = SpContext.ClientContext.Web.ContentTypes;
            SpContext.ClientContext.Load(contentList);
            SpContext.ClientContext.ExecuteQuery();
            foreach (var contentType in contentList)
            {
                comboParentContent.Items.Add(new ComboboxItem() { Text = contentType.Name, Value = contentType });
            }

            comboParentContent.SelectedIndex = SelectByValue(comboParentContent, "Item");
        }

        #endregion

        #region Constructors

        public SiteContent(List<Field> fields)
        {
            InitializeComponent();
            loadContentTypes();
            this.Fields = fields;
        }

        #endregion

        #region Events
        private void btnCreateContentType_Click(object sender, EventArgs e)
        {
            ContentTypeUtility.CreateContentType(SpContext.ClientContext,
                                                    (ContentType)comboParentContent.SelectedValue,
                                                    textContentType.Text,
                                                    textDescription.Text,
                                                    textGroup.Text,
                                                    Fields);
        }

        #endregion

    }
}
