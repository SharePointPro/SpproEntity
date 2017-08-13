using SpproFramework.Attributes;
using SpproFramework.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpproFramework.Utilities
{
    public class CamlUtility<T> where T : ISpproEntity
    {
        #region Private Members

        private string GetFieldType(string propertyName)
        {
            var property = typeof(T).GetProperty(SpNameUtility.GetPropertyName(propertyName, typeof(T)), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            string spFieldType;
            var customAttributes = property.GetCustomAttributes(typeof(SpproFieldAttribute), true);
            if (customAttributes.Length > 0 && ((SpproFieldAttribute)customAttributes[0]).FieldType != null)
            {
                var attribute = (SpproFieldAttribute)customAttributes[0];
                spFieldType = attribute.FieldType;
            }
            else
            {
                if (TypeUtility.IsNumeric(property))
                {
                    spFieldType = "Number";
                }
                else if (property.GetType() == typeof(DateTime) || property.GetType() == typeof(DateTime?))
                {
                    spFieldType = "DateTime";
                }
                else
                {
                    //TO DO. Add all field types
                    spFieldType = "Text";
                }
            }
            return spFieldType;
        }


        private bool RecursiveView()
        {
            var listAttributes = (SpproListAttribute[])typeof(T).GetCustomAttributes(typeof(SpproListAttribute), true);
            if (listAttributes.Length > 0)
            {
                return listAttributes[0].Recursive;
            }
            return false;
        }

        #endregion

        #region Public Methods

        public string GenerateFromQueryString(string queryString)
        {
            var keyValues = Regex.Split(queryString, @"(\&|\|\|)");
            var xDoc = new XDocument();
            var currentDelimiter = "";

            for (int i = 0; i < keyValues.Count(); i++)
            {
                var isDelimiter = (i + 1) % 2 == 0;

                if (isDelimiter)
                {
                    currentDelimiter = keyValues[i];
                }
                else
                {

                    var keyValue = keyValues[i];
                    string operatorType = "", operatorString = "";
                    //Less than or Equal To
                    if (keyValue.Contains(">="))
                    {
                        operatorString = "Leq";
                        operatorType = ">=";
                    }
                    //Greater than or Equal To
                    else if (keyValue.Contains("<="))
                    {
                        operatorString = "Geq";
                        operatorType = "<=";                        
                    }
                    //Equal Operator
                    else if (keyValue.Contains("="))
                    {
                        operatorString = "eq";
                        operatorType = "=";
                    }
                    var key = keyValue.Split(new string[] { operatorType }, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (key != "_")
                    {
                        var value = keyValue.Split('=')[1];
                        var spFieldType = GetFieldType(key);
                        var spFieldName = SpNameUtility.GetSPFieldName(key, typeof(T));

                        var xmlElement = new XElement(operatorString,
                                              new XElement("FieldRef",
                                                  new XAttribute("Name", spFieldName),
                                                        spFieldType == "Lookup" ? new XAttribute("LookupId", "True") : new XAttribute("LookupId", "False")),
                                                  new XElement("Value",
                                                    new XAttribute("Type", spFieldType), value)
                                                       );

                        if (currentDelimiter != "")
                        {
                            switch (currentDelimiter)
                            {
                                case "&":
                                    xDoc = new XDocument(new XElement("And", xDoc.Root, xmlElement));
                                    break;

                                case "||":
                                    xDoc = new XDocument(new XElement("Or", xDoc.Root, xmlElement));
                                    break;
                            }
                        }
                        else
                        {
                            xDoc.Add(
                                xmlElement
                            );
                        }
                    }
                }
            }

            if (RecursiveView())
            {
                return new XDocument(new XElement("View",
                                              new XAttribute("Scope", "Recursive"),
                                                  new XElement("Query",
                                                      new XElement("Where",
                                                          xDoc.Root))))
                                                          .ToString();
            }
            else
            {
                return new XDocument(new XElement("View",
                                         new XElement("Query",
                                             new XElement("Where",
                                                xDoc.Root))))
                                                .ToString(SaveOptions.None);
            }

        }

        #endregion
    }
}
