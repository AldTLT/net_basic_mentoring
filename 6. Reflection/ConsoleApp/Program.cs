using ConsoleApp.DAL;
using ConsoleApp.Interfaces;
using ConsoleApp.Models;
using ConsoleApp.Providers;
using Ioc;
using System;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new MyContainer();
            container.AddType(typeof(Customer));
            container.AddType(typeof(OrderDAL), typeof(IOrderDal));
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerProviderInstance = container.CreateInstance(typeof(CustomerProvider));
            var orderProviderInstance = container.CreateInstance<OrderProvider>();
            var logger = container.CreateInstance<Logger>();
            var customer = container.CreateInstance(typeof(Customer));
            
            Console.ReadKey();
        }
    }
}
