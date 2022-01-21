using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Interfaces
{
    /// <summary>
    /// Represents access methods for customer
    /// </summary>
    public interface ICustomerDal
    {
        /// <summary>
        /// Return Customer
        /// </summary>
        /// <param name="customerName">Customer name</param>
        /// <returns>Customer</returns>
        public Customer GetCustomer(string customerName);
    }
}
