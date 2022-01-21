using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    /// <summary>
    /// Order of customer
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Order price
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime Date { get; }
        public Order(string name, decimal price, DateTime date)
        {
            Name = name;
            Price = price;
            Date = date;
        }
    }
}
