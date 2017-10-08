using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Generic
{
    public abstract class SpproBaseRepository
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

        #region Internal Methods


        internal List GetList()
        {
            List oList = ClientContext.Web.Lists.GetByTitle(ListName);
            ClientContext.Load(oList);
            ClientContext.Load(oList, includes => includes.Fields);
            ClientContext.ExecuteQuery();
            return oList;
        }


        internal ListItem CreateListItem()
        {
            var oList = GetList();
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem oListItem = oList.AddItem(itemCreateInfo);
            return oListItem;
        }

        #endregion
    }
}
