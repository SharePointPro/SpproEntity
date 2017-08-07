using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using SpproFramework.Migrate.Model;
using SpproFramework.Migrate.Utilities;
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
    public partial class MainForm : System.Windows.Forms.Form
    {
        #region Private Members

        private List CurrentList { get; set; }

        private bool CheckListSpLists_SelectedIndexChanged_Disabled { get; set; }

        private bool CheckListSpFields_ItemCheck_Disabled { get; set; }

        #endregion

        #region Contructors
        public MainForm()
        {
            InitializeComponent();
            this.Width = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * 0.6);
            this.Height = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.7);
            ResizeCheckListbox();
            OpenConnect();
        }

        #endregion

        #region Private Methods

        private void ClearChecked()
        {
            foreach (var selectedField in SpContext.SelectedListItems)
            {
                foreach (var field in selectedField.Fields.Where(a => a.Selected))
                {
                    field.Selected = false;
                }
            }
            CheckListSpLists.ClearSelected();
        }

        private void OpenConnect()
        {
            Connect connect = new Connect();
            var result = connect.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                LoadLists();
            }
        }

        /// <summary>
        /// Load all Lists
        /// </summary>
        private void LoadLists()
        {
            try
            {
                CheckListSpLists.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                List<string> listList = new List<string>();
                var lists = SpContext.ClientContext.Web.Lists;
                SpContext.ClientContext.Load(lists);
                SpContext.ClientContext.ExecuteQuery();
                foreach (var list in lists)
                {
                    CheckListSpLists.Items.Add(new CheckListItem() { Display = list.Title, Value = list });
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }


        private CheckFieldItem LoadListFromSp(List spList)
        {
            CheckFieldItem listItem = new CheckFieldItem();
            listItem.SpList = spList;
            listItem.Fields = new List<FieldSelected>();

            SpContext.ClientContext.Load(spList.Fields);
            SpContext.ClientContext.ExecuteQuery();
            foreach (var field in spList.Fields.Where(a=> !InternalFields.List.Contains(a.InternalName)))
            {
                listItem.Fields.Add(new FieldSelected() { Field = field, Selected = false });
            }
            SpContext.SelectedListItems.Add(listItem);
            return listItem;
        }

        private void ShowList(List spList)
        {
            if (CurrentList != spList)
            {
                CurrentList = spList;
                CheckListSpFields.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                var currentList = SpContext.SelectedListItems.Where(a => a.SpList == spList).FirstOrDefault();
                if (currentList == null)
                {
                    currentList = LoadListFromSp(spList);
                }
                foreach (var field in currentList.Fields.Where(a=>!a.Field.Hidden).OrderBy(a=>a.Field.Title).Distinct())
                {
            
                    CheckListSpFields.Items.Add(new CheckListItem() { Display = field.Field.Title + " (" + field.Field.InternalName + ")", Value = field.Field }, field.Selected);
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void ResizeCheckListbox()
        {
            var spListWidth = (int)(this.Size.Width * .3);
            CheckListSpLists.Size = new Size(spListWidth, this.Size.Height - 135);
            CheckListSpLists.Location = new Point(0, 79);
            CheckListSpFields.Location = new Point(spListWidth, 79);
            CheckListSpFields.ClientSize = new Size((int)this.Size.Width - spListWidth - 15, this.Size.Height - 130);
        }

        private SelectedLists GetSelecetedLists()
        {
            SelectedLists allItems = new SelectedLists();
            allItems.SelectedListCollection = new List<SelectedList>();
            foreach (CheckListItem checkedItems in CheckListSpLists.CheckedItems)
            {
                var selectedList = new SelectedList();
                selectedList.CheckedField = new List<Field>();
                List list = (List)checkedItems.Value;
                var checkedFields = SpContext
                                    .SelectedListItems
                                    .Where(a => a.SpList == list)
                                    .FirstOrDefault()
                                    .Fields.Where(a => a.Selected)
                                    .ToList();
                selectedList.List = list;
                foreach (var checkedField in checkedFields)
                {
                    selectedList.CheckedField.Add(checkedField.Field);
                }
                allItems.SelectedListCollection.Add(selectedList);

            }
            return allItems;
        }

        #endregion

        #region Events

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SpContext.Clear();
            CheckListSpLists.Items.Clear();
            CheckListSpFields.Items.Clear();
            OpenConnect();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizeCheckListbox();
        }

        private void CheckListSpLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CheckListSpLists_SelectedIndexChanged_Disabled)
            {
                var selectedItem = (List)((CheckListItem)((CheckedListBox)sender).SelectedItem).Value;
                ShowList(selectedItem);

            }
        }

        private void CheckListSpFields_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!CheckListSpFields_ItemCheck_Disabled)
            {
                var selectedItem = (Field)((CheckListItem)CheckListSpFields.Items[e.Index]).Value;
                SpContext.SelectedListItems
                    .Where(a => a.SpList == CurrentList)
                    .FirstOrDefault()
                    .Fields
                    .Where(a => a.Field == selectedItem)
                    .FirstOrDefault()
                    .Selected = e.NewValue == CheckState.Checked;
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            var allItems = GetSelecetedLists();
            allItems.ContextName = SpContext.ContextName;
            allItems.ModelFoler = SpContext.ModelFolder;
            allItems.NamespaceString = SpContext.NamespaceString;
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveUtils saveUtil = new SaveUtils();
                saveUtil.SaveInfo(allItems, saveFile.FileName);
            }
        }

        private void toolStripOpen_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CheckListSpFields_ItemCheck_Disabled = true;
            CheckListSpLists_SelectedIndexChanged_Disabled = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveUtils saveUtil = new SaveUtils();
                var loadedSelectedLists = saveUtil.OpenInfo(openFile.FileName);
                SpContext.NamespaceString = loadedSelectedLists.NamespaceString;
                SpContext.ModelFolder = loadedSelectedLists.ModelFoler;
                SpContext.ContextName = loadedSelectedLists.ContextName;
                ClearChecked();
                foreach (var info in loadedSelectedLists.SelectedListCollection)
                {
                    for (int i = 0; i < CheckListSpLists.Items.Count; i++)
                    {
                        var listItem = (List)((CheckListItem)CheckListSpLists.Items[i]).Value;
                        if (listItem.Id == info.ListGuid)
                        {
                            CheckListSpLists.SetItemChecked(i, true);
                            var loadedList = LoadListFromSp(listItem);
                            if (info.CheckedFieldGuids != null)
                            {
                                foreach (var fieldListItem in loadedList.Fields)
                                {
                                    if (info.CheckedFieldGuids.Contains(fieldListItem.Field.Id))
                                    {
                                        fieldListItem.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CheckListSpFields_ItemCheck_Disabled = false;
            CheckListSpLists_SelectedIndexChanged_Disabled = false;
            
            Cursor.Current = Cursors.Default;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSave_Click(sender, e);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripOpen_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SpContext.Clear();
            CheckListSpLists.Items.Clear();
            CheckListSpFields.Items.Clear();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var selectedLists = GetSelecetedLists();
            selectedLists.ContextName = SpContext.ContextName;
            selectedLists.ModelFoler = SpContext.ModelFolder;
            selectedLists.NamespaceString = SpContext.NamespaceString;
            CreateCs createCs = new CreateCs(GetSelecetedLists());
            createCs.ShowDialog();
        }

        #endregion

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var fields = SpContext.SelectedListItems
                                  .Where(a => a.SpList == CurrentList)
                                  .FirstOrDefault()
                                  .Fields
                                  .Where(a => a.Selected)
                                  .Select(a=>a.Field)
                                  .ToList();
            SiteContent siteContent = new SiteContent(fields);
            siteContent.ShowDialog();
        }


    }
}
