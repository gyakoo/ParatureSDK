using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using ParatureSDK.Fields;
using ParatureSDK.ParaObjects;
using ParatureSDK.ParaObjects.EntityReferences;
using Action = ParatureSDK.ParaObjects.Action;

namespace ParatureSDK
{
    internal class XmlGenerator
    {
        static internal XmlDocument GenerateXml(ParaEntity entity)
        {
            var entityType = entity.GetType().Name;
            var doc = new XmlDocument();
            var rootNode = doc.CreateElement(entityType);
            if (entity.Id > 0)
            {
                var attribute = doc.CreateAttribute("id");
                attribute.Value = entity.Id.ToString();
                rootNode.Attributes.Append(attribute);
            }

            foreach (var sf in entity.StaticFields)
            {
                bool foundFieldType = false;

                var fieldVal = sf.Value;
                //For Static fields set FieldType to ignore to skip xml generation
                if (fieldVal == null || sf.IgnoreSerializeXml)
                {
                    continue;
                }

                //generate the nested XML for entity references
                if (fieldVal is IEntityReference)
                {
                    var entRef = fieldVal as IEntityReference;
                    var entityRefType = entRef.GetEntity().GetType().Name; //property name in the class
                    //check if there is an XmlElement attribute on the property. Null if none exists
                    var entRefTypeOverride = GetEntityReferenceXmlAttributeValue(entRef);

                    XmlGenerateComplexEntityNode(doc, rootNode, sf.Name, entRefTypeOverride ?? entityRefType, "id", entRef.GetEntity().Id.ToString());
                    foundFieldType = true;
                }

                //List fields
                //TODO: Need to use the XmlAttributes, since class types may not match the item node name (ex CsrRole vs Role)
                if (sf.FieldType == "entitymultiple")
                {
                    var node = doc.CreateElement(sf.Name);
                    var sfList = sf.Value as IEnumerable<ParaEntityBaseProperties>;
                    var attList = sf.Value as IEnumerable<Attachment>;

                    //attachments are a special case that don't extend ParaEntityBaseProperties, so check first
                    //Check the internal setting to determine whether to delete the list of attachments (send an empty node)
                    if (attList != null && (entity.GetFieldValue<bool?>("AllowDeleteAllAttachments") == true || attList.Any()))
                    {
                        var nodeName = sf.Name;
                        node = XmlGenerateAttachmentNodes(doc, nodeName, attList);
                        rootNode.AppendChild(node);
                    }
                    //Don't populate if the list is empty
                    else if (sfList != null && sfList.Any())
                    {
                        //Handle the very specific download folders scenario
                        if (entity is Download && sf.Name == "Folders")
                        {
                            node = GenerateDownloadFoldersNode(entity as Download, doc, rootNode);
                            rootNode.AppendChild(node);
                        }
                        else
                        {
                            foreach (var ent in sfList)
                            {
                                var nodeName = ent.GetType().Name;
                                var nodechild = doc.CreateElement(nodeName);
                                var attribute = doc.CreateAttribute("id");
                                attribute.Value = ent.Id.ToString();
                                nodechild.Attributes.Append(attribute);
                                node.AppendChild(nodechild);
                            }
                        }

                        rootNode.AppendChild(node);
                    }
                    foundFieldType = true;
                }

                //for simple types, a tostring suffices
                if (fieldVal is Int32
                    || fieldVal is Int64
                    || fieldVal is string)
                {
                    XmlGenerateElement(doc, rootNode, sf.Name, sf.Value.ToString());
                    foundFieldType = true;
                }
                //C# outputs boolean.ToString as "True" or "False"... Needs to be lower case for XML
                if (fieldVal is bool)
                {
                    XmlGenerateElement(doc, rootNode, sf.Name, sf.Value.ToString().ToLower());
                    foundFieldType = true;
                }
                //DateTimes are more odd
                if (fieldVal is DateTime)
                {
                    var val = (DateTime) fieldVal;
                    const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
                    XmlGenerateElement(doc, rootNode, sf.Name, val.ToString(dateTimeFormat));
                    foundFieldType = true;
                }

                if (!foundFieldType)
                {
                    throw new Exception(String.Format("No XML generation logic found for field type: {0}", sf.FieldType));
                }
            }

            ObjectGenerateCustomFieldsXml(doc, rootNode, entity.CustomFields);
            doc.AppendChild(rootNode);
            return doc;
        }

        static public XmlDocument GenerateActionXml<TEntity>(Action obj) where TEntity : ParaEntity
        {
            return GenerateActionXml(obj, typeof (TEntity).Name);
        }

