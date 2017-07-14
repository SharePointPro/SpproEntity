using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Configuration;
using SpproFramework.Utilities;
using System.ComponentModel;
using SpproFramework.Attributes;
using System.Device.Location;
using System.Web;
using System.Net;


namespace SpproFramework.Generic
{
    public class SpproRepository
    {
        
    }
    public class SpproRepository<T> : SpproRepository where T : ISpproEntity
    {
        #region Private Members

        private string ListName { get; set; }

        #endregion

        #region Internals Members

        internal string SiteUrl { get; set; }

        internal ClientContext ClientContext { get; set; }

        #endregion

        #region Constructors

        public SpproRepository(string listName, ClientContext clientContext)
        {
            this.ListName = listName;
            this.ClientContext = clientContext;
        }

        #endregion

        #region Private Methods

        private bool IsObjectFalsy(object obj)
        {
            int number;
            if (int.TryParse(obj.ToString(), out number))
            {
                return number == 0;
            }
            return false;
        }

        private ListItem SetValues(string[] keyValues, ListItem listItem, ref T sEntity)
        {
            foreach (var keyValue in keyValues)
            {
                try
                {
                    bool readOnly = false;
                    var key = keyValue.Split('=')[0];
                    var value = WebUtility.UrlDecode(keyValue.Split('=')[1]);                    
                    var property = typeof(T).GetProperty(SpNameUtility.GetPropertyName(key, typeof(T)));
                    var customAttributes = property.GetCustomAttributes(typeof(SpproFieldAttribute), true);

                    if (customAttributes.Count() > 0)
                    {
                        var attribute = (SpproFieldAttribute)customAttributes[0];
                        readOnly = attribute.ReadOnly;
                    }
                    if (!readOnly)
                    {

                        object finalValue = value;
                        var targetType = IsNullableType(property.PropertyType) ? Nullable.GetUnderlyingType(property.PropertyType) : property.PropertyType;
                        if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                        {
                            var format = value.Split('[', ']')[1];
                            value = value.Substring(0, value.IndexOf('['));
                            DateTime dateValue = new DateTime(); ;
                            if (DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out dateValue))
                            {
                                finalValue = dateValue;
                                property.SetValue(sEntity, Convert.ChangeType(finalValue, targetType));
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (property.PropertyType == typeof(GeoCoordinate))
                        {
                            var latitude = Convert.ToDouble(value.Split(',')[0]);
                            var longitude = Convert.ToDouble(value.Split(',')[1]);
                            var geoValue = new FieldGeolocationValue();
                            geoValue.Latitude = latitude;
                            geoValue.Longitude = longitude;
                            finalValue = geoValue;
                            var geoLocation = new GeoCoordinate(latitude, longitude);
                            property.SetValue(sEntity, Convert.ChangeType(geoLocation, targetType));
                        }
                        else if (property.PropertyType == typeof(Microsoft.SharePoint.Client.FieldUrlValue))
                        {
                            finalValue = new FieldUrlValue() { Description = value.Split(',')[0], Url = WebUtility.UrlDecode(value.Split(',')[1]) };
                            property.SetValue(sEntity, finalValue);
                        }
                        else
                        {
                            property.SetValue(sEntity, Convert.ChangeType(finalValue, targetType));
                        }
                        //Get SharePoint Field Name
                        key = SpNameUtility.GetSPFieldName(key, typeof(T));
                        listItem[key] = finalValue;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("error with " + keyValue, ex);
                }
            }

            return listItem;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        private T CreateInstance()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }


        #endregion

        #region Internal Methods

        /// <summary>
        /// Populate SharePoint Entity using Reflection
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        internal T PopulateSEntity(ListItem item)
        {
            var entity = CreateInstance();
            entity.ID = item.Id;
            foreach (var field in item.FieldValues)
            {
              
                try
                {                    
                    var property = typeof(T).GetProperty(SpNameUtility.GetPropertyName(field.Key, typeof(T)));
                    if (property != null)
                    {
                        var targetType = property.PropertyType;
                        //Nullable properties have to be treated differently, since we 
                        //  use their underlying property to set the value in the object
                        if (targetType.IsGenericType
                            && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            //if it's null, just set the value from the reserved word null, and return
                            if (field.Value == null)
                            {
                                property.SetValue(entity, null, null);
                                continue;
                            }

                            //Get the underlying type property instead of the nullable generic
                            targetType = new NullableConverter(property.PropertyType).UnderlyingType;
                        }
                        var customAttributes = property.GetCustomAttributes(typeof(SpproFieldAttribute), true);
                        if (customAttributes.Length > 0 && !string.IsNullOrWhiteSpace(((SpproFieldAttribute)customAttributes[0]).FieldType))
                        {
                            var attribute = (SpproFieldAttribute)customAttributes[0];
                            switch (attribute.FieldType)
                            {
                                case "Lookup":
                                    if (field.Value != null && !(IsObjectFalsy(field.Value)))
                                    {
                                        dynamic value;
                                        switch (attribute.FieldValue)
                                        {
                                            case "Value":
                                                value = ((FieldLookupValue)field.Value).LookupId;
                                                property.SetValue(entity, value);
                                                break;
                                            case "Id":
                                            case null:
                                                if (field.Value == null || field.Value.ToString() == "")
                                                {
                                                    property.SetValue(entity, 0);
                                                }
                                                else
                                                {
                                                    value = ((FieldLookupValue)field.Value).LookupId;
                                                    property.SetValue(entity, value);
                                                }
                                                break;
                                        }
                                    }
                                    break;

                                case "User":
                                    if (field.Value != null)
                                    {
                                        dynamic value;
                                        switch (attribute.FieldValue)
                                        {
                                            case "Value":
                                                value = ((FieldUserValue[])field.Value).Select(a => a.LookupValue).ToList();
                                                property.SetValue(entity, value);
                                                break;
                                            case "IdAndValue":
                                                value = ((IEnumerable<dynamic>)((FieldUserValue[])field.Value).Select(a => new { a.LookupId, a.LookupValue })).ToList();
                                                property.SetValue(entity, value);
                                                break;
                                            default:
                                            case "Id":
                                            case null:
                                                if (field.Value is FieldUserValue[])
                                                {
                                                    value = ((FieldUserValue[])field.Value).Select(a => a.LookupId).ToList();
                                                    property.SetValue(entity, value);
                                                }
                                                else
                                                {
                                                    value = ((FieldUserValue)field.Value).LookupId;
                                                    property.SetValue(entity, value);
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            //Deal with null SP Columns
                            if (TypeUtility.IsNumeric(targetType) && field.Value == null)
                            {
                                property.SetValue(entity, Convert.ChangeType(0, targetType));
                            }
                            else
                            {
                                //Handle unusal object types (ie GeoCoordinate = FieldGeolocation)
                                switch (targetType.Name)
                                {
                                    case "GeoCoordinate":
                                        GeoCoordinate GeoValue = new GeoCoordinate();
                                        GeoValue.Longitude = ((FieldGeolocationValue)field.Value).Longitude;
                                        GeoValue.Latitude = ((FieldGeolocationValue)field.Value).Latitude;
                                        property.SetValue(entity, GeoValue);
                                        break;
                                    case "Boolean":
                                        if (field.Value == null)
                                        {
                                            property.SetValue(entity, false);
                                        }
                                        else
                                        {
                                            property.SetValue(entity, field.Value.ToString().ToLower() == "yes" || field.Value.ToString().ToLower() == "true");
                                        }
                                        break;
                                    default:
                                        property.SetValue(entity, Convert.ChangeType(field.Value, targetType));
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error with " + field, ex);
                }
            }

            return entity;
        }

        #endregion

        #region Public Methods

        public List<T> Query(string queryString)
        {
            var itemList = new List<T>();
            var list = GetList();
            var camlUtility = new CamlUtility<T>();
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = camlUtility.GenerateFromQueryString(queryString);
            var listItems = list.GetItems(camlQuery);
            ClientContext.Load(listItems);
            ClientContext.ExecuteQuery();

            foreach (var item in listItems)
            {
                itemList.Add(PopulateSEntity(item));
            }
            return itemList;
        }

        public T UpdateOrCreate(ISpproEntity entity)
        {
            //Create Form Data from entity
            List<string> formData = new List<string>();
            int id = 0;
            foreach (var property in entity.GetType().GetProperties())
            {
                var name = property.Name;
                var value = entity.GetType().GetProperty(name).GetValue(entity);
                if (name.ToLower() != "id" && value != null)
                {
                    if (value is DateTime?)
                    {
                        formData.Add(string.Format("{0}={1}[dd-MM-yyyy hh:mm tt]", name, ((DateTime?)value).Value.ToString("dd-MM-yyyy hh:mm tt")));
                    }
                    else if (value is FieldUrlValue)
                    {
                        formData.Add(string.Format("{0}={1},{2}", name, ((FieldUrlValue)value).Description, WebUtility.UrlEncode(((FieldUrlValue)value).Url)));
                    }
                    else
                    {
                        formData.Add(string.Format("{0}={1}", name, WebUtility.UrlEncode(value.ToString())));
                    }
                }
                else if (name.ToLower() == "id")
                {
                    id = (int)value;
                }
            }
            if (id > 0)
            {
                return Update(id, string.Join("&", formData));
            }
            else
            {
                return Create(string.Join("&", formData));
            }
        }

        /// <summary>
        /// Update using Form Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formDaa"></param>
        /// <returns></returns>
        public T Update(int id, string formData)
        {
            var sEntity = CreateInstance();
            var list = GetList();
            var listItem = list.GetItemById(id);
            var keyValues = formData.Split('&');
            listItem = SetValues(keyValues, listItem, ref sEntity);
            listItem.Update();
            ClientContext.ExecuteQuery();
            return sEntity;
        }

        /// <summary>
        /// Create using Form Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formDaa"></param>
        /// <returns></returns>
        public T Create(string formData)
        {
            var sEntity = CreateInstance();
            var list = GetList();
            this.ClientContext.Load(list);
            var listItem = CreateListItem();
            var keyValues = formData.Split('&');
            listItem = SetValues(keyValues, listItem, ref sEntity);
            listItem.Update();
            ClientContext.ExecuteQuery();
            sEntity.ID = listItem.Id;
            return sEntity;
        }

        public List GetList()
        {
            List oList = ClientContext.Web.Lists.GetByTitle(ListName);
            return oList;
        }

        public ListItem CreateListItem()
        {
            var oList = GetList();
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem oListItem = oList.AddItem(itemCreateInfo);
            return oListItem;
        }

        public T GetById(int id)
        {
            var list = GetList();
            var listItem = list.GetItemById(id);
            ClientContext.Load(listItem);
            ClientContext.ExecuteQuery();
            return PopulateSEntity(listItem);
        }

        public List<T> GetAll()
        {
            var list = GetList();
            var itemList = new List<T>();
            CamlQuery query = CamlQuery.CreateAllItemsQuery();
            var listItems = list.GetItems(query);
            ClientContext.Load(listItems);
            ClientContext.ExecuteQuery();
            foreach (var item in listItems)
            {
                itemList.Add(PopulateSEntity(item));
            }
            return itemList;

        }
        #endregion


    }
}
