using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;

namespace ParatureAPI
{
    /// <summary>
    /// All the objects you will need while interacting with your Parature solution.
    /// </summary>
    public class ParaObjects
    {
        /// <summary>
        /// Provides basic shared properties among all the objects.
        /// </summary>
        public abstract partial class objectBaseProperties
        {
            /// <summary>
            /// Indicates whether the object is fully loaded or not. If the object is returned as a second level object, this flag will indicate whether only the id property of the object is filled, or if all the properties have been loaded.
            /// </summary>
            public bool FullyLoaded;
            /// <summary>
            /// Contains all the information regarding the API Call that was made.
            /// </summary>
            public ApiCallResponse ApiCallResponse = new ApiCallResponse();

            /// <summary>
            /// The unique identifier of this object. This is mainly used to standardize the integration process for the 
            /// Parature Technical Services Team.
            /// </summary>
            public Int64 uniqueIdentifier;

            public string serviceDeskUri;

            public string uid;

            //public ObjectType type = ObjectType.Custom;

            private bool _isDirty = false;

            /// <summary>
            /// Indicate whether the object is dirty or not (means it needs to be updated, created or deleted).
            /// </summary>
            public bool IsDirty
            {
                get { return _isDirty; }
                set { _isDirty = value; }
            }

            public objectBaseProperties()
            {
            }

            public objectBaseProperties(objectBaseProperties objBP)
            {
                this.FullyLoaded = objBP.FullyLoaded;
                this.ApiCallResponse = new ParaObjects.ApiCallResponse(objBP.ApiCallResponse);
                this.uniqueIdentifier = objBP.uniqueIdentifier;
                this.IsDirty = objBP.IsDirty;
                this.uid = objBP.uid;
            }

            /// <summary>
            /// Manages the "IsDirty" flag property of the object.
            /// </summary>
            /// <param name="isModified">Input Parameter, to indicate whether a change happened to the object or not</param>
            protected bool DirtyStateManager(bool isModified)
            {
                if (isModified)
                {
                    _isDirty = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public Paraenums.Operation operation = Paraenums.Operation.Ignore;
        }

        /// <summary>
        /// Designed to provide properties for paged results.
        /// </summary>
        public abstract class PagedData
        {
            /// <summary>
            /// Total number of items that matched your request
            /// </summary>
            public int TotalItems = 0;
            /// <summary>
            /// The number of items returned with the current call.
            /// </summary>
            public int ResultsReturned = 0;
            /// <summary>
            /// The maximum number of items returned per call.
            /// </summary>
            public int PageSize = 0;
            /// <summary>
            /// Number of this page
            /// </summary>
            public int PageNumber = 1;
            /// <summary>
            /// Contains all the information regarding the API Call that was made.
            /// </summary>
            public ApiCallResponse ApiCallResponse = new ApiCallResponse();

            public PagedData()
            {
            }

            public PagedData(PagedData pagedData)
            {
                this.TotalItems = pagedData.TotalItems;
                this.ResultsReturned = pagedData.ResultsReturned;
                this.PageSize = pagedData.PageSize;
                this.PageNumber = pagedData.PageNumber;
                this.ApiCallResponse = new ApiCallResponse(pagedData.ApiCallResponse);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Tickets
        /// </summary>
        public partial class TicketsList : PagedData
        {
            /// <summary>
            /// The collection of Tickets objects returned.
            /// </summary>
            public List<ParaObjects.Ticket> Tickets = new List<ParaObjects.Ticket>();

            public TicketsList()
            {
            }

            public TicketsList(TicketsList ticketsList)
                : base(ticketsList)
            {
                this.Tickets = new List<Ticket>(ticketsList.Tickets);
            }

        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Accounts
        /// </summary>
        public partial class AccountsList : PagedData
        {
            public List<ParaObjects.Account> Accounts = new List<ParaObjects.Account>();

            public AccountsList()
            {
            }

            public AccountsList(AccountsList accountsList)
                : base(accountsList)
            {
                this.Accounts = new List<ParaObjects.Account>(accountsList.Accounts);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Customers
        /// </summary>
        public partial class CustomersList : PagedData
        {
            public List<ParaObjects.Customer> Customers = new List<ParaObjects.Customer>();

            public CustomersList()
            {
            }

            public CustomersList(CustomersList customersList)
                : base(customersList)
            {
                this.Customers = new List<Customer>(customersList.Customers);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Products
        /// </summary>
        public partial class ProductsList : PagedData
        {
            public List<ParaObjects.Product> Products = new List<ParaObjects.Product>();

            public ProductsList()
            {
            }

            public ProductsList(ProductsList productsList)
                : base(productsList)
            {
                this.Products = new List<Product>(productsList.Products);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Assets
        /// </summary>
        public partial class AssetsList : PagedData
        {
            public List<ParaObjects.Asset> Assets = new List<ParaObjects.Asset>();

            public AssetsList()
            {
            }

            public AssetsList(AssetsList assetsList)
                : base(assetsList)
            {
                this.Assets = new List<Asset>(assetsList.Assets);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Knowledge base articles
        /// </summary>
        public partial class ArticlesList : PagedData
        {
            public List<ParaObjects.Article> Articles = new List<ParaObjects.Article>();

            public ArticlesList()
            {
            }

            public ArticlesList(ArticlesList articlesList)
                : base(articlesList)
            {
                this.Articles = new List<Article>(articlesList.Articles);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Downloads
        /// </summary>
        public partial class DownloadsList : PagedData
        {
            public List<ParaObjects.Download> Downloads = new List<ParaObjects.Download>();

            public DownloadsList()
            {
            }

            public DownloadsList(DownloadsList downloadsList)
                : base(downloadsList)
            {
                this.Downloads = new List<Download>(downloadsList.Downloads);
            }
        }

        /// <summary>
        /// Instantiate this class to hold the result set of a list call to APIs. Whenever you need to get a list of 
        /// Chats
        /// </summary>
        public partial class ChatList : PagedData
        {
            public List<ParaObjects.Chat> chats = new List<ParaObjects.Chat>();

            public ChatList()
            {
            }
        }

        public abstract partial class ModuleWithCustomFields : objectBaseProperties
        {
            public ModuleWithCustomFields()
                : base()
            {

            }

            public ModuleWithCustomFields(ModuleWithCustomFields moduleWithCustomFields)
                : base(moduleWithCustomFields)
            {
                if (moduleWithCustomFields != null && moduleWithCustomFields.CustomFields != null)
                {
                    this.CustomFields = new List<CustomField>();

                    foreach (CustomField cf in moduleWithCustomFields.CustomFields)
                    {
                        this.CustomFields.Add(new CustomField(cf));
                    }
                }
            }

            /// <summary>
            /// The collection of custom fields for this module
            /// </summary>
            public List<ParaObjects.CustomField> CustomFields = new List<ParaObjects.CustomField>();

            /// <summary>
            /// This method accepts a custom field id and will reset all of its values: if this custom field has any options, they will be 
            /// all unselected. If the custom field value is not set to "", this method will set it. Basically, the field will be empty.            /// 
            /// </summary>           
            /// <returns>Returns True if the custom field was modified, False if there was no need to modify the custom field.</returns>
            public bool CustomFieldReset(Int64 CustomFieldid)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldReset(CustomFieldid, CustomFields));
            }

            /// <summary>
            /// Sets the value of a custom field in the fields collection of this object. If there is no custom field with the 
            /// id that you pass to this method, a new custom field will be created and its value will be set to the one 
            /// passed to this method. Set the Ignore case to indicate whether the comparison should take into account the case or not.
            /// </summary>
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            ///</returns>
            public bool CustomFieldSetValue(Int64 CustomFieldid, string CustomFieldValue, bool ignoreCase)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetValue(CustomFieldid, CustomFieldValue, CustomFields, ignoreCase));
            }

            /// <summary>
            /// Sets the value of a custom field in the fields collection of this object. If there is no custom field with the 
            /// id that you pass to this method, a new custom field will be created and its value will be set to the one 
            /// passed to this method.
            /// </summary>
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            ///</returns>
            public bool CustomFieldSetValue(Int64 CustomFieldid, string CustomFieldValue)
            {
                return CustomFieldSetValue(CustomFieldid, CustomFieldValue, true);
            }

            /// <summary>
            /// Sets the value of a custom field in the fields collection of this object. If there is no custom field with the 
            /// id that you pass to this method, a new custom field will be created and its value will be set to the one 
            /// passed to this method.
            /// </summary>
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            ///</returns>
            public bool CustomFieldSetValue(Int64 CustomFieldid, bool CustomFieldValue)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetValue(CustomFieldid, CustomFieldValue.ToString().ToLower(), CustomFields, true));
            }


            /// <summary>
            /// Sets the value of a custom field in the fields collection of this object. If there is no custom field with the 
            /// id that you pass to this method, a new custom field will be created and its value will be set to the one 
            /// passed to this method.
            /// </summary>
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            ///</returns>
            public bool CustomFieldSetValue(Int64 CustomFieldid, DateTime CustomFieldValue)
            {
                string date = CustomFieldValue.ToString("yyyy-MM-ddTHH:mm:ss");
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetValue(CustomFieldid, date, CustomFields, true));
            }

            /// <summary>
            /// Will reset the value of the custom field with id you pass to this method, and then will make 
            /// sure to send the custom field back with an empty value so that it deletes the value stored in Parature 
            /// for this custom field.
            /// </summary>
            public void CustomFieldFlagToDelete(Int64 CustomFieldid)
            {
                if (CustomFieldid > 0)
                {
                    List<ParaObjects.CustomField> fields = new List<CustomField>();
                    foreach (ParaObjects.CustomField cf in CustomFields)
                    {
                        if (cf.CustomFieldID == CustomFieldid)
                        {
                            fields.Add(cf);
                            DirtyStateManager(ParaHelper.HelperMethods.CustomFieldReset(CustomFieldid, fields));
                            cf.FlagToDelete = true;
                            break;
                        }
                    }
                }
            }

            /// <summary>
            /// Look for a custom field with the id passed and then set the selected option to the CustomFieldOptionid passed.
            /// If any other custom field option is selected, it will be unselected. If the option you need to select is not part 
            /// of the custom field options, it will add it. Finally, if a custom field with the id you are passing does not exist, it will create one 
            /// with the proper custom field option you need.
            /// </summary>          
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            /// </returns>
            public bool CustomFieldSetSelectedFieldOption(Int64 CustomFieldid, Int64 CustomFieldOptionid)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetFieldOption(CustomFieldid, CustomFieldOptionid, CustomFields, true));
            }

            /// <summary>
            /// Look for a custom field with the id passed and then set the selected option to the CustomFieldOption name passed.
            /// If any other custom field option is selected, it will be unselected. If the option you need to select is not part 
            /// of the custom field options, it will add it. Finally, if a custom field with the id you are passing does not exist, it will create one 
            /// with the proper custom field option you need.
            /// </summary>          
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            /// </returns>
            public bool CustomFieldSetSelectedFieldOption(Int64 CustomFieldid, string CustomFieldOptionname, bool ignoreCase)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetFieldOption(CustomFieldid, CustomFieldOptionname, CustomFields, true, ignoreCase));
            }

            /// <summary>
            /// Look for a custom field with the id passed and then set the selected option to the CustomFieldOptionid passed.
            /// If any other custom field option is selected, it will stay selected. If the option you need to select is not part 
            /// of the custom field options, it will add it. Finally, if a custom field with the id you are passing does not exist, it will create one 
            /// with the proper custom field option you need.
            /// </summary>          
            /// <returns>
            /// returns True if the custom field was modified (or created), False if there was no need to modify the custom field.
            /// </returns>
            public bool CustomFieldAddSelectedFieldOption(Int64 CustomFieldid, Int64 CustomFieldOptionid)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetFieldOption(CustomFieldid, CustomFieldOptionid, CustomFields, false));
            }

            /// <summary>
            /// Allows you to add an option to the selected field options already in this custom field.
            /// </summary>
            public bool CustomFieldAddSelectedFieldOption(Int64 CustomFieldid, string CustomFieldOptionname, bool ignoreCase)
            {
                return DirtyStateManager(ParaHelper.HelperMethods.CustomFieldSetFieldOption(CustomFieldid, CustomFieldOptionname, CustomFields, false, ignoreCase));
            }

            /// <summary>
            /// Returns the selected custom field option object for a custom field. Will return the first encountered selected 
            /// field options object.
            /// </summary>           
            public CustomFieldOptions CustomFieldGetSelectedOption(Int64 CustomFieldid)
            {
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldID == CustomFieldid)
                    {
                        foreach (CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                        {
                            if (cfo.IsSelected == true)
                            {
                                return cfo;
                            }
                        }
                    }
                }
                return null;
            }

            /// <summary>
            /// Returns the selected custom field option object for a custom field. Will return the first encountered selected 
            /// field options object.
            /// </summary>  
            public CustomFieldOptions CustomFieldGetSelectedOption(CustomField CustomField)
            {

                foreach (CustomFieldOptions cfo in CustomField.CustomFieldOptionsCollection)
                {
                    if (cfo.IsSelected == true)
                    {
                        return cfo;
                    }
                }

                return null;
            }

            /// <summary>
            /// Returns the selected custom field option object for a custom field. Will return the first encountered selected 
            /// field options object.
            /// </summary>  
            public CustomFieldOptions CustomFieldGetSelectedOption(string CustomFieldName)
            {
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldName == CustomFieldName)
                    {
                        foreach (CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                        {
                            if (cfo.IsSelected == true)
                            {
                                return cfo;
                            }
                        }
                    }
                }
                return null;
            }

