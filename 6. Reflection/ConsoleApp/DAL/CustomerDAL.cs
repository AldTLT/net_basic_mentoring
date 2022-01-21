using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using Ioc.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.DAL
{
    [Export(typeof(ICustomerDal))]
    public class CustomerDAL : ICustomerDal
    {
        /// <summary>
        /// Return Customer
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <returns>Customer</returns>
        public Customer GetCustomer(string customerName)
        {
            return new Customer() { Name = customerName };
        }
    }
}
