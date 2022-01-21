using ConsoleApp.Interfaces;
using Ioc.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Providers
{
    public class OrderProvider
    {
        [Import]
        public Logger logger { get; set; }

        [Import]
        public IOrderDal _order { get; set; }
        public OrderProvider()
        {

        }
    }
}
