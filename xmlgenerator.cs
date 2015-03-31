using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ParatureSDK.Fields;
using ParatureSDK.ParaObjects;
using ParatureSDK.ParaObjects.EntityReferences;
using Action = ParatureSDK.ParaObjects.Action;

namespace ParatureSDK
{
    internal class XmlGenerator
    {
        //WIP - not production ready
        static public XmlDocument GenerateXml(ParaEntity entity)
        {
            var entityType = entity.GetType().ToString();
            var doc = new XmlDocument();
            XmlNode objNode = doc.CreateElement(entityType);
            if (entity.Id > 0)
            {
                var attribute = doc.CreateAttribute("id");
                attribute.Value = entity.Id.ToString();
                objNode.Attributes.Append(attribute);
            }

            foreach (var sf in entity.StaticFields)
            {
                var fieldVal = sf.Value;

                //generate the nested XML for entity references
                if (fieldVal is EntityReference<ParaEntity>)
                {
                    var entRef = fieldVal as EntityReference<ParaEntity>;
                    var entityRefType = entRef.Entity.GetType().ToString();
                    XMLGenerateComplexEntityNode(doc, objNode, sf.Name, entityRefType, "id", entRef.Entity.Id.ToString());
                }

                //for simple types, a tostring suffices
                if (fieldVal is Int32
                    || fieldVal is Int64
                    || fieldVal is string
                    || fieldVal is bool)
                {
                    XMLGenerateElement(doc, objNode, sf.Name, sf.Value.ToString());
                }
            }

            ObjectGenerateCustomFieldsXML(doc, objNode, entity.CustomFields);
            doc.AppendChild(objNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the account object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(Account obj)
        {
            // TODO viewable accounts?
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Account");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }


            if (string.IsNullOrEmpty(obj.Account_Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Account_Name", obj.Account_Name);
            }
            if (obj.Sla.Sla.Id > 0)
            {
                XMLGenerateEntityNode(doc, ObjNode, "Sla", "id", obj.Sla.Sla.Id.ToString());
            }

            if (obj.Viewable_Account.Count > 0)
            {
                XmlNode node = doc.CreateElement("Shown_Accounts");
                foreach (var vAccount in obj.Viewable_Account)
                {
                    XmlElement nodechild = doc.CreateElement("Account");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = vAccount.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }


            if (obj.Default_Customer_Role.Role.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Default_Customer_Role", "CustomerRole", "id", obj.Default_Customer_Role.Role.Id.ToString());
            }

            if (obj.CustomFields.Any())
            {
                ObjectGenerateCustomFieldsXML(doc, ObjNode, obj.CustomFields);
            }
            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the Contact object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(Customer obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Customer");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            if (string.IsNullOrEmpty(obj.First_Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "First_Name", obj.First_Name);
            }
            if (string.IsNullOrEmpty(obj.Last_Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Last_Name", obj.Last_Name);
            }
            if (string.IsNullOrEmpty(obj.Email) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Email", obj.Email);
            }
            if (string.IsNullOrEmpty(obj.User_Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "User_Name", obj.User_Name);
            }

            if (string.IsNullOrEmpty(obj.Password) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Password", obj.Password);
            }
            if (string.IsNullOrEmpty(obj.Password_Confirm) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Password_Confirm", obj.Password_Confirm);
            }
            else if (string.IsNullOrEmpty(obj.Password) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Password_Confirm", obj.Password);
            }

            if (obj.Sla.Sla.Id > 0)
            {
                XMLGenerateEntityNode(doc, ObjNode, "Sla", "id", obj.Sla.Sla.Id.ToString());
            }

            if (obj.Customer_Role.Role.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Customer_Role", "CustomerRole", "id", obj.Customer_Role.Role.Id.ToString());
            }

            if (obj.Account.Entity.Id > 0)
            {
                XMLGenerateEntityNode(doc, ObjNode, "Account", "id", obj.Account.Entity.Id.ToString());
            }

            if (obj.Status.Status.Id > 0)
            {
                XMLGenerateEntityNode(doc, ObjNode, "Status", "id", obj.Status.Status.Id.ToString());
            }

            ObjectGenerateCustomFieldsXML(doc, ObjNode, obj.CustomFields);

            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the Asset object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(Asset obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Asset");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            if (string.IsNullOrEmpty(obj.Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Name", obj.Name);
            }

            if (obj.Status.Status.Id > 0)
            {
                XMLGenerateEntityNode(doc, ObjNode, "Status", "id", obj.Status.Status.Id.ToString());
            }

            if (obj.Customer_Owner != null && obj.Customer_Owner.Entity.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Customer_Owner", "Customer", "id", obj.Customer_Owner.Entity.Id.ToString());
            }
            else if (obj.Account_Owner!= null && obj.Account_Owner.Entity.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Account_Owner", "Account", "id", obj.Account_Owner.Entity.Id.ToString());
            }

            if (obj.Product.Entity.Id > 0)
            {
                XMLGenerateEntityNode(doc, ObjNode, "Product", "id", obj.Product.Entity.Id.ToString());
            }
            if (string.IsNullOrEmpty(obj.Serial_Number) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Serial_Number", obj.Serial_Number);
            }
            ObjectGenerateCustomFieldsXML(doc, ObjNode, obj.CustomFields);
            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the Ticket object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(Ticket obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Ticket");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            if (obj.Cc_Csr != null)
            {
                XMLGenerateElementFromArray(doc, ObjNode, "Cc_Csr", new ArrayList() { obj.Cc_Csr }, ",");
            }
            if (obj.Cc_Customer != null)
            {
                XMLGenerateElementFromArray(doc, ObjNode, "Cc_Customer", new ArrayList() { obj.Cc_Customer }, ",");
            }

            if (obj.Ticket_Product != null && obj.Ticket_Product.Entity.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Ticket_Product", "Product", "id", obj.Ticket_Product.Entity.Id.ToString());
            }

            if (obj.Ticket_Asset != null && obj.Ticket_Asset.Entity.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Ticket_Asset", "Asset", "id", obj.Ticket_Asset.Entity.Id.ToString());
            }

            if (obj.Ticket_Sla != null && obj.Ticket_Sla.Sla.Id > 0)
            {
                XMLGenerateComplexEntityNode(doc, ObjNode, "Ticket_Sla", "Sla", "id", obj.Ticket_Sla.Sla.Id.ToString());
            }

            if (obj.Email_Notification != null)
            {
                XMLGenerateElement(doc, ObjNode, "Email_Notification", obj.Email_Notification.ToString().ToLower());
            }

            // DJERAME
            if (obj.Email_Notification_Additional_Contact != null)
            {
                XMLGenerateElement(doc, ObjNode, "Email_Notification_Additional_Contact", obj.Email_Notification_Additional_Contact.ToString().ToLower());
            }

            if (obj.Ticket_Customer != null)
            {
                if (obj.Ticket_Customer.Entity.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Ticket_Customer", "Customer", "id", obj.Ticket_Customer.Entity.Id.ToString());
                }
            }

            if (obj.Hide_From_Customer != null)
            {
                XMLGenerateElement(doc, ObjNode, "Hide_From_Customer", obj.Hide_From_Customer.ToString().ToLower());
            }

            // DJERAME
            if (obj.Additional_Contact != null)
            {
                if (obj.Additional_Contact.Entity.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Additional_Contact", "Customer", "id", obj.Additional_Contact.Entity.Id.ToString());
                }
            }

            if (obj.Department != null)
            {
                if (obj.Department.Department.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Department", "Department", "id", obj.Department.Department.Id.ToString());
                }
            }

            if (obj.Ticket_Parent != null)
            {
                //Trying to avoid passing the credentials class to pass the account id with the 
                //parent ticket number. will have to add the account number if that does not work.
                if (obj.Ticket_Parent.Entity.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Ticket_Parent", "Ticket", "id", obj.Ticket_Parent.Entity.Id.ToString());
                }
            }
            //Sending back the Child tickets XML.
            if (obj.Ticket_Children != null)
            {
                if (obj.Ticket_Children.Count > 0)
                {
                    XmlNode mainnode = doc.CreateElement("Ticket_Children");
                    foreach (ParaObjects.Ticket tc in obj.Ticket_Children)
                    {
                        XmlElement mainnodechild = doc.CreateElement("Ticket");
                        XmlAttribute attribute = doc.CreateAttribute("id");
                        attribute.Value = tc.Id.ToString();
                        mainnodechild.Attributes.Append(attribute);
                        mainnode.AppendChild(mainnodechild);
                    }
                    ObjNode.AppendChild(mainnode);
                }
            }
            if (obj.Ticket_Attachments != null && obj.Ticket_Attachments.Count > 0)
            {
                ObjectGenerateAttachmentNodes(doc, ObjNode, "Ticket_Attachments", "Attachment", obj.Ticket_Attachments);
            }

            ObjectGenerateCustomFieldsXML(doc, ObjNode, obj.CustomFields);
            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// Generate the XML needed to run an action.
        /// </summary>
        static public XmlDocument GenerateXml(Action obj, ParaEnums.ParatureModule module)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Action");
            XmlNode ActionWrapperNode = doc.CreateElement(module.ToString());
            XmlNode ActionNode = doc.CreateElement("Action");


            XmlAttribute attribute = doc.CreateAttribute("id");
            attribute.Value = obj.Id.ToString();

            ObjNode.Attributes.Append(attribute);

            if (obj.actionType != ParaEnums.ActionType.Grab)
            {

                if (obj.TimeSpentHours > 0)
                {
                    XMLGenerateElement(doc, ObjNode, "TimeSpentHours", obj.TimeSpentHours.ToString());
                }

                if (obj.TimeSpentMinutes > 0)
                {
                    XMLGenerateElement(doc, ObjNode, "TimeSpentMinutes", obj.TimeSpentMinutes.ToString());
                }

                if (obj.EmailListCustomers != null)
                {
                    if (obj.EmailListCustomers.Count > 0)
                    {
                        XMLGenerateElementFromArray(doc, ObjNode, "Emailcustlist", obj.EmailListCustomers, ",");
                    }
                }
                if (obj.EmailListCsr != null)
                {
                    if (obj.EmailListCsr.Count > 0)
                    {
                        XMLGenerateElementFromArray(doc, ObjNode, "EmailCsrList", obj.EmailListCsr, ",");
                    }
                }

                if (module == ParaEnums.ParatureModule.Ticket)
                {
                    XMLGenerateElement(doc, ObjNode, "ShowToCust", obj.VisibleToCustomer.ToString().ToLower());
                }


                if (obj.Comment != null)
                {
                    if (string.IsNullOrEmpty(obj.Comment) == false)
                    {
                        XMLGenerateElement(doc, ObjNode, "Comment", obj.Comment);
                    }
                }
                if (obj.EmailText != null)
                {
                    if (string.IsNullOrEmpty(obj.EmailText) == false)
                    {
                        XMLGenerateElement(doc, ObjNode, "Emailtext", obj.EmailText);
                    }
                }
                //if (obj.Action_Attachments.Count > 0)
                //{
                //    ObjectGenerateAttachmentNodes(doc, ObjNode, "Attachments", "Attachment", obj.Action_Attachments);
                //}
            }

            switch (obj.actionType)
            {
                case ParaEnums.ActionType.Assign_Queue:
                    XMLGenerateElement(doc, ObjNode, "AssignToQueue", obj.AssignToQueueid.ToString());
                    break;
                case ParaEnums.ActionType.Assign:
                    XMLGenerateElement(doc, ObjNode, "AssignToCsr", obj.AssigntToCsrid.ToString());
                    break;
            }


            ActionNode.AppendChild(ObjNode);
            ActionWrapperNode.AppendChild(ActionNode);
            doc.AppendChild(ActionWrapperNode);
            return doc;
        }

        /// <summary>
        /// Generate the XML needed to create/update a product.
        /// </summary>
        static public XmlDocument GenerateXml(Product obj)
        {
            XmlDocument doc = new XmlDocument();
            //XmlNode declarationNode = doc.CreateXmlDeclaration("1.0", "", "");
            //doc.AppendChild(declarationNode);
            XmlNode ObjNode = doc.CreateElement("Product");


            if (string.IsNullOrEmpty(obj.Currency) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Currency", obj.Currency.ToString());
            }

            //////////////////////
            // Not sure how to handle this, as a bool will never be null..
            if (obj.InStock != null)
            {
                //XMLGenerateElement(doc, ObjNode, "Instock", obj.Instock.ToString().ToLower());
            }
            if (string.IsNullOrEmpty(obj.Longdesc) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Longdesc", obj.Longdesc.ToString());
            }
            if (string.IsNullOrEmpty(obj.Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Name", obj.Name.ToString());
            }
            if (string.IsNullOrEmpty(obj.Shortdesc) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Shortdesc", obj.Shortdesc.ToString());
            }
            if (string.IsNullOrEmpty(obj.Price) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Price", obj.Price.ToString());
            }
            if (string.IsNullOrEmpty(obj.Sku) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Sku", obj.Sku.ToString());
            }
            if (obj.Visible != null)
            {
                XMLGenerateElement(doc, ObjNode, "Visible", obj.Visible.ToString().ToLower());
            }
            if (obj.InStock != null)
            {
                XMLGenerateElement(doc, ObjNode, "Instock", obj.InStock.ToString().ToLower());
            }

            //XMLGenerateComplexEntityNode
            if (obj.Folder != null)
            {
                if (obj.Folder.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Folder", "ProductFolder", "id", obj.Folder.Id.ToString());
                }
            }

            ObjectGenerateCustomFieldsXML(doc, ObjNode, obj.CustomFields);



            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// Generate the XML needed to create/update a Knowledge Base Article.
        /// </summary>
        static public XmlDocument GenerateXml(Article obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Article");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            if (obj.Permissions.Count > 0)
            {
                XmlNode node = doc.CreateElement("Permissions");
                foreach (Sla sla in obj.Permissions)
                {
                    XmlElement nodechild = doc.CreateElement("Sla");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = sla.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }

            if (obj.Products.Count > 0)
            {
                XmlNode node = doc.CreateElement("Products");
                foreach (ParaObjects.Product product in obj.Products)
                {
                    XmlElement nodechild = doc.CreateElement("Product");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = product.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }
            if (obj.Published != null)
            {
                XMLGenerateElement(doc, ObjNode, "Published", obj.Published.ToString().ToLower());
            }
            if (string.IsNullOrEmpty(obj.Question) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Question", obj.Question);
            }
            if (string.IsNullOrEmpty(obj.Answer) == false)
            {
                XMLGenerateCDataElement(doc, ObjNode, "Answer", obj.Answer);
            }
            if (obj.Expiration_Date != DateTime.MinValue)
            {
                XMLGenerateElement(doc, ObjNode, "Expiration_Date", obj.Expiration_Date.ToString());
            }

            if (obj.Folders.Count > 0)
            {
                XmlNode node = doc.CreateElement("Folders");
                foreach (Folder folder in obj.Folders)
                {
                    XmlElement nodechild = doc.CreateElement("ArticleFolder");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = folder.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }

            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// Generate the XML needed to create/update a Csr.
        /// </summary>
        static public XmlDocument GenerateXml(Csr obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Csr");

            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            #region Entities

            if (obj.Role.Count > 0)
            {
                XmlNode node = doc.CreateElement("Role");
                foreach (Role role in obj.Role)
                {
                    XmlElement nodechild = doc.CreateElement("CsrRole");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = role.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }

            if (obj.Status != null)
            {
                if (obj.Status.Status.Id != 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Status", "Status", "id", obj.Status.Status.Id.ToString());
                }
            }

            if (obj.Timezone != null)
            {
                if (obj.Timezone.Timezone.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Timezone", "Timezone", "id", obj.Timezone.Timezone.Id.ToString());
                }
            }
            #endregion
            #region Strings
            if (string.IsNullOrEmpty(obj.Date_Format) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Date_Format", obj.Date_Format);
            }
            if (string.IsNullOrEmpty(obj.Email) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Email", obj.Email);
            }
            if (string.IsNullOrEmpty(obj.Fax) == false)
            {
                XMLGenerateCDataElement(doc, ObjNode, "Fax", obj.Fax);
            }
            if (string.IsNullOrEmpty(obj.Full_Name) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Full_Name", obj.Full_Name);
            }
            if (string.IsNullOrEmpty(obj.Password) == false)
            {
                // XMLGenerateCDataElement(doc, ObjNode, "Password", obj.Password);
                XMLGenerateElement(doc, ObjNode, "Password", obj.Password);
            }
            if (string.IsNullOrEmpty(obj.Phone_1) == false)
            {
                XMLGenerateCDataElement(doc, ObjNode, "Phone_1", obj.Phone_1);
            }
            if (string.IsNullOrEmpty(obj.Phone_2) == false)
            {
                XMLGenerateCDataElement(doc, ObjNode, "Phone_2", obj.Phone_2);
            }
            if (string.IsNullOrEmpty(obj.Screen_Name) == false)
            {
                // XMLGenerateCDataElement(doc, ObjNode, "Screen_Name", obj.Screen_Name);
                XMLGenerateElement(doc, ObjNode, "Screen_Name", obj.Screen_Name);
            }
            #endregion
            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the Download object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(Download obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("Download");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            if (obj.Published != null)
            {
                XMLGenerateElement(doc, ObjNode, "Published", obj.Published.ToString().ToLower());
            }
            if (string.IsNullOrEmpty(obj.Description) == false)
            {
                XMLGenerateCDataElement(doc, ObjNode, "Description", obj.Description);
                //XMLGenerateElement(doc, ObjNode, "Description", obj.Description.ToString());
            }
            if (string.IsNullOrEmpty(obj.Guid) == false)
            {
                XMLGenerateElement(doc, ObjNode, "Guid", obj.Guid.ToString());
            }
            else if (string.IsNullOrEmpty(obj.External_Link) == false)
            {
                XMLGenerateElement(doc, ObjNode, "External_Link", obj.External_Link.ToString());
            }

            if (obj.Visible != null)
            {
                XMLGenerateElement(doc, ObjNode, "Visible", obj.Visible.ToString().ToLower());
            }
            if (!string.IsNullOrEmpty(obj.Name))
            {
                XMLGenerateElement(doc, ObjNode, "Name", obj.Name);
            }
            if (!string.IsNullOrEmpty(obj.Title))
            {
                XMLGenerateElement(doc, ObjNode, "Title", obj.Title);
            }
          
            if (obj.Folders != null && obj.Folders.Count > 0)
            {
                if (!obj.MultipleFolders && obj.Folders.Count > 1)
                {
                    throw new ArgumentOutOfRangeException("Folders","There are too many folders for this Download. MultipleFolders is set to false.");
                }

                //Need to handle multiple folders
                XmlNode node;
                if (obj.MultipleFolders)
                {
                    node = doc.CreateElement("Folders");
                    foreach (Folder folder in obj.Folders)
                    {
                        XmlElement nodechild = doc.CreateElement("DownloadFolder");
                        XmlAttribute attribute = doc.CreateAttribute("id");
                        attribute.Value = folder.Id.ToString();
                        nodechild.Attributes.Append(attribute);
                        node.AppendChild(nodechild);
                    }
                }
                else
                {
                    node = doc.CreateElement("Folder");
                    var folder = obj.Folders.FirstOrDefault();
                    XmlElement nodechild = doc.CreateElement("DownloadFolder");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = folder.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }

                ObjNode.AppendChild(node);
            }

            if (obj.Permissions.Count > 0)
            {
                XmlNode node = doc.CreateElement("Permissions");
                foreach (Sla sla in obj.Permissions)
                {
                    XmlElement nodechild = doc.CreateElement("Sla");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = sla.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }

            if (obj.Products.Count > 0)
            {
                XmlNode node = doc.CreateElement("Products");
                foreach (ParaObjects.Product product in obj.Products)
                {
                    XmlElement nodechild = doc.CreateElement("Product");
                    XmlAttribute attribute = doc.CreateAttribute("id");
                    attribute.Value = product.Id.ToString();
                    nodechild.Attributes.Append(attribute);
                    node.AppendChild(nodechild);
                }
                ObjNode.AppendChild(node);
            }


            if (obj.Eula != null)
            {
                if (obj.Eula.Id > 0)
                {
                    XMLGenerateComplexEntityNode(doc, ObjNode, "Eula", "Eula", "id", obj.Eula.Id.ToString());
                }
            }

            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the DownloadFolder object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(DownloadFolder obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("DownloadFolder");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            XMLGenerateElement(doc, ObjNode, "Is_Private", obj.Is_Private.ToString().ToLower());
            XMLGenerateElement(doc, ObjNode, "Name", obj.Name.ToString());
            XMLGenerateElement(doc, ObjNode, "Description", obj.Description.ToString());

            XMLGenerateComplexEntityNode(doc, ObjNode, "Parent_Folder", "DownloadFolder", "id", obj.Parent_Folder.Id.ToString());
            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the ArticleFolder object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(ArticleFolder obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("ArticleFolder");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            XMLGenerateElement(doc, ObjNode, "Name", obj.Name.ToString());
            XMLGenerateElement(doc, ObjNode, "Is_Private", obj.Is_Private.ToString().ToLower());
            //if (obj.Parent_Folder != null)

            XMLGenerateComplexEntityNode(doc, ObjNode, "Parent_Folder", "ArticleFolder", "id", obj.Parent_Folder.Id.ToString());
            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// This methods requires the ProductFolder object to be inserted/updated, and returns the XML to be posted to the APIs Server
        /// </summary>
        static public XmlDocument GenerateXml(ProductFolder obj)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode ObjNode = doc.CreateElement("ProductFolder");
            if (obj.Id > 0)
            {
                XmlAttribute attribute = doc.CreateAttribute("id");
                attribute.Value = obj.Id.ToString();
                ObjNode.Attributes.Append(attribute);
            }

            XMLGenerateElement(doc, ObjNode, "Is_Private", obj.Is_Private.ToString().ToLower());
            XMLGenerateElement(doc, ObjNode, "Name", obj.Name.ToString());
            XMLGenerateElement(doc, ObjNode, "Description", obj.Description.ToString());
            XMLGenerateComplexEntityNode(doc, ObjNode, "Parent_Folder", "ProductFolder", "id", obj.Parent_Folder.Id.ToString());

            doc.AppendChild(ObjNode);
            return doc;
        }

        /// <summary>
        /// An internal method that generates a node and apprend it to the xmldocument root element passed to it.
        /// </summary>       
        static void XMLGenerateElement(XmlDocument doc, XmlNode ObjNode, string nodename, string nodevalue)
        {
            XmlNode node = doc.CreateElement(nodename);
            node.InnerText = nodevalue;
            ObjNode.AppendChild(node);
        }

        /// <summary>
        /// An internal method that generates a CData node and apprend it to the xmldocument root element passed to it.
        /// </summary>       
        static void XMLGenerateCDataElement(XmlDocument doc, XmlNode ObjNode, string nodename, string nodevalue)
        {
            if (nodevalue.Contains("]]>"))
            {
                XMLGenerateElement(doc, ObjNode, nodename, nodevalue);
            }
            else
            {
                XmlCDataSection CData = doc.CreateCDataSection(nodevalue);
                XmlNode node = doc.CreateElement(nodename);
                node.AppendChild(CData);
                ObjNode.AppendChild(node);
            }
        }


        /// <summary>
        /// An internal method that generates a node from a string array and apprend it to the xmldocument root element passed to it.
        /// </summary>
        static void XMLGenerateElementFromArray(XmlDocument doc, XmlNode ObjNode, string nodename, ArrayList nodevalue, string Separator)
        {
            XmlNode node = doc.CreateElement(nodename);
            string value = "";
            bool lastvalue = false;

            for (int i = 0; i < nodevalue.Count; i++)
            {
                lastvalue = false;
                if (i == nodevalue.Count - 1)
                {
                    lastvalue = true;
                }
                if (lastvalue == false)
                {
                    value = value + nodevalue[i].ToString() + Separator;
                }
                else
                {
                    value = value + nodevalue[i].ToString();
                }
            }

            node.InnerText = value;
            ObjNode.AppendChild(node);

        }

        /// <summary>
        /// An internal method that generates a complex entity node with an external name and an internal element name to apprend it to the xmldocument root element passed to it.
        /// </summary>
        static void XMLGenerateComplexEntityNode(XmlDocument doc, XmlNode ObjNode, string Externalnodename, string Internalnodename, string attributeName, string attributeValue)
        {

            XmlNode node = doc.CreateElement(Externalnodename);
            XmlElement nodechild = doc.CreateElement(Internalnodename);

            XmlAttribute attribute = doc.CreateAttribute(attributeName);
            attribute.Value = attributeValue;

            nodechild.Attributes.Append(attribute);
            node.AppendChild(nodechild);
            ObjNode.AppendChild(node);

        }

        /// <summary>
        /// Generates the whole attachments node that contains the attachment collection details.
        /// Useful for objects that may contain multiple attachments (ticket, action, etc).
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="ObjNode"></param>
        /// <param name="ExternalAttachmentNodeName">
        /// The external node name for the attachment collection. For example: "Ticket_Attachment" for ticket attachments.
        /// </param>
        /// <param name="AttachmentNodeName">
        /// This would be the node name for each attachment in the collection.
        /// </param>
        /// <param name="Attachments"></param>
        static void ObjectGenerateAttachmentNodes(XmlDocument doc, XmlNode ObjNode, string ExternalAttachmentNodeName, string AttachmentNodeName, List<Attachment> Attachments)
        {
            XmlNode node = doc.CreateElement(ExternalAttachmentNodeName);
            foreach (Attachment at in Attachments)
            {
                ObjectGenerateAttachmentNode(doc, node, AttachmentNodeName, at);
            }
            ObjNode.AppendChild(node);
        }

        /// <summary>
        /// Appends a single attachment node.
        /// </summary>
        static void ObjectGenerateAttachmentNode(XmlDocument doc, XmlNode ObjNode, string AttachmentNodeName, Attachment Attachment)
        {
            XmlNode node = doc.CreateElement(AttachmentNodeName);

            XmlNode guid = doc.CreateElement("Guid");
            guid.InnerText = Attachment.Guid.ToString();
            XmlNode name = doc.CreateElement("Name");
            name.InnerText = Attachment.Name.ToString();
            node.AppendChild(guid);
            node.AppendChild(name);
            ObjNode.AppendChild(node);
        }


        /// <summary>
        /// An internal method that generates an entity node and apprend it to the xmldocument root element passed to it.
        /// </summary>
        static void XMLGenerateEntityNode(XmlDocument doc, XmlNode ObjNode, string nodename, string attributeName, string attributeValue)
        {
            XmlNode node = doc.CreateElement(nodename);
            XmlElement nodechild = doc.CreateElement(nodename);

            XmlAttribute attribute = doc.CreateAttribute(attributeName);
            attribute.Value = attributeValue;

            nodechild.Attributes.Append(attribute);
            node.AppendChild(nodechild);
            ObjNode.AppendChild(node);
        }

        /// <summary>
        /// Loops through the custom fields and prepares (then appends) the whole XML portion dealing with them.
        /// </summary>
        static void ObjectGenerateCustomFieldsXML(XmlDocument doc, XmlNode ObjNode, IEnumerable<CustomField> customFields)
        {
            foreach (CustomField cf in customFields)
            {
                XmlNode cfnode = null;
                cfnode = doc.CreateElement("Custom_Field");
                XmlAttribute attid = doc.CreateAttribute("id");
                attid.Value = cf.Id.ToString();
                cfnode.Attributes.Append(attid);

                bool hascustomfields = false;

                int cfocount = cf.CustomFieldOptionsCollection.Count;

                if (cfocount > 0)
                {
                    bool haschild = false;

                    foreach (CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                    {
                        XmlNode cfonode = doc.CreateElement("Option");
                        XmlAttribute cfoattid = doc.CreateAttribute("id");
                        cfoattid.Value = cfo.Id.ToString();
                        cfonode.Attributes.Append(cfoattid);

                        if (cfo.Selected == true)
                        {
                            XmlAttribute attSel = doc.CreateAttribute("selected");
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
                        switch (cf.FieldDataType)
                        {
                            case ParaEnums.FieldDataType.Date:
                            {
                                // In case the date time bug is fixed, we are stripping the 
                                // extra "z" at the end of the date, and formatting the date 
                                // while adding back the "z".
                                cfnode.InnerText = cf.GetFieldValue<DateTime>().ToString("yyyy-MM-ddTHH:mm:ssZ");
                            }
                                break;
                            case ParaEnums.FieldDataType.Boolean:
                                cfnode.InnerText = cf.GetFieldValue<bool>().ToString();
                                break;
                            case ParaEnums.FieldDataType.ReadOnly:
                            case ParaEnums.FieldDataType.Unknown:
                                hascustomfields = false;
                                break;
                            //default to string
                            case ParaEnums.FieldDataType.String:
                            default:
                                cfnode.InnerText = cf.Value.ToString();
                                break;
                        }
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
                    ObjNode.AppendChild(cfnode);
                }
            }
        }
    }
}