            /// <summary>
            /// Returns the list of all selected custom field options objects for a custom field.             
            /// </summary>  
            public List<CustomFieldOptions> CustomFieldGetSelectedOptions(Int64 CustomFieldid)
            {
                List<CustomFieldOptions> SelectedOptions = new List<CustomFieldOptions>();
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldID == CustomFieldid)
                    {
                        foreach (CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                        {
                            if (cfo.IsSelected == true)
                            {
                                SelectedOptions.Add(cfo);
                            }
                        }
                    }
                }
                return SelectedOptions;
            }

            /// <summary>
            /// Returns the list of all custom field options for a custom field.
            /// </summary>
            /// <param name="CustomFieldId"></param>
            /// <returns></returns>
            public List<CustomFieldOptions> CustomFieldGetOptions(Int64 CustomFieldId)
            {
                List<CustomFieldOptions> options = new List<CustomFieldOptions>();
                
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldID == CustomFieldId)
                    {
                        foreach (CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                        {
                            options.Add(cfo);
                        }
                    }
                }

                return options;

            }

            /// <summary>
            /// Returns the list of all selected custom field options objects for a custom field.             
            /// </summary>  
            public List<CustomFieldOptions> CustomFieldGetSelectedOptions(CustomField CustomField)
            {
                List<CustomFieldOptions> SelectedOptions = new List<CustomFieldOptions>();
                foreach (CustomFieldOptions cfo in CustomField.CustomFieldOptionsCollection)
                {
                    if (cfo.IsSelected == true)
                    {
                        SelectedOptions.Add(cfo);
                    }
                }

                return SelectedOptions;
            }

