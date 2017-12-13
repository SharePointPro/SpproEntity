using SpproFramework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpproFramework.Extensions;
using System.Reflection;

namespace SpproFramework.Utilities
{
    internal class SpNameUtility
    {
        /// <summary>
        /// Get SharePoint Field Name from C# Property Name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static string GetSPFieldName(string propertyName, Type type)
        {
            var property = type.GetProperty(propertyName.CleanName().Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            string spFieldName;
            var customAttributes = property.GetCustomAttributes(typeof(SpproFieldAttribute), true);
            if (customAttributes.Length > 0)
            {
                var attribute = (SpproFieldAttribute)customAttributes[0];
                spFieldName = attribute.SpName;
            }
            else
            {
                spFieldName = propertyName.DirtyName();
            }
            return spFieldName;
        }

        /// <summary>
        /// Get C# Property Name from SharePoint Field Name
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(string spName, Type type)
        {
            var properties = type.GetProperties();

            foreach (var property in properties)
            {

                var customAttributes = property.GetCustomAttributes(typeof(SpproFieldAttribute), true);
                if (customAttributes.Length > 0)
                {
                    var attribute = (SpproFieldAttribute)customAttributes[0];
                    if (attribute.SpName == null &&
                        property.Name.ToLower() == spName.ToLower())
                    {
                        return property;
                    }
                    else if (attribute.DisplayName != null && attribute.DisplayName.ToLower() == spName.ToLower())
                    {
                        return property;
                    }
                    else if (attribute.SpName != null && attribute.SpName.ToLower() == spName.ToLower())
                    {
                        return property;
                 
                    }
                }
                    if (property.Name.ToLower() == spName.ToLower())
                    {
                        return property;
                    }
            }

            return null;
        }

        /// <summary>
        /// Get C# Property Name from SharePoint Field Name
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPropertyName(string spName, Type type)
        {

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
           
                var customAttributes = property.GetCustomAttributes(typeof(SpproFieldAttribute), true);
                if (customAttributes.Length > 0)
                {
                    var attribute = (SpproFieldAttribute)customAttributes[0];
                    if (attribute.SpName == null && 
                        property.Name == spName)
                    {
                        return spName.CleanName();
                    }
                    else if (attribute.DisplayName == spName)
                    {
                        return property.Name;
                    }
                    else if (attribute.SpName == spName)
                    {
                        return property.Name;
                    }
                }
            }

            return spName.CleanName();
        }
    }
}