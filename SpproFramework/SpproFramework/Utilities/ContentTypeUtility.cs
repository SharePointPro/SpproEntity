using Microsoft.SharePoint.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpproFramework.Utilities
{
    public class ContentTypeUtility
    {
        private static void AddAttributeValue(XDocument xdoc, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                xdoc.Root.SetAttributeValue(name, value);
            }
        }

        public static Field CreateSiteColumn(ClientContext context,
                                            string typeName,
                                            string displayName,
                                            string required,
                                            string enforceUniqueValues,
                                            string showField,
                                            string unlimitedLengthInDocumentLibrary,
                                            string relationshipDeleteBehavior,
                                            string staticName,
                                            string name,
                                            string sourceId,
                                            string list)
        {
            XDocument xdoc = XDocument.Parse("<Field />");
            xdoc.Root.SetAttributeValue("Type", typeName);
            xdoc.Root.SetAttributeValue("DisplayName", displayName);
            AddAttributeValue(xdoc, "Required", required);
            AddAttributeValue(xdoc, "EnforceUniqueValues", enforceUniqueValues);
            AddAttributeValue(xdoc, "ShowField", showField);
            AddAttributeValue(xdoc, "UnlimitedLengthInDocumentLibrary", unlimitedLengthInDocumentLibrary);
            AddAttributeValue(xdoc, "RelationshipDeleteBehavior", relationshipDeleteBehavior);
            AddAttributeValue(xdoc, "StaticName", staticName);
            AddAttributeValue(xdoc, "Name", name);
            //AddAttributeValue(xdoc, "SourceID", sourceId);
            AddAttributeValue(xdoc, "List", list);

            string schemaXml = xdoc.ToString();
            var web = context.Web;
            var field = web.Fields.AddFieldAsXml(schemaXml, true, AddFieldOptions.DefaultValue);
            context.ExecuteQuery();
            return field;
        }

        public static string CreateUnqiueName(List<Field> fields, string name)
        {
            int counter = 1;
            var newName = string.Format("{0}{1}", name, counter);
            while (fields.Where(a => a.InternalName.ToLower() == newName.ToLower()).Any())
            {
                newName = string.Format("{0}{1}", name, ++counter);
            }
            return newName;
        }
        public static void CreateContentType(ClientContext context,
                                      ContentType parentContentType,
                                      string contentName,
                                      string description,
                                      string contentGroup,
                                      List<Field> fields)
        {
            //// Get the content type collection for the website
            ContentTypeCollection contentTypeColl = context.Web.ContentTypes;
            
            ContentType contentType = contentTypeColl.Where(a=>a.Name == contentName).FirstOrDefault();
            if (contentType == null)
            {
                //// Specifies properties that are used as parameters to initialize a new content type.
                ContentTypeCreationInformation contentTypeCreation = new ContentTypeCreationInformation();
                contentTypeCreation.Name = contentName;
                contentTypeCreation.Description = contentName;
                contentTypeCreation.Group = contentGroup;
                contentTypeCreation.ParentContentType = parentContentType;
                //// Add the new content type to the collection
                contentType = contentTypeColl.Add(contentTypeCreation);
            }
            var siteFields = context.Web.Fields;
            context.Load(siteFields);
            context.Load(contentType);
            context.Load(contentType.Fields);
            context.ExecuteQuery();
            var existingSiteFields = siteFields.ToList();

            foreach (var field in fields)
            {   var siteField = field;
                if (!contentType.Fields.Where(a=> a.InternalName == field.InternalName).Any())
                {
                var fieldSchema = XDocument.Parse(field.SchemaXml);
                string internalName = CreateUnqiueName(existingSiteFields, fieldSchema.Root.Attribute("StaticName").Value);
                string typeName = fieldSchema.Root.Attribute("Type").Value;
                string displayName = fieldSchema.Root.Attribute("DisplayName").Value;
                string required = fieldSchema.Root.Attribute("Required") != null ? fieldSchema.Root.Attribute("Required").Value : "";
                string enforceUniqueValues = fieldSchema.Root.Attribute("EnforceUniqueValues") != null ? fieldSchema.Root.Attribute("EnforceUniqueValues").Value : "";
                string showField = fieldSchema.Root.Attribute("ShowField") != null ? fieldSchema.Root.Attribute("ShowField").Value : "";
                string unlimitedLengthInDocumentLibrary = fieldSchema.Root.Attribute("UnlimitedLengthInDocumentLibrary") != null ? fieldSchema.Root.Attribute("UnlimitedLengthInDocumentLibrary").Value : "";
                string relationshipDeleteBehavior = fieldSchema.Root.Attribute("RelationshipDeleteBehavior") != null ? fieldSchema.Root.Attribute("RelationshipDeleteBehavior").Value : "";
                string staticName = internalName;
                string name = internalName;
                string sourceId = fieldSchema.Root.Attribute("SourceID") != null ? fieldSchema.Root.Attribute("SourceID").Value : "";
                string list = fieldSchema.Root.Attribute("List") != null ? fieldSchema.Root.Attribute("List").Value : "";
                var test = existingSiteFields.Where(a => a.InternalName.ToLower() == "email1").FirstOrDefault();
                if (!existingSiteFields.Where(a => a.InternalName == field.InternalName).Any())
                {
                    siteField = CreateSiteColumn(context, typeName, displayName, required, enforceUniqueValues, showField, unlimitedLengthInDocumentLibrary, relationshipDeleteBehavior, staticName, name, sourceId, list);
                }
                contentType.FieldLinks.Add(new FieldLinkCreationInformation()
                {
                    Field = siteField
                });

                contentType.Update(true);
                context.Load(contentType);
                context.Load(contentType.FieldLinks);
                context.ExecuteQuery();
                }
            }
        }
    }
}
