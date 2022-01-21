using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using Ioc.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Providers
{
    [ImportConstructor]
    public class CustomerProvider
    {
        public ICustomerDal _customerDal;

        public CustomerProvider(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        /// <summary>
        /// Return Customer
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <returns>Customer</returns>
        public Customer GetCustomer(string customerName)
        {
            return _customerDal.GetCustomer(customerName);
        }
    }
}
