using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using SpproFramework.Migrate.Model;

namespace SpproFramework.Migrate
{
    static class SpContext
    {
        public static string SpUrl { get; set; }

        public static ClientContext ClientContext { get; set; }

        public static string ModelFolder { get; set; }

        public static string NamespaceString { get; set; }

        public static string ContextName { get; set; }

        private static List<CheckFieldItem> _SelectedListItems { get; set; }
        public static List<CheckFieldItem> SelectedListItems
        {
            get
            {
                if (_SelectedListItems == null)
                {
                    _SelectedListItems = new List<CheckFieldItem>();
                }
                return _SelectedListItems;
            }
        }

        public static void Clear()
        {
            SpUrl = null;
            ClientContext = null;
            _SelectedListItems = null;
        }
    }
}