        /// <summary>
        /// Generate the XML needed to run an action.
        /// </summary>
        static public XmlDocument GenerateActionXml(Action obj, string module)
        {
            var doc = new XmlDocument();
            var objNode = doc.CreateElement("Action");
            var actionWrapperNode = doc.CreateElement(module);
            var actionNode = doc.CreateElement("Action");
            var attribute = doc.CreateAttribute("id");
            attribute.Value = obj.Id.ToString();

            objNode.Attributes.Append(attribute);


            if (obj.TimeSpentHours > 0)
            {
                XmlGenerateElement(doc, objNode, "TimeSpentHours", obj.TimeSpentHours.ToString());
            }

            if (obj.TimeSpentMinutes > 0)
            {
                XmlGenerateElement(doc, objNode, "TimeSpentMinutes", obj.TimeSpentMinutes.ToString());
            }

            if (obj.EmailCustList != null)
            {
                if (obj.EmailCustList.Count > 0)
                {
                    XmlGenerateElementFromArray(doc, objNode, "Emailcustlist", obj.EmailCustList, ",");
                }
            }
            if (obj.EmailCsrList != null)
            {
                if (obj.EmailCsrList.Count > 0)
                {
                    XmlGenerateElementFromArray(doc, objNode, "EmailCsrList", obj.EmailCsrList, ",");
                }
            }

            if (module == "Ticket")
            {
                XmlGenerateElement(doc, objNode, "ShowToCust", obj.ShowToCust.ToString().ToLower());
            }


            if (obj.Comment != null)
            {
                if (string.IsNullOrEmpty(obj.Comment) == false)
                {
                    XmlGenerateElement(doc, objNode, "Comment", obj.Comment);
                }
            }
            if (obj.EmailText != null)
            {
                if (string.IsNullOrEmpty(obj.EmailText) == false)
                {
                    XmlGenerateElement(doc, objNode, "Emailtext", obj.EmailText);
                }
            }
            if (obj.AssignToQueue != null && obj.AssignToQueue > 0)
            {
                XmlGenerateElement(doc, objNode, "AssignToQueue", obj.AssignToQueue.ToString());
            }
            if (obj.AssignToCsr != null && obj.AssignToCsr > 0)
            {
                XmlGenerateElement(doc, objNode, "AssignToCsr", obj.AssignToCsr.ToString());
            }

            if (obj.Action_Attachments != null && obj.Action_Attachments.Any())
            {
                var node = XmlGenerateAttachmentNodes(doc, "Action_Attachments", obj.Action_Attachments);
                objNode.AppendChild(node);
            }

            actionNode.AppendChild(objNode);
            actionWrapperNode.AppendChild(actionNode);
            doc.AppendChild(actionWrapperNode);
            return doc;
        }

