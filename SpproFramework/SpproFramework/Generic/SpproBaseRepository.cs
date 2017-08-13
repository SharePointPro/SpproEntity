using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Generic
{
    abstract class SpproBaseRepository
    {
        #region Internal Members
        internal string ListName { get; set; }

        internal bool LazyLoading = true;

        internal string SiteUrl { get; set; }

        internal ClientContext ClientContext { get; set; }

        #endregion

        #region Constructor

        internal SpproBaseRepository(string listName, ClientContext clientContext)
        {
            this.ListName = listName;
            this.ClientContext = clientContext;
        }

        #endregion

    }
}
