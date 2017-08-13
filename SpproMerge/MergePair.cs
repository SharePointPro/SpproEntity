using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproMerge
{
    class MergePair
    {
        #region Public Members

        public string PlaceHolder { get; set; }

        public InvokeSolutions.Docx.Data.DataItem Value { get; set; }

        #endregion

        #region Constructors

        public MergePair(string placeHolder)
        {
            this.PlaceHolder = placeHolder;
        }

        public MergePair(string placeHolder, InvokeSolutions.Docx.Data.DataItem value)
        {
            this.PlaceHolder = placeHolder;
            this.Value = value;
        }

        #endregion

    }
}
