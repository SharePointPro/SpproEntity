using InvokeSolutions.Docx.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpproFramework.Attributes;

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

        public void AddString(string name, object value)
        {
            if (value != null)
            {
                this.MergePair.Add(new MergePair(name, value.ToString()));
            }
            else
            {
                this.MergePair.Add(new MergePair(name, string.Empty));
            }
        }

        public void Add(string name, DataItem value)
        {
            if (value != null)
            {
                this.MergePair.Add(new MergePair(name, value));
            }
            else
            {
                this.MergePair.Add(new MergePair(name, string.Empty));
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
        public static MergeTemplate Create(object template, byte[] docx)
        {
            return new MergeTemplate();
        }


        /// <summary>
        /// Create Merge Template from Template Class
        /// </summary>
        /// <param name="mergeOptions">Template: DocxData
        ///                            Object to Reflect for values: ReflectedObject
        /// </param>
        /// <returns></returns>
        public static MergeTemplate Create(MergeOptions mergeOptions)
        {
            MergeTemplate mergeTemplate = new MergeTemplate();
            var templateType = mergeOptions.ReflectedObject.GetType();
            mergeTemplate.InputFile = new MemoryStream(mergeOptions.DocxData);
            var properties = templateType.GetProperties();

            //Iterate through all properties
            //and assign MergePair with a Place holder (defaulting to property.name) and Value (Defaulting to text value)
            foreach (var property in properties)
            {
                MergePair mergePair = new MergePair(property.Name);
                if (property.GetValue(mergeOptions.ReflectedObject) != null)
                {
                    string placeHolder = property.Name;

                    var customAttributes = property.GetCustomAttributes(typeof(SpproNavigationAttribute), true);
                    if (customAttributes.Length > 0 && ((SpproNavigationAttribute)customAttributes[0]).NavigationProperty)
                    {
                        var convertedDataItem = (TableDataItem)DataItem.ConvertFrom(property.GetValue(mergeOptions.ReflectedObject));
                        mergePair.Value = convertedDataItem;
                        mergeTemplate.MergePair.Add(mergePair);
                    }
                    else
                    {

                        switch (property.PropertyType.Name)
                        {
                            case "String":
                            case "Int":
                            case "Decimal":
                            case "Int32":
                            case "Int16":
                                mergePair.Value = property.GetValue(mergeOptions.ReflectedObject).ToString();
                                break;
                        }
                        mergeTemplate.MergePair.Add(mergePair);
                    }
                }
            }

            foreach (var imageOption in mergeOptions.ImageOptions)
            {
                MergePair mergePair = new MergePair(imageOption.PlaceHolder);
                byte[] imageData = imageOption.Content;
                //Images must have custom attirbutes decoration
                var image = new Bitmap(new MemoryStream(imageData));
                var dataItem = new ImageDataItem(imageData);


                if (imageOption.MaxWidth > 0)
                {
                    //Reduce Image Ratio using max width
                    if (image.Width > imageOption.MaxWidth)
                    {
                        var ratio = image.Width / imageOption.MaxWidth;
                        image = new Bitmap(image, new Size(imageOption.MaxWidth, image.Height / ratio));
                    }

                }
                else if (imageOption.MaxHeight > 0)
                {
                    //Reduce Image Ratio using max height
                    if (image.Height > imageOption.MaxHeight)
                    {
                        var ratio = image.Height / imageOption.MaxHeight;
                        image = new Bitmap(image, new Size(image.Width / ratio, imageOption.MaxHeight));
                    }
                }

                dataItem.cY = image.Height;
                dataItem.cX = image.Width;
                dataItem.Unit = Unit.Cm;
                
                mergePair.Value = dataItem;
                mergeTemplate.MergePair.Add(mergePair);

            }
            //Add System Automatic merge pairs
            mergeTemplate.MergePair.Add(new MergePair("_Today_", DateTime.Now.ToString("dd-MM-yyyy")));
            return mergeTemplate;
        }

        #endregion
    }


}
