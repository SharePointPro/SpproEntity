using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Utilities
{
    public class GeoUtility
    {
        public static void AddGeolocationField(ClientContext context, string listName, string columnName)
        {
            List oList = context.Web.Lists.GetByTitle(listName);
            oList.Fields.AddFieldAsXml(string.Format("<Field Type='Geolocation' DisplayName='{0}'/>", columnName), true, AddFieldOptions.AddToAllContentTypes);
            oList.Update();
            context.ExecuteQuery();
        } 

    }
}
