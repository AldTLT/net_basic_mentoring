using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using Ioc.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.DAL
{
    public class OrderDAL : IOrderDal
    {
        public bool AddOrder(string customerName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(string customerName, string orderName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders(string customerName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrder(string customerName, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
