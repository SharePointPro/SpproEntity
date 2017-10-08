using SpproFramework.Migrate.Model;
using SpproFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpproFramework.Migrate.Builders
{
    class CsBuilder
    {

        #region Constructors

        #endregion

        #region Private Methods

        private string CreateContextUsingDeclerations
        {
            get
            {
                return "using System;\n"
                      + "using System.Collections.Generic;\n"
                      + "using Microsoft.SharePoint.Client;\n\n"
                      + "using SpproFramework.Generic;\n\n";
            }
        }

        private string CreateEntityUsingDeclerations
        {
            get
            {
                return "using SpproFramework.Attributes;\n"
                      + "using SpproFramework.Generic;\n"
                      + "using System;\n"
                      + "using System.Collections.Generic;\n"
                      + "using Microsoft.SharePoint.Client;\n"
                      + "using System.Device.Location;\n\n";
            }
        }


        private string CreateNamespace(string nameSpaceString)
        {
            return string.Format("namespace {0}\n{{\n", nameSpaceString);
        }

        private string CreateContextClass(string contextName)
        {
            return string.Format("\tpublic class {0} : SpproBaseContext \n\t{{", contextName) +
                       string.Format("\n\n\t\tpublic {0}(ClientContext clientContext) : base(clientContext)\n\t\t{{\n\t\t}}\n\n", contextName);
        }

        private string CreateEntityClassDecleration(string className, string listName)
        {
            string classString = string.Empty;
            if (className != listName)
            {
                if (!string.IsNullOrWhiteSpace(listName))
                {
                    classString = string.Format("\t[SpproListAttribute(ListName=\"{0}\")]\n", listName);
                }
            }
            classString += string.Format("\tpublic class {0} : ISpproEntity\n \t{{\n", className);
            return classString;
        }

        private string CreateContextProperty(string propertyName)
        {
            return string.Format("\t\tpublic virtual SpproRepository<{0}> {0} {{ get; set; }}\n\n", propertyName);
        }

        private string CreateEntityMethod(string methodName, string typeName, string fieldType, bool readOnly, string spName)
        {
            string classString = string.Empty;
            string fieldTypeString = string.Empty;
            string readOnlyString = string.Empty;
            string spNameString = string.Format("SpName = \"{0}\"", spName);
            if (!string.IsNullOrWhiteSpace(fieldType))
            {
                fieldTypeString = string.Format("FieldType = \"{0}\", ", fieldType);
            }
            if (readOnly)
            {
                readOnlyString = string.Format(" ReadOnly = true, ", fieldType);
            }
            classString = string.Format("\t\t[SpproFieldAttribute({0}{1}{2})]\n", fieldTypeString, readOnlyString, spNameString);
            return string.Format("{0}\t\tpublic {1} {2} {{ get; set; }}\n\n", classString, typeName, methodName);
        }

        private string CloseClass()
        {
            return "\t}\n}";
        }

        private string CreateEntityClass(SelectedList selectedList, string nameSpaceString)
        {
            var classString = CreateEntityUsingDeclerations;
            classString += CreateNamespace(nameSpaceString);
            classString += CreateEntityClassDecleration(selectedList.List.Title.CleanName(), selectedList.List.Title);
            List<string> propertyNames = new List<string>();
            foreach (var field in selectedList.CheckedField)
            {
                string spType = null;
                string cType = null;
                string propertyName = field.Title.CleanName();
                //Propety names can not be duplicates in C#, we'll just add a increment next to it if it already exists
                int counter = 0;
                while (propertyNames.Contains(propertyName))
                {
                    propertyName = propertyName + counter++.ToString();
                }
                propertyNames.Add(propertyName);
                //Set the C# Type (cType) based on the SharePoint Type (spType)
                switch (field.TypeAsString)
                {
                    case "Geolocation":
                        cType = "GeoCoordinate";
                        break;

                    case "Counter":
                        spType = "Counter";
                        cType = "int";
                        break;

                    case "Number":
                    case "Currency":
                        cType = "decimal";
                        break;

                    case "Boolean":
                        cType = "bool";
                        break;

                    case "DateTime":
                        cType = "DateTime?";
                        break;

                    case "Lookup":
                        cType = "int";
                        spType = "Lookup";
                        if (propertyName.Substring(propertyName.Length - 2).ToLower() != "id")
                        {
                            propertyName = propertyName + "ID";
                        }
                        break;

                    case "User":
                        cType = "int";
                        spType = "User";
                        if (propertyName.Substring(propertyName.Length - 2).ToLower() != "id")
                        {
                            propertyName = propertyName + "ID";
                        }
                        break;

                    case "URL":
                        cType = "FieldUrlValue";
                        break;
                    default:
                        cType = "string";
                        break;
                }
                classString += CreateEntityMethod(propertyName, cType, spType, field.ReadOnlyField, field.InternalName);

            }
            classString += CloseClass();
            return classString;
        }



        #endregion

        public void Create(SelectedLists selectedLists,
                           string nameSpaceString,
                           string path,
                           string contextName)
        {
            //Create Context
            var contextFileName = path + "\\" + contextName + ".cs";
            var contextString = CreateContextUsingDeclerations;
            contextString += CreateNamespace(nameSpaceString);
            contextString += CreateContextClass(contextName);
            foreach (var list in selectedLists.SelectedListCollection)
            {
                //Create Entity Files   
                var classString = CreateEntityClass(list, nameSpaceString);
                string fileName = path + "\\" + list.List.Title.CleanName() + ".cs";
                if (System.IO.File.Exists(fileName))
                {
                    string newFileName = fileName + "-bak";
                    var counter = 0;
                    while (System.IO.File.Exists(newFileName))
                    {
                        newFileName = string.Format("{0}{1}", newFileName, counter++);
                    }
                    System.IO.File.Move(fileName, newFileName);
                }
                System.IO.File.WriteAllText(fileName, classString, Encoding.Unicode);
                contextString += CreateContextProperty(list.List.Title.CleanName());
            }
            contextString += CloseClass();
            if (System.IO.File.Exists(contextFileName))
            {
                string newFileName = contextFileName + "-bak";
                var counter = 0;
                while (System.IO.File.Exists(newFileName))
                {
                    newFileName = string.Format("{0}{1}", newFileName, counter++);
                }
                System.IO.File.Move(contextFileName, newFileName);
            }
            System.IO.File.WriteAllText(contextFileName, contextString);
        }
    }
}