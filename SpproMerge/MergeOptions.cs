using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproMerge
{
    public class ImageOptions
    {
        public byte[] Content { get; set; }

        public string PlaceHolder { get; set; }

        public int MaxWidth { get; set; }

        public int MaxHeight { get; set; }
    }

    public class MergeOptions
    {
        #region Public Properties

        /// <summary>
        /// object to be reflected for values
        /// </summary>
        public object ReflectedObject { get; set; }

        /// <summary>
        /// Templat doxc file with placeholders
        /// </summary>
        public byte[] DocxData { get; set; }

        /// <summary>
        /// List of images to insert
        /// </summary>
        public List<ImageOptions> ImageOptions { get; set; }

        #endregion

        #region Constructors

        public MergeOptions()
        {
            this.ImageOptions = new List<ImageOptions>();
        }

        #endregion
    }
}
