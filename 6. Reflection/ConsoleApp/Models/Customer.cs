using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer mobile phone
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Customer city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<Order> Orders { get; set; }
    }
}
