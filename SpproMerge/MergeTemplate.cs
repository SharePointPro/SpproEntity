using InvokeSolutions.Docx.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproMerge
{
    public class MergeTemplate
    {

        #region Private Members

        private List<Byte[]> AddPages { get; set; }

        #endregion

        #region Internal Members

        internal List<string> RemoveColor { get; set; }

        internal List<string> LeaveAndRemoveColor { get; set; }

        internal Stream InputFile { get; set; }

        internal List<MergePair> MergePair { get; set; }

        #endregion

        #region Constructors

        public MergeTemplate()
        {
            MergePair = new List<MergePair>();
            LeaveAndRemoveColor = new List<string>();
            RemoveColor = new List<string>();
        }

        #endregion

        #region Public Methods

        public void Add(string name, object value)
        {
            if (value != null)
            {
                this.MergePair.Add(new MergePair(name, value.ToString()));
            }
        }

        public void AddPage(byte[] docx)
        {
            if (AddPages == null)
            {
                AddPages = new List<byte[]>();
            }
            AddPages.Add(docx);
        }

        //Merge 
        public byte[] Merge()
        {
            MergeDoc mergeDoc = new MergeDoc(this);
            return mergeDoc.Merge(AddPages);
        }

        #endregion

        #region Public Static Methods

        //Create Merge Template from Template Class
        public static MergeTemplate Create(object template, byte[] docx)
        {
            MergeTemplate mergeTemplate = new MergeTemplate();
            var templateType = template.GetType();
            mergeTemplate.InputFile = new MemoryStream(docx);
            var properties = templateType.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(template) != null)
                {
                    MergePair mergePair = new MergePair(property.Name);
                    switch (property.PropertyType.Name)
                    {                        
                        case "String":
                        case "Int":
                        case "Decimal":
                        case "Int32":
                        case "Int16":
                            mergePair.Value = property.GetValue(template).ToString();
                            break;
                    }
                    mergeTemplate.MergePair.Add(mergePair);
                }
            }
            //Add System Automatic merge pairs
            mergeTemplate.MergePair.Add(new MergePair("_Today_", DateTime.Now.ToString("dd-MM-yyyy")));
            return mergeTemplate;
        }

        #endregion
    }


}
