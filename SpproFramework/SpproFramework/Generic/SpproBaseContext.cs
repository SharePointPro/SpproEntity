using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using SpproFramework.Extensions;
using SpproFramework.Attributes;
using Microsoft.SharePoint.Client;

namespace SpproFramework.Generic
{
    public abstract class SpproBaseContext
    {
        #region Static Members

        private static string GetSiteUrlFromConfig
        {
            get
            {
                return ConfigurationManager.AppSettings["SpUrl"];
            }
        }

        #endregion

        #region Constructors
        public SpproBaseContext(ClientContext clientContext)
        {
            var type = this.GetType();
            foreach (var property in type.GetProperties())
            {
                if (typeof(SpproRepository).IsAssignableFrom(property.PropertyType))
                {
                    var listType = typeof(SpproRepository<>);
                    var genericArgs = property.PropertyType.GetGenericArguments();
                    var concreteType = listType.MakeGenericType(genericArgs);
                    var listName = genericArgs[0].Name;
                    var customAttributes = genericArgs[0].GetCustomAttributes(typeof(SpproListAttribute), true);
                    if (customAttributes.Length > 0 && !string.IsNullOrWhiteSpace(((SpproListAttribute)customAttributes[0]).ListName))
                    {
                        listName = ((SpproListAttribute)customAttributes[0]).ListName;
                    }
                    var newRepo = Activator.CreateInstance(concreteType, listName, clientContext);
                    property.SetValue(this, newRepo);
                }
            }
        }
        #endregion

    }
}