            /// <summary>
            /// Returns the list of all selected custom field options objects for a custom field.             
            /// </summary>  
            public List<CustomFieldOptions> CustomFieldGetSelectedOptions(string CustomFieldName)
            {
                List<CustomFieldOptions> SelectedOptions = new List<CustomFieldOptions>();
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldName == CustomFieldName)
                    {
                        foreach (CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                        {
                            if (cfo.IsSelected == true)
                            {
                                SelectedOptions.Add(cfo);
                            }
                        }
                    }
                }
                return SelectedOptions;
            }

            /// <summary>
            /// Returns the display name for the custom field id you specified.  Will return empty string if
            /// the custom field is not found.
            /// </summary>
            /// <param name="CustomFieldId"></param>
            /// <returns></returns>
            public string CustomFieldGetDisplayName(Int64 CustomFieldId)
            {
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldID == CustomFieldId)
                    {
                        return cf.CustomFieldName;
                    }
                }

                return string.Empty;
            }

            /// <summary>
            ///  Return the id for the custom field name you specified.  Will return -1 if the custom field is not found 
            ///  or duplicates are found.  Search will be case-insensitive.
            /// </summary>
            /// <param name="CustomFieldName"></param>
            /// <returns></returns>
            public Int64 CustomFieldGetId(string CustomFieldName)
            {
                Int64 customFieldId = -1;

                if (this.CustomFields != null)
                {
                    foreach(CustomField cf in this.CustomFields)
                    {
                        if (cf.CustomFieldName.ToLower() == CustomFieldName.ToLower())
                        {
                            if (customFieldId != -1)
                            {
                                // field name was found more than one time, return -1
                                return -1;
                            }
                            else
                            {
                                customFieldId = cf.CustomFieldID;
                            }
                        }
                    }
                }

                return customFieldId;
            }


            /// <summary>
            /// Returns the value for the custom field id you specified. Multiple values will be separated by "||"
            /// Will return empty string if the custom field was not found.
            /// </summary>  
            public string CustomFieldGetValue(Int64 CustomFieldid)
            {
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldID == CustomFieldid)
                    {
                        if (cf.DataType == Paraenums.CustomFieldDataType.Option)
                        {
                            // Custom field is an option field, iterate through the options

                            string returnValue = "";

                            foreach (ParaObjects.CustomFieldOptions cfo in cf.CustomFieldOptionsCollection)
                            {
                                if (cfo.IsSelected == true)
                                {
                                    if (!string.IsNullOrEmpty(returnValue))
                                    {
                                        returnValue += "||";
                                    }

                                    returnValue += cfo.CustomFieldOptionName;
                                }
                            }

                            return returnValue;
                        }
                        else
                        {
                            return cf.CustomFieldValue;
                        }
                    }
                }
                return string.Empty;
            }

            /// <summary>
            /// Returns the value for the custom field name you specified. Will return null if the custom field 
            /// was not found.
            /// </summary> 
            public string CustomFieldGetValue(string CustomFieldName)
            {
                foreach (CustomField cf in CustomFields)
                {
                    if (cf.CustomFieldName == CustomFieldName)
                    {
                        return cf.CustomFieldValue;
                    }
                }
                return "";
            }

            public abstract string GetReadableName();
        }

        //public abstract partial class ModuleWithCustomFieldsAndHistory : ModuleWithCustomFields
        //{
        //    /// <summary>
        //    /// The collection of custom fields for this module
        //    /// </summary>
        //    public List<ParaObjects.CustomField>  = new List<ParaObjects.CustomField>();

        //}

        /// <summary>
        /// Holds all the properties of the Ticket module.
        /// </summary>
        public partial class Ticket : ModuleWithCustomFields
        {
            /// <summary>
            /// The unique identifier of the ticket
            /// </summary>
            public Int64 id = 0;

            /// <summary>
            /// The full ticket number, including the account number. Usually in the format 
            /// of Account #-Ticket # 
            /// </summary>
            public string Ticket_Number = "";

            ///// <summary>
            ///// The id of the department the ticket is part of.
            ///// </summary>
            //public Int64 DepartmentID = 0;

            /// <summary>
            /// The product associated to a ticket. It will only be populated in certain configurations.
            /// </summary>
            public ParaObjects.Product Ticket_Product = new Product();

            /// <summary>
            /// The status of the ticket
            /// </summary>
            public TicketStatus Ticket_Status = new TicketStatus();

            /// <summary>
            /// The asset linked to the ticket. this is only populated for certain Product/Asset configurations, when the ticket is linked to an Asset.
            /// </summary>
            public ParaObjects.Asset Ticket_Asset = new ParaObjects.Asset();

            /// <summary>
            /// The department the tickets belongs to. While you specified already the department id in your
            /// credentials class, it could be that the user you are passing the Token of has access to multiple
            /// departments. In which case, the tickets that account has access to will be visible (no matter their departments).
            /// </summary>
            public ParaObjects.Department Department = new ParaObjects.Department();


            /// <summary>
            /// The customer that owns the ticket. If your only requested a standard Ticket read, only the customer id is returned withing the Customer class.
            /// </summary>
            public Customer Ticket_Customer = new Customer();

            /// <summary>
            /// The additional contact associated to this ticket.
            /// </summary>
            public Customer Additional_Contact = new Customer();

            /// <summary>
            /// The CSR that has entered this ticket (this class is filled only when a Ticket has been created by a CSR). Only the CSR id and Name are filled in case of a standard ticket read.
            /// </summary>
            public Csr Entered_By = new Csr();

            /// <summary>
            /// The CSR that is has this ticket assigned to. This class is only filled if the ticket is assigned to a CSR (as opposed to a Queue). If the ticket is assigned to a CSR, this class will only be filled with the ID of the CSR (unless you requested an appropriate request depth.
            /// </summary>
            public Csr Assigned_To = new Csr();

            /// <summary>
            /// Whether email notification is turned on or off.
            /// </summary>
            public bool Email_Notification;

            /// <summary>
            /// Whether email notification to Additional Contact is turned on or off.
            /// </summary>
            public Nullable<Boolean> Email_Notification_Additional_Contact;

            /// <summary>
            /// Whether email notification to Additional Contact is turned on or off.
            /// </summary>
            public Nullable<Boolean> Hide_From_Customer;


            /// <summary>
            /// An optional string array of CSR emails that are CCed when an email notification is sent.
            /// </summary>
            public ArrayList Cc_Csr = new ArrayList();

            /// <summary>
            /// An optional string array of customer emails that are CCed when an email notification is sent.
            /// </summary>
            public ArrayList Cc_Customer = new ArrayList();

            /// <summary>
            /// The Queue that has this ticket assigned to. This class is only filled if the ticket is assigned to a Queue (as opposed to a CSR).
            /// </summary>
            public Queue Ticket_Queue = new Queue();

            /// <summary>
            /// Parent Ticket of this ticket. Only filled whenever there is a parent ticket. Also, only the ticket id will be filled. Please make sure
            /// </summary>
            public Ticket Ticket_Parent;

            /// <summary>
            /// The list, if any exists, of all the child tickets. Please note that, by default, only the ticket id is filled.
            /// </summary>
            public List<Ticket> Ticket_Children;

            /// <summary>
            /// The list, if any exists, of all the related chats.
            /// </summary>
            public List<ParaObjects.Chat> Related_Chats;

            /// <summary>
            /// The list, if any exists, of all the Attachments of this ticket.
            /// </summary>
            public List<Attachment> Ticket_Attachments = new List<Attachment>();

            /// <summary>
            /// The list, if any exists, of all the available actions that can be run agains this ticket.
            /// Only the id and the name of the action
            /// </summary>
            public List<Action> Actions = new List<Action>();

            /// <summary>
            /// The actions that ran on this ticket. This is only populated if you requested the ticket action history.
            /// </summary>
            public List<ActionHistory> ActionHistory = new List<ActionHistory>();

            public string Date_Created = "";
            public string Date_Updated = "";

            /// <summary>
            /// Uploads an attachment to the current ticket. 
            /// The attachment will also be added to the current Ticket's attachments collection.
            /// </summary>
            /// <param name="Attachment">
            /// The binary Byte array of the attachment you would like to add. 
            ///</param>
            public void AttachmentsAdd(ParaCredentials paracredentials, Byte[] Attachment, string contentType, string FileName)
            {
                Ticket_Attachments.Add(ApiHandler.Ticket.TicketAddAttachment(paracredentials, Attachment, contentType, FileName));
            }

            /// <summary>
            /// Uploads a text based file to the current ticket. You need to pass a string, and the mime type of a text based file (html, text, etc...).            
            /// </summary>
            /// <param name="text">
            /// The content of the text based file. 
            ///</param>           
            /// <param name="paracredentials">
            /// The parature credentials class for the APIs.
            /// </param>            
            /// <param name="contentType">
            /// The type of content being uploaded, you have to make sure this is the right text.
            /// </param>
            /// <param name="FileName">
            /// The name you woule like the attachment to have.
            ///</param>
            public void AttachmentsAdd(ParaCredentials paracredentials, string text, string contentType, string FileName)
            {
                Ticket_Attachments.Add(ApiHandler.Ticket.TicketAddAttachment(paracredentials, text, contentType, FileName));
            }

            /// <summary>
            /// Updates the current Ticket attachment with a text based file. You need to pass a string, and the mime type of a text based file (html, text, etc...).            
            /// </summary>
            /// <param name="text">
            /// The content of the text based file. 
            ///</param>           
            /// <param name="paracredentials">
            /// The parature credentials class for the APIs.
            /// </param>            
            /// <param name="contentType">
            /// The type of content being uploaded, you have to make sure this is the right text.
            /// </param>
            /// <param name="FileName">
            /// The name you woule like the attachment to have.
            ///</param>
            public void AttachmentsUpdate(ParaCredentials paracredentials, string text, string AttachmentGuid, string contentType, string FileName)
            {
                AttachmentsDelete(AttachmentGuid);
                Ticket_Attachments.Add(ApiHandler.Ticket.TicketAddAttachment(paracredentials, text, contentType, FileName));
            }



            /// <summary>
            /// If you have an attachment and would like to replace the file, use this method. It will actually delete 
            /// the existing attachment, and then add a new one to replace it.
            /// </summary>
            public void AttachmentsUpdate(ParaCredentials paracredentials, Byte[] Attachment, string AttachmentGuid, string contentType, string FileName)
            {
                AttachmentsDelete(AttachmentGuid);
                Ticket_Attachments.Add(ApiHandler.Ticket.TicketAddAttachment(paracredentials, Attachment, contentType, FileName));
            }

            /// <summary>
            /// If you have an attachment and would like to delete, just pass the id here.
            /// </summary>
            public void AttachmentsDelete(string AttachmentGuid)
            {
                foreach (ParaObjects.Attachment at in Ticket_Attachments)
                {
                    if (at.GUID == AttachmentGuid)
                    {
                        Ticket_Attachments.Remove(at);
                    }
                }

            }

            public Ticket()
                : base()
            {
            }

            public Ticket(Ticket ticket)
                : base(ticket)
            {
                if (ticket != null)
                {
                    this.ActionHistory = new List<ActionHistory>(ticket.ActionHistory);
                    this.Actions = new List<Action>(ticket.Actions);
                    this.Additional_Contact = new Customer(ticket.Additional_Contact);
                    this.Assigned_To = new Csr(ticket.Assigned_To);
                    this.Cc_Csr = new ArrayList(ticket.Cc_Csr);
                    this.Cc_Customer = new ArrayList(ticket.Cc_Customer);
                    this.Date_Created = new string(ticket.Date_Created.ToCharArray());
                    this.Date_Updated = ticket.Date_Updated;
                    this.Department = new Department(ticket.Department);
                    this.Email_Notification = ticket.Email_Notification;
                    this.Email_Notification_Additional_Contact = ticket.Email_Notification_Additional_Contact;
                    this.Entered_By = new Csr(ticket.Entered_By);
                    this.Hide_From_Customer = ticket.Hide_From_Customer;
                    this.id = ticket.id;
                    this.operation = ticket.operation;
                    this.Ticket_Asset = new Asset(ticket.Ticket_Asset);
                    this.Ticket_Attachments = new List<Attachment>(ticket.Ticket_Attachments);
                    if (ticket.Ticket_Customer != null)
                    {
                        this.Ticket_Customer = new Customer(ticket.Ticket_Customer);
                    }
                    if (ticket.Ticket_Children != null)
                    {
                        this.Ticket_Children = new List<Ticket>(ticket.Ticket_Children);
                    }
                    this.Ticket_Number = ticket.Ticket_Number;
                    if (ticket.Ticket_Parent != null)
                    {
                        this.Ticket_Parent = new Ticket(ticket.Ticket_Parent);
                    }
                    this.Ticket_Product = new Product(ticket.Ticket_Product);
                    this.Ticket_Queue = new Queue(ticket.Ticket_Queue);
                    this.Ticket_Status = new TicketStatus(ticket.Ticket_Status);
                }
            }

            public override string GetReadableName()
            {
                return "Ticket #" + this.uniqueIdentifier.ToString();
            }
        }

        /// <summary>
        /// Holds all the properties of the Chat module.
        /// </summary>
        public partial class Chat : ModuleWithCustomFields
        {
            public Int64 ChatID = 0;
            public Int64 Chat_Number = 0;
            public String Browser_Language = "";
            public String Browser_Type = "";
            public String Browser_Version = "";
            public Customer customer= new Customer();
            public DateTime Date_Created;
            public DateTime Date_Ended;

            public List<ParaObjects.Ticket> Related_Tickets;
            public string Email = "";
            
            public Csr Initial_Csr= new Csr();

            public Role Customer_Role = new Role();
            public String Ip_Address = "";
            public Boolean Is_Anonymous;
            public String Referrer_Url = "";
            public Status Status= new Status();
            public String Summary="";
            public String User_Agent="";
            public Int32 Sla_Violation = 0;

            public List<ChatTranscript> ChatTranscripts= new List<ChatTranscript>();

            public Chat()
                : base()
            {
            }

            public override string GetReadableName()
            {
                return "Chat #" + this.uniqueIdentifier.ToString();
            }
        }

        public partial class ChatTranscript:objectBaseProperties
        {
            public Boolean isInternal =false;
            public Paraenums.ActionHistoryPerformerType performer;
            public String csrName="";
            public String customerName="";
            public String Text="";
            public DateTime Timestamp= new DateTime();
        }


        /// <summary>
        /// Holds all the properties of the Customer module.
        /// </summary>
        public partial class Customer : ModuleWithCustomFields
        {
            public Int64 customerid = 0;
            public Account Account = new Account();
            public Sla Sla = new Sla();
            public DateTime Date_Visited;
            public DateTime Date_Created;
            public DateTime Date_Updated;
            public string Email = "";
            public Role Customer_Role = new Role();

            /// <summary>
            /// Only used when you use "Username" as the identifier of your account.
            /// </summary>
            public string User_Name = "";
            public string First_Name = "";
            public string Last_Name = "";


            ////////////////////////////////////////// COULD NOT LOCATE IT FOR NOW //////////////
            /// <summary>
            /// User accepted terms of use.
            /// Certain configs have the terms of use feature activated. This property should be taken into account
            /// only when you are using the "customer terms of use" feature.
            /// </summary>
            public bool Tou;
            /////////////////////////////////////////////////////////////////////////////////////

            /// <summary>
            /// Password is only used when creating a new customer. This property is not filled when retrieving the customer details. It must be empty when updating a customer object.
            /// </summary>
            public string Password = "";

            /// <summary>
            /// Password confirm is only used when creating a new customer. This property is not filled when retrieving the customer details. It must be empty when updating a customer object.
            /// </summary>
            public string Password_Confirm = "";


            public CustomerStatus Status = new CustomerStatus();

            ///// <summary>
            ///// The list of all of the customer custom fields of this object.
            ///// </summary>
            //public List<ParaObjects.CustomField> CustomFields= new List<ParaObjects.CustomField>();

            public Customer()
                : base()
            {
            }

            public Customer(Customer customer)
                : base(customer)
            {
                this.customerid = customer.customerid;
                this.Account = new Account(customer.Account);
                this.Sla = new Sla(customer.Sla);
                this.Date_Visited = customer.Date_Visited;
                this.Email = customer.Email;
                this.User_Name = customer.User_Name;
                this.First_Name = customer.First_Name;
                this.Tou = customer.Tou;
                this.Password = customer.Password;
                this.Password_Confirm = customer.Password_Confirm;
                this.Status = new CustomerStatus(customer.Status);
                this.Customer_Role = new Role(customer.Customer_Role);
            }



            public override string GetReadableName()
            {
                return this.First_Name + " " + this.Last_Name + "(" + this.Email + ")";
            }
        }

        /// <summary>
        /// Holds all the properties of the Account module.
        /// </summary>
        public partial class Account : ModuleWithCustomFields
        {
            public Int64 Accountid = 0;
            public string Account_Name = "";
            public Csr Modified_By = new Csr();
            public Csr Owned_By = new Csr();
            public Sla Sla = new Sla();
            public DateTime Date_Created;
            public DateTime Date_Updated;
            public Role Default_Customer_Role = new Role();
            /// <summary>
            /// The list of all the other Viewable accounts, only available to certain configs.
            /// </summary>
            public List<ParaObjects.Account> Viewable_Account = new List<ParaObjects.Account>();

            ///// <summary>
            ///// The list of all custom fields of this object.
            ///// </summary>
            //public List<ParaObjects.CustomField> CustomFields= new List<ParaObjects.CustomField>() ;// = List<ParaObjects.CustomField>();

            public Account()
                : base()
            {
            }

            public Account(Account account)
                : base(account)
            {
                this.Accountid = account.Accountid;
                this.Account_Name = account.Account_Name;
                this.Modified_By = new Csr(account.Modified_By);
                this.Owned_By = new Csr(account.Owned_By);
                this.Sla = new Sla(account.Sla);
                this.Viewable_Account = new List<Account>(account.Viewable_Account);
                this.Default_Customer_Role = new Role(account.Default_Customer_Role);
            }

            public override string GetReadableName()
            {
                return this.Account_Name;
            }
        }

        /// <summary>
        /// Holds all the properties of the Download module.
        /// </summary>
        public partial class Download : objectBaseProperties
        {
            // Specific properties for this module
            /// <summary>
            /// An attachment object holds the information about any attachment.
            /// </summary>
            public Attachment Attachment = new Attachment();
            /// <summary>
            /// The unique identified of the download.
            /// </summary>
            public Int64 Downloadid = 0;
            public string Date_Created = "";
            public string Date_Updated = "";
            /// <summary>
            /// The description of the download.
            /// </summary>
            public string Description = "";
            /// <summary>
            /// In case this download consists of an external link, instead of a file, this property will be populated.
            /// Please make sure, when you use this property (in case of a create/update of a download) 
            /// that the Guid property is set to empty, as only one of these two properties must be filled.
            /// </summary>
            public string External_Link = "";

            public bool MultipleFolders;

            /// <summary>
            /// The list of folders under which the download is listed.
            /// </summary>
            public List<DownloadFolder> Folders = new List<DownloadFolder>();

            /// <summary>
            /// If the download consists of a file that has been uploaded, this would be the GUID of the file.
            /// Please make sure, when you use this property (in case of a create/update of a download) 
            /// that the ExternalLink property is set to empty, as only one of these two properties must be filled.
            /// </summary>
            public string Guid = "";
            /// <summary>
            /// List of Sla type objects. These SLAs are the ones allowed to see this download.
            /// </summary>
            public List<ParaObjects.Sla> Permissions = new List<Sla>();
            /// <summary>
            /// The name of the download.
            /// </summary>
            public string Name = "";
            /// <summary>
            /// List of products that are linked to this download. In case your config uses this feature.
            /// </summary>
            public List<ParaObjects.Product> Products = new List<Product>();
            /// <summary>
            /// Whether the download is published or not.
            /// </summary>
            public Boolean Published = new Boolean();
            public string Title = "";
            public bool Visible;
            /// <summary>
            /// File extension
            /// </summary>
            public string Extension = "";

            /// <summary>
            /// Certain configuration use the End User License Agreement (EULA). this controls what Eula would 
            /// be associated with this download.
            /// </summary>
            public Eula Eula = new Eula();

            /// <summary>
            /// Number of times the files where updated.
            /// </summary>
            public Int64 File_Hits = 0;

            /// <summary>
            /// Size of the file.
            /// </summary>
            public Int64 File_Size = 0;

            /// <summary>
            /// Uploads a file to the Parature system, from a standard System.Net.Mail.Attachment object, in case you use this from an email.
            /// </summary>            
            /// <param name="EmailAttachment">
            /// The email attachment to upload.
            /// </param>
            public void AttachmentsAdd(ParaCredentials paracredentials, System.Net.Mail.Attachment EmailAttachment)
            {
                this.Attachment = ApiHandler.Download.DownloadUploadFile(paracredentials, EmailAttachment);
                this.Guid = this.Attachment.GUID;
            }
            
            /// <summary>
            /// Uploads the file to the current Download. 
            /// The file will also be added to the current Downloads's Guid.
            /// </summary>
            /// <param name="Attachment">
            /// The binary Byte array of the file you would like to upload. 
            ///</param>           
            /// <param name="paracredentials">
            /// The parature credentials class for the APIs.
            /// </param>            
            /// <param name="contentType">
            /// The type of content being uploaded, you have to make sure this is the right text.
            /// </param>
            /// <param name="FileName">
            /// 
            ///</param>
            public void AttachmentsAdd(ParaCredentials paracredentials, Byte[] Attachment, string contentType, string FileName)
            {
                this.Attachment = ApiHandler.Download.DownloadUploadFile(paracredentials, Attachment, contentType, FileName);
                this.Guid = this.Attachment.GUID;
            }

            /// <summary>
            /// Uploads a text based file to the current Download. You need to pass a string, and the mime type of a text based file (html, text, etc...).            
            /// </summary>
            /// <param name="text">
            /// The content of the text based file. 
            ///</param>           
            /// <param name="paracredentials">
            /// The parature credentials class for the APIs.
            /// </param>            
            /// <param name="contentType">
            /// The type of content being uploaded, you have to make sure this is the right text.
            /// </param>
            /// <param name="FileName">
            /// The name you woule like the attachment to have.
            ///</param>
            public void AttachmentsAdd(ParaCredentials paracredentials, string text, string contentType, string FileName)
            {
                this.Attachment = ApiHandler.Download.DownloadUploadFile(paracredentials, text, contentType, FileName);
                this.Guid = this.Attachment.GUID;
            }

            /// <summary>
            /// Updates the current download attachment with a text based file. You need to pass a string, and the mime type of a text based file (html, text, etc...).            
            /// </summary>
            /// <param name="text">
            /// The content of the text based file. 
            ///</param>           
            /// <param name="paracredentials">
            /// The parature credentials class for the APIs.
            /// </param>            
            /// <param name="contentType">
            /// The type of content being uploaded, you have to make sure this is the right text.
            /// </param>
            /// <param name="FileName">
            /// The name you woule like the attachment to have.
            ///</param>
            public void AttachmentsUpdate(ParaCredentials paracredentials, string text, string contentType, string FileName)
            {
                this.Attachment = ApiHandler.Download.DownloadUploadFile(paracredentials, text, contentType, FileName);
                this.Guid = this.Attachment.GUID;
                this.Name = FileName;
            }


            /// <summary>
            /// If you have a download file and would like to replace the file, use this method. It will actually delete 
            /// the existing attachment, and then add a new one to replace it.
            /// </summary>
            public void AttachmentsUpdate(ParaCredentials paracredentials, Byte[] Attachment, string contentType, string FileName)
            {
                this.Attachment = ApiHandler.Download.DownloadUploadFile(paracredentials, Attachment, contentType, FileName);
                this.Guid = this.Attachment.GUID;
                this.Name = FileName;
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="allowMultipleFolders">True if Multiple Folders is configured</param>
            public Download(bool allowMultipleFolders)
            {
                this.MultipleFolders = allowMultipleFolders;
            }

            public Download(Download download)
                : base(download)
            {
                this.Downloadid = download.Downloadid;
                this.Date_Created = download.Date_Created;
                this.Date_Updated = download.Date_Updated;
                this.Description = download.Description;
                this.External_Link = download.External_Link;
                this.MultipleFolders = download.MultipleFolders;
                this.Folders = download.Folders;
                this.Guid = download.Guid;
                this.Permissions = new List<ParaObjects.Sla>(download.Permissions);
                this.Name = download.Name;
                this.Products = new List<ParaObjects.Product>(download.Products);
                this.Published = download.Published;
                this.Title = download.Title;
                this.Visible = download.Visible;
                this.Eula = new Eula(download.Eula);
                this.File_Hits = download.File_Hits;
                this.File_Size = download.File_Size;
            }
        }

        /// <summary>
        /// Holds all the properties of the Knowledge Base module.
        /// </summary>
        public partial class Article : objectBaseProperties
        {
            // Specific properties for this module

            /// <summary>
            /// The unique identified of the Knowledge Base id.
            /// </summary>
            public Int64 Articleid = 0;
            public string Date_Created = "";
            public string Date_Updated = "";
            /// <summary>
            /// The answer to the knowledge base question.
            /// </summary>
            public string Answer = "";
            /// <summary>
            /// The date this Article will expire on.
            /// </summary>
            public string Expiration_Date = "";
            /// <summary>
            /// Whether this article is published or not.
            /// </summary>
            public Boolean Published = new Boolean();
            /// <summary>
            /// The question asked for this Article.
            /// </summary>
            public string Question = "";
            /// <summary>
            /// The average rating this article received.
            /// </summary>
            public Int32 Rating = 0;
            /// <summary>
            /// The number of times this article has been viewed.
            /// </summary>
            public Int32 Times_Viewed = 0;
            public Csr Modified_By = new Csr();
            public Csr Created_By = new Csr();

            /// <summary>
            /// List of Folders under which this article is listed.
            /// </summary>
            public List<ParaObjects.Folder> Folders = new List<ParaObjects.Folder>();

            /// <summary>
            /// List of Sla type objects. These SLAs are the ones allowed to see this article.
            /// </summary>
            public List<ParaObjects.Sla> Permissions = new List<Sla>();

            /// <summary>
            /// List of products that are linked to this article. In case your config uses this feature.
            /// </summary>
            public List<ParaObjects.Product> Products = new List<Product>();

            public Article()
            {
            }

            public Article(Article article)
                : base(article)
            {
                this.Articleid = article.Articleid;
                this.Date_Created = article.Date_Created;
                this.Date_Updated = article.Date_Updated;
                this.Answer = article.Answer;
                this.Expiration_Date = article.Expiration_Date;
                this.Rating = article.Rating;
                this.Times_Viewed = article.Times_Viewed;
                this.Modified_By = new Csr(article.Modified_By);
                this.Created_By = new Csr(article.Created_By);
                this.Folders = new List<Folder>(article.Folders);
                this.Permissions = new List<Sla>(article.Permissions);
                this.Products = new List<Product>(article.Products);
            }
        }

        public partial class ArticleFolder : Folder
        {

            public ArticleFolder Parent_Folder;

            public bool FullyLoaded = false;

            public ArticleFolder()
            {
            }

            public ArticleFolder(ArticleFolder articleFolder)
            {
                this.Description = articleFolder.Description;
                this.FolderID = articleFolder.FolderID;
                this.FullyLoaded = articleFolder.FullyLoaded;
                this.Is_Private = articleFolder.Is_Private;
                this.Name = articleFolder.Name;
            }
        }

        public partial class ArticleFoldersList : PagedData
        {
            public List<ParaObjects.ArticleFolder> ArticleFolders = new List<ParaObjects.ArticleFolder>();

            public ArticleFoldersList()
            {
            }

            public ArticleFoldersList(ArticleFoldersList articleFoldersList)
                : base(articleFoldersList)
            {
                this.ArticleFolders = new List<ArticleFolder>(articleFoldersList.ArticleFolders);
            }
        }

        public partial class Product : ModuleWithCustomFields
        {


            ///////////////////////////////////////////////////////////////////////////////////////////
            ////////// Not sure this will work with reflection. Need to check the node name////////////
            ///////////////////////////////////////////////////////////////////////////////////////////
            public string Currency = "";

            public string Date_Created;
            public string Date_Updated;

            public Int64 productid = 0;
            public string Price = "";

            public ProductFolder Folder = new ProductFolder();




            public bool Instock;

            /// <summary>
            /// The long description of the product.
            /// </summary>
            public string Longdesc = "";
            public string Name = "";
            /// <summary>
            /// The short description of the product.
            /// </summary>
            public string Shortdesc = "";

            public string Sku = "";
            public bool Visible;
            public Product()
                : base()
            {
            }
            public Product(Product product)
                : base(product)
            {
                this.productid = product.productid;
                this.Price = product.Price;
                this.Currency = product.Currency;
                this.Folder = new ProductFolder(product.Folder);
                this.Instock = product.Instock;
                this.Longdesc = product.Longdesc;
                this.Name = product.Name;
                this.Shortdesc = product.Shortdesc;
                this.Sku = product.Sku;
                this.Visible = product.Visible;
                this.Date_Created = product.Date_Created;
                this.Date_Updated = product.Date_Updated;
            }


            public override string GetReadableName()
            {
                return this.Name;
            }
        }

        public partial class Asset : ModuleWithCustomFields
        {
            public Int64 Assetid = 0;
            /// <summary>
            /// The account that owns the asset, if any.
            /// </summary>
            public Account Account_Owner = new Account();

            /// <summary>
            /// The CSR that created the asset.
            /// </summary>
            public Csr Created_By = new Csr();

            /// <summary>
            /// The customer that owns the asset, if any.
            /// </summary>
            public Customer Customer_Owner = new Customer();

            /// <summary>
            /// The CSR that last modified the asset.
            /// </summary>
            public Csr Modified_By = new Csr();

            /// <summary>
            /// The name of the Asset.
            /// </summary>
            public string Name = "";

            ///// <summary>
            ///// The collection of custom fields of the asset module
            ///// </summary>
            //public List<ParaObjects.CustomField> CustomFields = new List<ParaObjects.CustomField>();

            /// <summary>
            /// The product this asset is derived from.
            /// </summary>
            public Product Product = new Product();

            public string Serial_Number = "";

            /// <summary>
            /// The status of the Asset.
            /// </summary>           
            public AssetStatus Status = new AssetStatus();

            public string Date_Created = "";
            public string Date_Updated = "";


            /// <summary>
            /// The list, if any exists, of all the available actions that can be run agains this ticket.
            /// Only the id and the name of the action
            /// </summary>
            public List<Action> AvailableActions = new List<Action>();

            // No vendors for now.
            ///// <summary>
            ///// Only use this if you have the Vendor feature activated.
            ///// </summary>
            //public string Vendor = "";

            public Asset()
                : base()
            {
            }

            public Asset(Asset asset)
                : base(asset)
            {
                this.Assetid = asset.Assetid;
                this.Account_Owner = new Account(asset.Account_Owner);
                this.Created_By = new Csr(asset.Created_By);
                this.Customer_Owner = new Customer(asset.Customer_Owner);
                this.Modified_By = new Csr(asset.Modified_By);
                this.Name = asset.Name;
                this.Product = new Product(asset.Product);
                this.Status = new AssetStatus(asset.Status);
                this.Date_Created = asset.Date_Created;
                this.Date_Updated = asset.Date_Updated;
                this.AvailableActions = new List<Action>(asset.AvailableActions);
                this.Serial_Number = asset.Serial_Number;
            }


            public override string GetReadableName()
            {
                return this.Name;
            }
        }

        public partial class Role
        {
            public Int64 RoleID;
            public string Name;
            public string Description;
            public Role()
            {
                RoleID = 0;
                Name = "";
                Description = "";
            }
            public Role(Role role)
            {
                this.RoleID = role.RoleID;
                this.Name = role.Name;
                this.Description = role.Description;
            }
            public Role(Int64 RoleID, string Name, string Description)
            {
                this.RoleID = RoleID;
                this.Name = Name;
                this.Description = Description;
            }
        }

        public partial class RolesList : PagedData
        {
            public List<ParaObjects.Role> Roles = new List<Role>();

            public RolesList()
            {
            }
            public RolesList(RolesList rolesList)
                : base(rolesList)
            {
                this.Roles = new List<Role>(rolesList.Roles);
            }
        }

        public partial class Timezone
        {
            public Int64 TimezoneID;
            public string Name;
            public string Abbreviation;
            public Timezone()
            {
                TimezoneID = 0;
                Name = "";
                Abbreviation = "";
            }
            public Timezone(Timezone timezone)
            {
                this.TimezoneID = timezone.TimezoneID;
                this.Name = timezone.Name;
                this.Abbreviation = timezone.Abbreviation;
            }

            public Timezone(Int64 ID, string Name, string Abbreviation)
            {
                this.TimezoneID = ID;
                this.Name = Name;
                this.Abbreviation = Abbreviation;
            }
        }

        public partial class TimezonesList : PagedData
        {
            public List<ParaObjects.Timezone> Timezones = new List<Timezone>();

            public TimezonesList()
            {
            }
            public TimezonesList(TimezonesList TimezonesList)
                : base(TimezonesList)
            {
                this.Timezones = new List<Timezone>(TimezonesList.Timezones);
            }
        }

        public partial class Sla
        {
            public Int64 SlaID = 0;
            public string Name = "";

            public Sla()
            {
            }

            public Sla(Sla sla)
            {
                this.SlaID = sla.SlaID;
                this.Name = sla.Name;
            }
        }

        public partial class SlasList : PagedData
        {
            public List<ParaObjects.Sla> Slas = new List<ParaObjects.Sla>();

            public SlasList()
            {
            }

            public SlasList(SlasList slasList)
                : base(slasList)
            {
                this.Slas = new List<Sla>(slasList.Slas);
            }
        }

        /// <summary>
        /// A department's property.
        /// </summary>
        public partial class Department : objectBaseProperties
        {
            private Int64 _DepartmentID = 0;

            public Int64 DepartmentID
            {
                get { return _DepartmentID; }
                set { _DepartmentID = value; }
            }

            private string _name = "";

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _description = "";

            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }


            public Department()
            {
            }

            public Department(Department department)
            {
                this.DepartmentID = department.DepartmentID;
                this.Name = department.Name;
                this.Description = department.Description;
            }
        }


        public partial class DepartmentsList : PagedData
        {
            public List<ParaObjects.Department> Departments = new List<ParaObjects.Department>();

            public DepartmentsList()
            {
            }

            public DepartmentsList(DepartmentsList departmentsList)
                : base(departmentsList)
            {
                this.Departments = new List<Department>(departmentsList.Departments);
            }
        }


        public abstract partial class View
        {
            // Specific properties for this module
            //private Int64 id
            private Int64 _ID = 0;

            public Int64 ID
            {
                get { return _ID; }
                set { _ID = value; }
            }

            private string _Name = "";

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
            /// <summary>
            /// Indicates whether this object is fully loaded or not. An object that is not fully loaded means 
            /// that only the id and name are available.
            /// </summary>
            public bool FullyLoaded = false;

            /// <summary>
            /// Contains all the information regarding the API Call that was made.
            /// </summary>
            public ApiCallResponse ApiCallResponse = new ApiCallResponse();


            public View()
            {
            }

            public View(View view)
            {
                this.ID = view.ID;
                this.Name = view.Name;
                this.FullyLoaded = view.FullyLoaded;
                this.ApiCallResponse = new ParaObjects.ApiCallResponse(view.ApiCallResponse);
            }

        }


        public partial class AccountView : View
        {


        }
        public partial class ContactView : View
        {


        }
        public partial class TicketView : View
        {


        }
        public partial class TicketViewList : PagedData
        {
            public List<ParaObjects.TicketView> views = new List<ParaObjects.TicketView>();

        }
        public partial class AccountViewList : PagedData
        {
            public List<ParaObjects.AccountView> views = new List<ParaObjects.AccountView>();

        }
        public partial class ContactViewList : PagedData
        {
            public List<ParaObjects.ContactView> views = new List<ParaObjects.ContactView>();

        }



        public partial class Folder
        {
            // Specific properties for this module

            public Int64 FolderID = 0;
            public string Name = "";
            public string Description = "";
            public bool Is_Private = false;

            /// <summary>
            /// Contains all the information regarding the API Call that was made.
            /// </summary>
            public ApiCallResponse ApiCallResponse = new ApiCallResponse();


            public Folder()
            {
            }

            public Folder(Folder folder)
            {
                this.FolderID = folder.FolderID;
                this.Name = folder.Name;
                this.Description = folder.Description;
                this.Is_Private = folder.Is_Private;
                this.ApiCallResponse = folder.ApiCallResponse;
            }

        }








        /// <summary>
        /// Used only for the downloads module folders.
        /// </summary>
        public partial class ProductFolder : Folder
        {
            /// <summary>
            /// Indicates whether this object is fully loaded or not. An object that is not fully loaded means 
            /// that only the id and name are available.
            /// </summary>
            public bool FullyLoaded = false;
            /// <summary>
            /// The last date this folder was updated.
            /// </summary>
            public string Date_Updated = "";


            public ProductFolder()
            {
            }

            public ProductFolder(ProductFolder ProductFolder)
            {
                this.FolderID = ProductFolder.FolderID;
                this.Date_Updated = ProductFolder.Date_Updated;
                this.FullyLoaded = ProductFolder.FullyLoaded;
                if (ProductFolder.Parent_Folder != null)
                {
                    this.Parent_Folder = new ProductFolder();
                    this.Parent_Folder = ProductFolder.Parent_Folder;
                }
            }

            /// <summary>
            /// To avoid infinite loops, the parent folder is not instantiated when 
            /// you instantiate a new ProductFolder object. In the case you are creating a download folder, please make sure to create a new ProductFolder, 
            /// set just the id of the folder, then make the ParentFolder equals the one you just created.
            /// </summary>
            public ProductFolder Parent_Folder;
        }

        public partial class ProductFoldersList : PagedData
        {
            public List<ParaObjects.ProductFolder> ProductFolders = new List<ParaObjects.ProductFolder>();

            public ProductFoldersList()
            {
            }

            public ProductFoldersList(ProductFoldersList ProductFoldersList)
                : base(ProductFoldersList)
            {
                this.ProductFolders = new List<ProductFolder>(ProductFoldersList.ProductFolders);
            }
        }

        /// <summary>
        /// Used only for the downloads module folders.
        /// </summary>
        public partial class DownloadFolder : Folder
        {
            public bool FullyLoaded = false;
            public string Date_Updated = "";
            /// <summary>
            /// To avoid infinite loops, the parent folder is not instantiated when 
            /// you instantiate a new DownloadFolder object. In the case you are creating a download folder, please make sure to create a new download folder, 
            /// set just the id of the folder, then make the ParentFolder equals the one you just created.
            /// </summary>
            public DownloadFolder Parent_Folder;

            public DownloadFolder()
            {
            }

            public DownloadFolder(DownloadFolder downloadFolder)
            {
                this.FullyLoaded = downloadFolder.FullyLoaded;
                this.Date_Updated = downloadFolder.Date_Updated;
                this.Parent_Folder = new DownloadFolder(downloadFolder.Parent_Folder);
            }
        }

        public partial class DownloadFoldersList : PagedData
        {
            public List<ParaObjects.DownloadFolder> DownloadFolders = new List<ParaObjects.DownloadFolder>();

            public DownloadFoldersList()
            {
            }

            public DownloadFoldersList(DownloadFoldersList downloadFoldersList)
                : base(downloadFoldersList)
            {
                this.DownloadFolders = new List<DownloadFolder>(downloadFoldersList.DownloadFolders);
            }
        }

        public partial class Eula
        {
            // Specific properties for this module
            public string ShortTitle = "";
            public Int64 EulaID = 0;

            public Eula()
            {
            }

            public Eula(Eula eula)
            {
                this.ShortTitle = eula.ShortTitle;
                this.EulaID = eula.EulaID;
            }

        }

        public partial class Queue
        {
            // Specific properties for this module

            private Int32 _QueueID = 0;

            public Int32 QueueID
            {
                get { return _QueueID; }
                set { _QueueID = value; }
            }
            private string _Name = "";

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            public Queue()
            {
            }

            public Queue(Queue queue)
            {
                this.QueueID = queue.QueueID;
                this.Name = queue.Name;
            }

        }

        public partial class QueueList : PagedData
        {
            public List<ParaObjects.Queue> Queues = new List<ParaObjects.Queue>();
            public QueueList()
            {
            }
            public QueueList(QueueList queueList)
                : base(queueList)
            {
                this.Queues = new List<Queue>(queueList.Queues);
            }

        }

        public partial class Status
        {
            // Specific properties for this module

            /// <summary>
            /// Contains all the information regarding the API Call that was made.
            /// </summary>
            public ApiCallResponse ApiCallResponse = new ApiCallResponse();
            private Int64 _StatusID = 0;

            /// <summary>
            /// 1 = Active, -1 = Deactivated
            /// </summary>
            public Int64 StatusID
            {
                get { return _StatusID; }
                set { _StatusID = value; }
            }
            private string _Name = "";

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            public Status()
            {
            }

            public Status(Status status)
            {
                this.StatusID = status.StatusID;
                this.Name = status.Name;
            }

            public Status(Int64 ID, string Name)
            {
                this.StatusID = ID;
                this.Name = Name;
            }
        }

        public partial class StatusList : PagedData
        {
            public List<ParaObjects.Status> Statuses = new List<ParaObjects.Status>();
            public StatusList()
            {
            }
            public StatusList(StatusList statuslist)
                : base(statuslist)
            {
                this.Statuses = new List<Status>(statuslist.Statuses);
            }
        }

        public partial class CustomerStatus : Status
        {
            public string Description = "";
            public string Text = "";

            public CustomerStatus()
            {
            }

            public CustomerStatus(CustomerStatus customerStatus)
                : base(customerStatus)
            {
                this.Description = customerStatus.Description;
                this.Text = customerStatus.Text;
                this.Name = customerStatus.Name;
                this.StatusID = customerStatus.StatusID;
            }
        }

        public partial class CustomerStatusList : PagedData
        {
            public List<ParaObjects.CustomerStatus> CustomerStatuses = new List<ParaObjects.CustomerStatus>();
            public CustomerStatusList()
            {
            }
            public CustomerStatusList(CustomerStatusList Customerstatuslist)
                : base(Customerstatuslist)
            {
                this.CustomerStatuses = new List<CustomerStatus>(Customerstatuslist.CustomerStatuses);
            }
        }

        public partial class TicketStatus : Status
        {
            public string Customer_Text = "";
            public Paraenums.TicketStatusType StatusType = Paraenums.TicketStatusType.All;


            public TicketStatus()
            {
            }

            public TicketStatus(TicketStatus ticketStatus)
                : base(ticketStatus)
            {
                this.StatusType = ticketStatus.StatusType;
                this.Customer_Text = ticketStatus.Customer_Text;
            }
        }

        public partial class TicketStatusList : PagedData
        {
            public List<ParaObjects.TicketStatus> TicketStatuses = new List<ParaObjects.TicketStatus>();
            public TicketStatusList()
            {
            }
            public TicketStatusList(TicketStatusList ticketstatuslist)
                : base(ticketstatuslist)
            {
                this.TicketStatuses = new List<TicketStatus>(ticketstatuslist.TicketStatuses);
            }

        }
        public partial class CsrStatus : Status
        {

            public CsrStatus()
            {
            }

            public CsrStatus(CsrStatus csrStatus)
                : base(csrStatus)
            {

            }
        }

        public partial class CsrStatusList : PagedData
        {
            public List<ParaObjects.CsrStatus> CsrStatuses = new List<ParaObjects.CsrStatus>();
            public CsrStatusList()
            {
            }
            public CsrStatusList(CsrStatusList csrstatuslist)
                : base(csrstatuslist)
            {
                this.CsrStatuses = new List<CsrStatus>(csrstatuslist.CsrStatuses);
            }

        }
        public partial class AssetStatus : Status
        {
            // Specific properties for this module

            /// <summary>
            /// The status internal name, as CSRs see it.
            /// </summary>
            public string Text = "";
            /// <summary>
            /// The status name as customers see it on the portal and in the emails they receive.
            /// </summary>
            public string Description = "";

            public AssetStatus()
            {
            }

            public AssetStatus(AssetStatus assetStatus)
                : base(assetStatus)
            {
                this.Text = assetStatus.Text;
                this.Description = assetStatus.Description;
            }

        }
        public abstract class ActionBase
        {

        }
        public partial class Action : ActionBase
        {
            public bool FullyLoaded;
            public Int64 ActionID = 0;
            public string ActionName = "";
            internal Paraenums.ActionType actionType;
            public string Comment = "";
            /// <summary>
            /// Indicates whether this action will be visible to the customer or not
            /// Only used for tickets.
            /// </summary>
            public bool VisibleToCustomer = false;
            public string EmailText;
            public ArrayList EmailListCsr;
            public ArrayList EmailListCustomers;
            public int TimeSpentHours = 0;
            public int TimeSpentMinutes = 0;

            public List<Attachment> Action_Attachments = new List<Attachment>();

            /// <summary>
            /// This property will only be considered when 
            /// the action if of type Assign to Queue.
            /// </summary>
            internal Int64 AssignToQueueid = 0;

            /// <summary>
            /// This property will only be considered when 
            /// the action if of type Assign to CSR.
            /// </summary>
            internal Int64 AssigntToCsrid = 0;

            public Action()
            {
            }

            public Action(Action action)
            {
                this.FullyLoaded = action.FullyLoaded;
                this.ActionID = action.ActionID;
                this.ActionName = action.ActionName;
                this.actionType = action.actionType;
                this.Comment = action.Comment;
                this.VisibleToCustomer = action.VisibleToCustomer;
                this.EmailText = action.EmailText;
                this.EmailListCsr = action.EmailListCsr;
                this.EmailListCustomers = action.EmailListCustomers;
                this.TimeSpentHours = action.TimeSpentHours;
                this.TimeSpentMinutes = action.TimeSpentMinutes;
                this.AssignToQueueid = action.AssignToQueueid;
                this.AssigntToCsrid = action.AssigntToCsrid;
                this.Action_Attachments = action.Action_Attachments;
            }
            ///// <summary>
            ///// Uploads an attachment to the current ticket. 
            ///// The attachment will also be added to the current Actions's attachments collection.
            ///// </summary>
            ///// <param name="Attachment">
            ///// The binary Byte array of the attachment you would like to add. 
            /////</param>
            //public void AttachmentsAdd(ParaCredentials paracredentials, Byte[] Attachment, string contentType, string FileName)
            //{
            //    Action_Attachments.Add(ApiHandler.Ticket.TicketAddAttachment(paracredentials, Attachment, contentType, FileName));
            //}

            ///// <summary>
            ///// Uploads a text based file to the current ticket. You need to pass a string, and the mime type of a text based file (html, text, etc...).            
            ///// </summary>
            ///// <param name="text">
            ///// The content of the text based file. 
            /////</param>           
            ///// <param name="paracredentials">
            ///// The parature credentials class for the APIs.
            ///// </param>            
            ///// <param name="contentType">
            ///// The type of content being uploaded, you have to make sure this is the right text.
            ///// </param>
            ///// <param name="FileName">
            ///// The name you woule like the attachment to have.
            /////</param>
            //public void AttachmentsAdd(ParaCredentials paracredentials, string text, string contentType, string FileName)
            //{
            //    Action_Attachments.Add(ApiHandler.Ticket.TicketAddAttachment(paracredentials, text, contentType, FileName));
            //}
        }

        public partial class ActionHistory
        {
            /// <summary>
            /// The unique identifier of this action history item
            /// </summary>
            public Int64 ActionHistoryID = 0;
            /// <summary>
            /// the action that run
            /// </summary>
            public Action Action = new Action();
            /// <summary>
            /// The status the object was on, before the action was run
            /// </summary>
            public TicketStatus Old_Status = new TicketStatus();
            /// <summary>
            /// The new status that the object moved to, after the action was run.
            /// </summary>
            public TicketStatus New_Status = new TicketStatus();
            public String Comments;
            /// <summary>
            /// Whether this action was exposed to the customer or not.
            /// </summary>
            public bool Show_To_Customer;
            public int Time_Spent;
            /// <summary>
            /// The date this action took place.
            /// </summary>
            public DateTime Action_Date;
            public ArrayList Cc_Csr = new ArrayList();
            public ArrayList Cc_Customer = new ArrayList();
            /// <summary>
            /// The list, if any exists, of all the Attachments of this action history item.
            /// </summary>
            public List<Attachment> History_Attachments = new List<Attachment>();
            public ActionHistoryPerformer Action_Performer = new ActionHistoryPerformer();

            public ActionHistory()
            {
            }

            public ActionHistory(ActionHistory actionHistory)
            {
                this.ActionHistoryID = actionHistory.ActionHistoryID;
                this.Action = new Action(actionHistory.Action);
                this.Old_Status = new TicketStatus(actionHistory.Old_Status);
                this.New_Status = new TicketStatus(actionHistory.New_Status);
                this.Comments = actionHistory.Comments;
                this.Show_To_Customer = actionHistory.Show_To_Customer;
                this.Time_Spent = actionHistory.Time_Spent;
                this.Action_Date = actionHistory.Action_Date;
                this.History_Attachments = new List<Attachment>(actionHistory.History_Attachments);
                this.Action_Performer = new ActionHistoryPerformer(actionHistory.Action_Performer);
                this.Cc_Csr = new ArrayList(actionHistory.Cc_Csr);
                this.Cc_Customer = new ArrayList(actionHistory.Cc_Customer);
            }
        }

        /// <summary>
        /// Indicates who performed an action history item, whether a CSR or a Customer and includes the id and name of the performer.
        /// </summary>
        public partial class ActionHistoryPerformer
        {
            public Paraenums.ActionHistoryPerformerType ActionHistoryPerformerType = Paraenums.ActionHistoryPerformerType.System;
            // Will be loaded with the CSR id and name, only if it was a CSR that performed the action.
            public ParaObjects.Csr CsrPerformer = new Csr();

            // Will be loaded with the Customer id and name, only if it was a Customer that performed the action.
            public ParaObjects.Customer CustomerPerformer = new Customer();

            public ActionHistoryPerformer()
            {
            }
            public ActionHistoryPerformer(ActionHistoryPerformer actionHistoryperformer)
            {
                this.ActionHistoryPerformerType = actionHistoryperformer.ActionHistoryPerformerType;
                this.CsrPerformer = actionHistoryperformer.CsrPerformer;
                this.CustomerPerformer = actionHistoryperformer.CustomerPerformer;
            }

            public string getDisplayName()
            {
                if (this.ActionHistoryPerformerType == Paraenums.ActionHistoryPerformerType.Csr)
                {
                    return this.CsrPerformer.Full_Name;
                }
                else if (this.ActionHistoryPerformerType == Paraenums.ActionHistoryPerformerType.Customer)
                {
                    return this.CustomerPerformer.First_Name + " " + this.CustomerPerformer.Last_Name;
                }
                else if (this.ActionHistoryPerformerType == Paraenums.ActionHistoryPerformerType.System)
                {
                    return "System";
                }
                else
                {
                    return "";
                }
            }

        }

        public partial class CsrsList : PagedData
        {
            public List<ParaObjects.Csr> Csrs = new List<ParaObjects.Csr>();

            public CsrsList()
            {
            }

            public CsrsList(CsrsList csrsList)
                : base(csrsList)
            {
                this.Csrs = new List<Csr>(csrsList.Csrs);
            }
        }

        public partial class Csr : objectBaseProperties
        {
            // Specific properties for this module

            private Int64 _CsrID = 0;

            public Int64 CsrID
            {
                get { return _CsrID; }
                set { _CsrID = value; }
            }
            private string _Full_Name = "";

            public string Full_Name
            {
                get { return _Full_Name; }
                set { _Full_Name = value; }
            }
            public string Email = "";
            public string Fax = "";
            public string Phone_1 = "";
            public string Phone_2 = "";
            public string Screen_Name = "";
            /// <summary>
            /// The following strings are the valid options for Date_Format:
            /// mm/dd/yyyy | mm/dd/yy | dd/mm/yyyy | dd/mm/yy | month dd, yyyy | month dd, yy
            /// </summary>
            public string Date_Format = "";
            public string Password = "";
            public Timezone Timezone = new Timezone();
            public Status Status = new Status();
            public DateTime Date_Created;
            public List<Role> Role = new List<Role>();

            public Csr()
            {
            }

            public Csr(Csr csr)
            {
                this.CsrID = csr.CsrID;
                this.Full_Name = csr.Full_Name;
                this.Email = csr.Email;
                this.Fax = csr.Fax;
                this.Phone_1 = csr.Phone_1;
                this.Phone_2 = csr.Phone_2;
                this.Screen_Name = csr.Screen_Name;
                this.Date_Format = csr.Date_Format;
                this.Status = new Status(csr.Status);
                this.Timezone = new Timezone(csr.Timezone);
                this.Date_Created = csr.Date_Created;
                this.Role = new List<Role>(csr.Role);
                this.Password = csr.Password;
            }
        }

        /// <summary>
        /// A custom field class is specific to each module.
        /// </summary>
        public partial class CustomField
        {
            /// <summary>
            /// The internal ID of the field
            /// </summary>
            public Int64 CustomFieldID = 0;
            /// <summary>
            /// The public name of the field
            /// </summary>
            public string CustomFieldName = "";

            public bool CustomFieldRequired;

            public bool dependent = false;

            public Int32 MaxLength=0;

            /// <summary>
            /// This Value will be populated with the field's value. For example, if this is a textbox field, this will hold the textbox's default field.
            /// </summary>
            public string CustomFieldValue = "";

            /// <summary>
            /// this indicates whether the custom field is editable or read only. If it is a read only, inluding it in an update will not result in that field value being updated.
            /// </summary>
            public bool Editable;

            public Paraenums.CustomFieldDataType DataType;

            public bool MultiValue;

            public bool FlagToDelete = false;

            /// <summary>
            /// If this is a custom field that holds multiple options, this collection of CustomFieldOptions will be populated.
            /// </summary>
            public List<CustomFieldOptions> CustomFieldOptionsCollection = new List<CustomFieldOptions>();

            public CustomField()
            {
            }

            public CustomField(CustomField customField)
            {
                this.CustomFieldID = customField.CustomFieldID;
                this.CustomFieldName = customField.CustomFieldName;
                this.CustomFieldRequired = customField.CustomFieldRequired;
                this.Editable = customField.Editable;
                this.DataType = customField.DataType;
                this.MultiValue = customField.MultiValue;
                this.MaxLength = customField.MaxLength;
                //this.CustomFieldOptionsCollection = new List<CustomFieldOptions>(customField.CustomFieldOptionsCollection);

                if (customField != null && customField.CustomFieldOptionsCollection != null)
                {
                    this.CustomFieldOptionsCollection = new List<CustomFieldOptions>();

                    foreach (CustomFieldOptions cfo in customField.CustomFieldOptionsCollection)
                    {
                        this.CustomFieldOptionsCollection.Add(new CustomFieldOptions(cfo));
                    }
                }

                this.FlagToDelete = customField.FlagToDelete;
            }

        }

        /// <summary>
        /// A custom field option is actually one of the possible values a custom field can take. So this can be an option in a dropdown, or a checkbox in a CheckBoxList field.
        /// </summary>
        public partial class CustomFieldOptions
        {
            public Int64 CustomFieldOptionID = 0;
            public string CustomFieldOptionName = "";

            public Int64 OptionID
            {
                get { return CustomFieldOptionID; }
                set { CustomFieldOptionID = value; }
            }

            public string OptionName
            {
                get { return CustomFieldOptionName; }
                set { CustomFieldOptionName = value; }
            }

            public bool dependent = false;
            public bool IsSelected = false;

            /// <summary>
            /// If the custom field option has dependent fields, or dependant field options, they will be listed under the DependantCustomFields collection.
            /// </summary>
            public List<DependantCustomFields> DependantCustomFields = new List<DependantCustomFields>();

            public CustomFieldOptions()
            {
            }

            public CustomFieldOptions(CustomFieldOptions customFieldOptions)
            {
                this.CustomFieldOptionID = customFieldOptions.CustomFieldOptionID;
                this.CustomFieldOptionName = customFieldOptions.CustomFieldOptionName;
                this.IsSelected = customFieldOptions.IsSelected;
                this.dependent = customFieldOptions.dependent;
                this.DependantCustomFields = new List<DependantCustomFields>(customFieldOptions.DependantCustomFields);
            }
        }

        /// <summary>
        /// Holds the list of dependant field IDs, as well as the field options contained within it.
        /// </summary>
        public partial class DependantCustomFields
        {
            public Int64 DependantFieldID = 0;
            public Int64[] DependantFieldOptions;
            public string DependantFieldPath;

            public DependantCustomFields()
            {
            }

            public DependantCustomFields(DependantCustomFields dependantCustomFields)
            {
                this.DependantFieldID = dependantCustomFields.DependantFieldID;
                this.DependantFieldOptions = dependantCustomFields.DependantFieldOptions;
                this.DependantFieldPath = dependantCustomFields.DependantFieldPath;
            }

        }

        /// <summary>
        /// An attachment object holds the information about any attachment, whether in the ticket history, in the ticket itself, a download, or any other module that supports the attachments feature.
        /// </summary>
        public partial class Attachment
        {
            /// <summary>
            /// This is the unique identifier of the Attachment/Download file in your Parature license.
            /// </summary>
            public string GUID = "";
            /// <summary>
            /// The name of the attachment.
            /// </summary>
            public string Name = "";
            public string AttachmentURL = "";
            /// <summary>
            /// The details of the error message, if the call generated an exception.
            /// </summary>
            public string Error = "";
            /// <summary>
            /// Whether or not there was an exception.
            /// </summary>
            public bool HasException;

            public Attachment()
            {
            }

            public Attachment(Attachment attachment)
            {
                this.GUID = attachment.GUID;
                this.Name = attachment.Name;
                this.AttachmentURL = attachment.AttachmentURL;
                this.Error = attachment.Error;
                this.HasException = attachment.HasException;
            }
        }

        public partial class OptimizationResult
        {
            public ParaQuery Query;

            public PagedData objectList;

            public ParaObjects.ApiCallResponse apiResponse;

            public OptimizationResult()
            {
            }

            public OptimizationResult(OptimizationResult optResult)
            {
                this.Query = optResult.Query;
                this.objectList = optResult.objectList;
                this.apiResponse = new ApiCallResponse(optResult.apiResponse);
            }
        }

        /// <summary>
        /// This class is contains all row information regarding an API call.
        /// </summary>
        public partial class ApiCallResponse
        {
            /// <summary>
            /// The url that was called when making the API call.
            /// </summary>
            public string CalledUrl = "";
            /// <summary>
            /// Call method performed: eg: Post, Put, Delete, etc...
            /// </summary>
            public string httpCallMethod = "";
            /// <summary>
            /// The Http response code that was returned. This might be 0 if the server did not respond.
            /// </summary>
            public int httpResponseCode = 0;
            /// <summary>
            /// Whether or not there was an exception.
            /// </summary>
            public bool HasException;
            /// <summary>
            /// The details of the error message, if the call generated an exception.
            /// </summary>
            public string ExceptionDetails = "";
            /// <summary>
            /// The XML that was received back from the server. Will only contain data when a proper XML is returned. This will be null if an exception was encountered. Please check first whether there was an exception or not, then check if this XML document is not null, before trying to use it.
            /// </summary>
            public System.Xml.XmlDocument xmlReceived = new System.Xml.XmlDocument();
            /// <summary>
            /// The XML that was sent to the server when making the API Call. In case of of a list or a retrieve, there is not XML that is sent. Please check first whether this is a null object or not, before using it.
            /// </summary>
            public System.Xml.XmlDocument xmlSent = new System.Xml.XmlDocument();

            /// <summary>
            /// If you were inserting or updating a record, check this value, if it is more than 0, it means the operations
            /// was successfull. Otherwise, it means there was an issue. If you created a new object, this value will hold the new id.
            /// </summary>
            public Int64 Objectid = 0;

            /// <summary>
            /// Number of API retries made to get the call to go through
            /// </summary>
            public Int16 AutomatedRetries = 0;

            public ApiCallResponse()
            {
            }

            public ApiCallResponse(ApiCallResponse apiCallResponse)
            {
                this.CalledUrl = apiCallResponse.CalledUrl;
                this.httpCallMethod = apiCallResponse.httpCallMethod;
                this.httpResponseCode = apiCallResponse.httpResponseCode;
                this.HasException = apiCallResponse.HasException;
                this.ExceptionDetails = apiCallResponse.ExceptionDetails;
                this.xmlReceived = apiCallResponse.xmlReceived;
                this.xmlSent = apiCallResponse.xmlSent;
                this.Objectid = apiCallResponse.Objectid;
            }
        }
    }

}