        private static XmlElement GenerateDownloadFoldersNode(Download obj, XmlDocument doc, XmlElement objNode)
        {
            if (!obj.MultipleFolders && obj.Folders.Count > 1)
            {
                throw new ArgumentOutOfRangeException("Folders",
                    "There are too many folders for this Download. MultipleFolders is set to false.");
            }

            //Need to handle multiple folders
            XmlElement node;
            if (obj.MultipleFolders)
            {
                node = doc.CreateElement("Folders");
                foreach (var folder in obj.Folders)
                {
                    var nodechild = doc.CreateElement("DownloadFolder");
                    var attribute = doc.CreateAttribute("id");
                    attribute.Value = folder.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
            }
            else
            {
                node = doc.CreateElement("Folder");
                var folder = obj.Folders.FirstOrDefault();
                var nodechild = doc.CreateElement("DownloadFolder");
                var attribute = doc.CreateAttribute("id");
                attribute.Value = folder.Id.ToString();
                nodechild.Attributes.Append(attribute);
                node.AppendChild(nodechild);
            }

            objNode.AppendChild(node);
            return node;
        }

        static public XmlDocument GenerateXml(Folder folder)
        {
            var folderType = folder.GetType().ToString();
            var doc = new XmlDocument();
            var objNode = doc.CreateElement(folderType);
            if (folder.Id > 0)
            {
                var attribute = doc.CreateAttribute("id");
                attribute.Value = folder.Id.ToString();
                objNode.Attributes.Append(attribute);
            }

            XmlGenerateElement(doc, objNode, "Is_Private", folder.Is_Private.ToString().ToLower());
            XmlGenerateElement(doc, objNode, "Name", folder.Name);
            XmlGenerateElement(doc, objNode, "Description", folder.Description);
            XmlGenerateComplexEntityNode(doc, objNode, "Parent_Folder", folderType, "id", folder.Parent_Folder.Id.ToString());

            doc.AppendChild(objNode);
            return doc;
        }

        /// <summary>
        /// An internal method that generates a node and apprend it to the xmldocument root element passed to it.
        /// </summary>       
        static void XmlGenerateElement(XmlDocument doc, XmlNode objNode, string nodename, string nodevalue)
        {
            XmlNode node = doc.CreateElement(nodename);
            node.InnerText = nodevalue;
            objNode.AppendChild(node);
        }

        /// <summary>
        /// An internal method that generates a CData node and apprend it to the xmldocument root element passed to it.
        /// </summary>       
        static void XmlGenerateCDataElement(XmlDocument doc, XmlNode objNode, string nodename, string nodevalue)
        {
            if (nodevalue.Contains("]]>"))
            {
                XmlGenerateElement(doc, objNode, nodename, nodevalue);
            }
            else
            {
                var CData = doc.CreateCDataSection(nodevalue);
                XmlNode node = doc.CreateElement(nodename);
                node.AppendChild(CData);
                objNode.AppendChild(node);
            }
        }

        /// <summary>
        /// An internal method that generates a node from a string array and apprend it to the xmldocument root element passed to it.
        /// </summary>
        static void XmlGenerateElementFromArray(XmlDocument doc, XmlNode objNode, string nodename, ArrayList nodevalue, string separator)
        {
            var node = doc.CreateElement(nodename);
            var value = "";
            var lastvalue = false;

            for (var i = 0; i < nodevalue.Count; i++)
            {
                lastvalue = false;
                if (i == nodevalue.Count - 1)
                {
                    lastvalue = true;
                }
                if (lastvalue == false)
                {
                    value = value + nodevalue[i].ToString() + separator;
                }
                else
                {
                    value = value + nodevalue[i].ToString();
                }
            }

            node.InnerText = value;
            objNode.AppendChild(node);

        }

        /// <summary>
        /// An internal method that generates a complex entity node with an external name and an internal element name to apprend it to the xmldocument root element passed to it.
        /// </summary>
        static void XmlGenerateComplexEntityNode(XmlDocument doc, XmlNode objNode, string externalNodeName, string internalNodeName, string attributeName, string attributeValue)
        {

            var node = doc.CreateElement(externalNodeName);
            var nodechild = doc.CreateElement(internalNodeName);

            var attribute = doc.CreateAttribute(attributeName);
            attribute.Value = attributeValue;

            nodechild.Attributes.Append(attribute);
            node.AppendChild(nodechild);
            objNode.AppendChild(node);

        }

        static XmlElement XmlGenerateAttachmentNodes(XmlDocument doc, string nodeName, IEnumerable<Attachment> attachments)
        {
            var node = doc.CreateElement(nodeName);
            foreach (var at in attachments)
            {
                ObjectGenerateAttachmentNode(doc, node, at.GetType().Name, at);
            }
            return node;
        }

        /// <summary>
        /// Appends a single attachment node.
        /// </summary>
        static void ObjectGenerateAttachmentNode(XmlDocument doc, XmlNode objNode, string attachmentNodeName, Attachment attachment)
        {
            var node = doc.CreateElement(attachmentNodeName);

            var guid = doc.CreateElement("Guid");
            guid.InnerText = attachment.Guid.ToString();
            var name = doc.CreateElement("Name");
            name.InnerText = attachment.Name.ToString();
            node.AppendChild(guid);
            node.AppendChild(name);
            objNode.AppendChild(node);
        }

        /// <summary>
        /// Loops through the custom fields and prepares (then appends) the whole XML portion dealing with them.
        /// </summary>
        static void ObjectGenerateCustomFieldsXml(XmlDocument doc, XmlNode objNode, IEnumerable<CustomField> customFields)
        {
            foreach (var cf in customFields)
            {
                XmlNode cfnode = null;
                cfnode = doc.CreateElement("Custom_Field");
                var attid = doc.CreateAttribute("id");
                attid.Value = cf.Id.ToString();
                cfnode.Attributes.Append(attid);

                bool hascustomfields = false;

                int cfocount = cf.Options.Count;

                if (cfocount > 0)
                {
                    bool haschild = false;

                    foreach (var cfo in cf.Options)
                    {
                        XmlNode cfonode = doc.CreateElement("Option");
                        var cfoattid = doc.CreateAttribute("id");
                        cfoattid.Value = cfo.Id.ToString();
                        cfonode.Attributes.Append(cfoattid);

                        if (cfo.Selected == true)
                        {
                            var attSel = doc.CreateAttribute("selected");
                            attSel.Value = "true";
                            cfonode.Attributes.Append(attSel);
                            haschild = true;
                        }

                        cfnode.AppendChild(cfonode);
                    }

                    if (haschild || cf.FlagToDelete)
                    {
                        hascustomfields = true;
                    }

                }
                else
                {
                    if (cf.FlagToDelete)
                    {
                        hascustomfields = true;
                        cf.Value = null;
                    }
                    else if (cf.Value != null)
                    {
                        hascustomfields = true;
                        cfnode.InnerText = cf.Value;
                    }
                }

                if (cf.FlagToDelete)
                {
                    hascustomfields = true;
                }
                if (hascustomfields == true)
                {
                    XmlAttribute atid = doc.CreateAttribute("id");
                    atid.Value = cf.Id.ToString();
                    cfnode.Attributes.Append(atid);
                    objNode.AppendChild(cfnode);
                }
            }
        }

        internal static string GetEntityReferenceXmlAttributeValue(IEntityReference entRef)
        {
            var entRefTypeOverride = entRef.GetType();
            //first attribute, which should be the overridden property if this isn't the base class
            var firstAttribute = entRefTypeOverride
                .GetProperties()
                .FirstOrDefault();
            if (firstAttribute != null)
            {
                var xmlElement = firstAttribute.GetCustomAttributes(false)
                    .OfType<XmlElementAttribute>().FirstOrDefault();
                if (xmlElement != null)
                {
                    return xmlElement.ElementName;
                }
            }

            return null;
        }
    }
}
