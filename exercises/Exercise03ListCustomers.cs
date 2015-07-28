﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParatureSDK;
using ParatureSDK.ParaObjects;
using ParatureSDK.Query.ModuleQuery;

namespace Exercises
{
    class Exercise03ListCustomers
    {
        //Customers by account and date
        public static ParaEntityList<Customer> getCustomersByAccountAndDate(long acccountID, DateTime date)
        {
            var customerQuery = new CustomerQuery();
            customerQuery.RetrieveAllRecords = true;
            customerQuery.AddStaticFieldFilter(CustomerQuery.CustomerStaticFields.AccountID, ParaEnums.QueryCriteria.Equal, acccountID.ToString());
            //Use a custom filter if correct enums aren't available
            customerQuery.AddCustomFilter(String.Format("Date_Created_max_={0}/{1}/{2}", date.Month, date.Day, date.Year));

            var parature = new ParaService(CredentialProvider.Creds);
            var customers = parature.GetList<Customer>(customerQuery);

            return customers;
        }

        public static ParaEntityList<Customer> getCustomersByEmail(string email)
        {
            var customerQuery = new CustomerQuery();
            customerQuery.RetrieveAllRecords = true;
            customerQuery.AddStaticFieldFilter(CustomerQuery.CustomerStaticFields.CustomerEmail, ParaEnums.QueryCriteria.Equal, email);

            var parature = new ParaService(CredentialProvider.Creds);
            var customers = parature.GetList<Customer>(customerQuery);

            return customers;
        }

        public static ParaEntityList<Customer> getCustomersByStatus(long statusID)
        {
            var customerQuery = new CustomerQuery();
            customerQuery.RetrieveAllRecords = true;
            customerQuery.AddStaticFieldFilter(CustomerQuery.CustomerStaticFields.Status, ParaEnums.QueryCriteria.Equal, statusID.ToString());

            var parature = new ParaService(CredentialProvider.Creds);
            var customers = parature.GetList<Customer>(customerQuery);

            return customers;
        }

        public static ParaEntityList<Customer> getCustomersAndOrderByLastName()
        {
            var customerQuery = new CustomerQuery();
            customerQuery.RetrieveAllRecords = true;
            customerQuery.AddSortOrder(CustomerQuery.CustomerStaticFields.LastName, ParaEnums.QuerySortBy.Asc);

            var parature = new ParaService(CredentialProvider.Creds);
            var customers = parature.GetList<Customer>(customerQuery);

            return customers;
        }
    }
}