using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ParatureSDK.Query
{
    public abstract class ParaQuery
    {
        internal abstract Type QueryTargetType { get; }

        protected class QueryElement
        {
            public string QueryName = "";
            public string QueryFilter = "";
            public string QueryValue = "";
        }

        private bool _retrieveAllRecords = false;
        protected ArrayList _IncludedFields = new ArrayList();
        protected ArrayList _SortByFields = new ArrayList();
        protected ArrayList _QueryFilters = new ArrayList();
        protected ArrayList _CustomFilters = new ArrayList();
        protected List<QueryElement> QElements = new List<QueryElement>();

        protected string ProcessEncoding(string value)
        {
            var encodedValue = "";
            if (string.IsNullOrEmpty(value) == false)
            {
                value = Regex.Replace(value, ",", "\\,");
                encodedValue = WebUtility.UrlEncode(value);
            }
            return encodedValue;
        }

        /// <summary>
        /// If you set this property to "True", only the total number of items meeting your query is returned. There will be no objects returned.
        /// </summary>        
        public bool TotalOnly { get; set; }

        /// <summary>
        /// The number of the page you would like to request, first page should have the number 1 (which is the default value).
        /// </summary>       
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of records to return per page. Default is 25 (maximum is 500)
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Default API behavior is to retrieve entities of "active" status. 
        /// This will not include trashed entities by default. Provide an explicit status to override the default behavior.
        /// </summary>
        public ParaEnums.StatusType? GetActiveOrTrashed { get; set; }

        /// <summary>
        /// If you set this property to "True", ParaConnect will perform the appropriate number of calls to 
        /// retrieve all the data matching your request. Please note that the "PageSize" property will be ignored 
        /// since Paraconnect will manage the size of the page to call.
        /// CAUTION: This property might call a large amount of data and therefore be slow to respond, in addition to the pressure it 
        /// puts on Parature servers.
        /// </summary>        
        public bool RetrieveAllRecords
        {
            get { return _retrieveAllRecords; }
            set
            {
                if (value == true)
                {
                    PageSize = 500;
                }
                _retrieveAllRecords = value;
            }
        }

        /// <summary>
        /// The format you would like to get the results in. Leave as native for most cases, unless you specifically
        /// need an html or RSS format.
        /// </summary>
        public ParaEnums.OutputFormat OutputFormat { get; set; }

        /// <summary>
        /// Add a sort order to the Query, based on a static field.
        /// </summary>
        /// <param name="fieldName">
        /// the field name to passe would be the exact name of the field in the object properties.
        /// For example, if you have a property "Ticket.Date_Created", you will need to pass "Date_Created".
        /// </param>
        /// <param name="sortDirection"></param>              
        public bool AddSortOrder(string fieldName, ParaEnums.QuerySortBy sortDirection)
        {
            if (_SortByFields.Count < 5)
            {
                _SortByFields.Add(fieldName + "_" + sortDirection.ToString().ToLower() + "_");
                return true;
            }
            else
            {
                return false;
            }
        }

        public ParaQuery()
        {
            OutputFormat = ParaEnums.OutputFormat.native;
            PageSize = 25;
            PageNumber = 1;
            TotalOnly = false;
        }

        /// <summary>
        /// Adds a static field based filter to the query. 
        /// Static field filters are actually general properties that will be independant from static fields.
        /// You can use them this filter by passing the Read Only Static Property of the object you are using.
        /// You will find all these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </summary>
        /// <param name="staticFieldProperty">
        /// these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </param>
        /// <param name="criteria">
        /// The criteria you would like to apply to this static field.
        /// </param>
        /// <param name="value">
        /// The value you would like the static field to have, for this filter.
        /// </param>
        public void AddStaticFieldFilter(string staticFieldProperty, ParaEnums.QueryCriteria criteria, string value)
        {
            QueryFilterAdd(staticFieldProperty, criteria, ProcessEncoding(value));
        }

        /// <summary>
        /// Filter by static field with multiple values. Query acts like a union of all provided values.
        /// </summary>
        /// <param name="staticFieldProperty">The static field to filter against</param>
        /// <param name="criteria">The query criteria</param>
        /// <param name="values">The list of possible values</param>
        public void AddStaticFieldInListFilter(string staticFieldProperty, ParaEnums.QueryCriteria criteria, IEnumerable<string> values)
        {
            var processedValues = values.Select(value => ProcessEncoding(value)).ToList();

            QueryFilterAdd(staticFieldProperty, criteria, string.Join(",", processedValues));
        }

        /// <summary>
        /// Adds a static field based filter to the query. 
        /// Static field filters are actually general properties that will be independant from static fields.
        /// You can use them this filter by passing the Read Only Static Property of the object you are using.
        /// You will find all these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </summary>
        /// <param name="staticFieldProperty">
        /// these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </param>
        /// <param name="criteria">
        /// The criteria you would like to apply to this static field.
        /// </param>
        /// <param name="value">
        /// The DateTime value you would like the static field to have, for this filter. Down to the millisecond.
        /// DateTime will be converted to UTC and formatted as a string in the query.
        /// </param>
        public void AddStaticFieldFilter(string staticFieldProperty, ParaEnums.QueryCriteria criteria, DateTime value)
        {
            QueryFilterAdd(staticFieldProperty, criteria, value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
        }

        /// <summary>
        /// Adds a static field based filter to the query. Use this method only if you are dealing with a bool custom field (like a checkbox)
        /// Static field filters are actually general properties that will be independant from static fields.
        /// You can use them this filter by passing the Read Only Static Property of the object you are using.
        /// You will find all these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </summary>
        /// <param name="staticFieldProperty">
        /// these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </param>
        /// <param name="criteria">
        /// The criteria you would like to apply to this static field.
        /// </param>
        /// <param name="value">
        /// The bool value you would like the static field to have, for this filter.
        /// </param>
        public void AddStaticFieldFilter(string staticFieldProperty, ParaEnums.QueryCriteria criteria, bool value)
        {
            var filter = "0";
            filter = value 
                ? "1" 
                : "0";

            QueryFilterAdd(staticFieldProperty, criteria, filter);
        }

        /// <summary>
        /// Filter by static field for NULL or NOT NULL
        /// </summary>
        /// <param name="staticFieldProperty">Static field name</param>
        /// <param name="fieldFilter">Filter type</param>
        public void AddStaticFieldFilter(string staticFieldProperty, ParaEnums.FieldValueFilter fieldFilter)
        {
            string filterValue = "";

            switch (fieldFilter)
            {
                case ParaEnums.FieldValueFilter.IsNotNull:
                    filterValue = "_IS_NOT_NULL_";
                    break;
                case ParaEnums.FieldValueFilter.IsNull:
                    filterValue = "_IS_NULL_";
                    break;
            }

            QueryFilterAdd(staticFieldProperty, ParaEnums.QueryCriteria.Equal, filterValue);
        }

        /// <summary>
        /// Adds a static field based filter to the query. Use this method only if you are dealing with a bool custom field (like a checkbox)
        /// Static field filters are actually general properties that will be independant from static fields.
        /// You can use them this filter by passing the Read Only Static Property of the object you are using.
        /// You will find all these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </summary>
        /// <param name="staticFieldProperty">
        /// these properties in ModuleQuery>ObjectQuery>ObjectStaticFields, where object is
        /// the name of the module you are accessing.
        /// </param>
        /// <param name="criteria">
        /// The criteria you would like to apply to this static field.
        /// </param>
        /// <param name="value">
        /// The Date you would like to base your filter off. ParaConnect will manage the date formatting part.
        /// </param>        
        public void AddCustomFieldFilter(string staticFieldProperty, ParaEnums.QueryCriteria criteria, DateTime value)
        {
            QueryFilterAdd(staticFieldProperty, criteria, value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }

        protected void QueryFilterAdd(string field, ParaEnums.QueryCriteria criteria, string value)
        {
            var internalCrit = "";
            switch (criteria)
            {
                case ParaEnums.QueryCriteria.Equal:
                    internalCrit = "=";
                    break;
                case ParaEnums.QueryCriteria.LessThan:
                    internalCrit = "_max_=";
                    break;
                case ParaEnums.QueryCriteria.Like:
                    internalCrit = "_like_=";
                    break;
                case ParaEnums.QueryCriteria.MoreThan:
                    internalCrit = "_min_=";
                    break;
            }
            var qe = new QueryElement
            {
                QueryName = field, 
                QueryFilter = internalCrit, 
                QueryValue = value
            };
            QueryElementsRemoveDuplicate(qe);
            QElements.Add(qe);
        }

        /// <summary>
        /// This method allows you to inject an extra query parameter in the URL being called by our APIs.
        /// Using this method implies a very good knowledge of the underlying Parature API structure, as well as ParaConnect's inner workings and might break the API call.
        /// </summary>       
        public void AddCustomFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter) == false)
            {
                _CustomFilters.Add(filter);
            }

        }

        public ArrayList BuildQueryArguments()
        {
            _QueryFilters = new ArrayList();
            return BuildParaQueryArguments();
        }

        /// <summary>
        /// Provides the string array of all dynamic filtering and fields to include that will be further processed
        /// by the module specific object passed to the APIs, to include statis filtering.
        /// </summary>
        /// <summary>
        /// Builds the query arguments.
        /// </summary>
        protected ArrayList BuildParaQueryArguments()
        {
            if (_SortByFields != null)
            {
                if (_SortByFields.Count > 0)
                {
                    var fieldsSort = "_order_=";
                    for (var j = 0; j < _SortByFields.Count; j++)
                    {
                        if (j < _SortByFields.Count - 1)
                        {
                            fieldsSort = fieldsSort + ",";
                        }

                        fieldsSort = fieldsSort + _SortByFields[j].ToString();

                    }
                    _QueryFilters.Add(fieldsSort);
                }
            }

            BuildModuleSpecificFilter();

            // Include all regular queries
            foreach (var qe in QElements)
            {
                _QueryFilters.Add(qe.QueryName + qe.QueryFilter + qe.QueryValue);
            }

            // Include any custom filters strings.
            foreach (string s in _CustomFilters)
            {
                _QueryFilters.Add(s);
            }

            if (TotalOnly == true)
            {
                _QueryFilters.Add("_total_=true");
                RetrieveAllRecords = false;

            }
            else
            {
                _QueryFilters.Add("_startPage_=" + PageNumber);
                _QueryFilters.Add("_pageSize_=" + PageSize);

            }

            if (OutputFormat != ParaEnums.OutputFormat.native)
            {
                _QueryFilters.Add("_output_=" + OutputFormat.ToString());
            }

            return _QueryFilters;
        }

        /// <summary>
        /// Before adding a query element, making sure that no duplicates is there.
        /// </summary>
        /// <param name="QueryName"></param>
        protected void QueryElementsRemoveDuplicate(QueryElement qe)
        {
            foreach (var qes in QElements)
            {
                if (string.Compare(qes.QueryName, qe.QueryName, true) == 0 && string.Compare(qes.QueryFilter, qe.QueryFilter) == 0)
                {
                    QElements.Remove(qe);
                    return;
                }
            }
        }

        /// <summary>
        /// Checking if a record exists, and deleting it if it did.
        /// </summary>
        protected void ArrayCheckAndDeleteRecord(ArrayList arr, string nameValue)
        {
            if (arr.IndexOf(nameValue).ToString() != "-1")
            {
                arr.Remove(nameValue);
            }
        }

        protected abstract void BuildModuleSpecificFilter();
    }
}
