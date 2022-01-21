using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Interfaces
{
    public interface IOrderDal
    {
        public IEnumerable<Order> GetOrders(string customerName);
        public bool AddOrder(string customerName);
        public bool DeleteOrder(string customerName, string orderName);
        public bool UpdateOrder(string customerName, Order order);
    }
}
