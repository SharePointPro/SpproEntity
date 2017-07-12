using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Migrate.Model
{
    internal class FieldSelected
    {
        internal Field Field { get; set; }

        internal bool Selected { get; set; }
    }
    internal class CheckFieldItem
    {
        public List SpList { get; set; }

        public List<FieldSelected> Fields { get; set; }
    }
}
