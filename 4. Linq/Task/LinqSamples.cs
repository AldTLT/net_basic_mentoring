// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Complete")]
		[Title("Task 1")]
		[Description("Find all customers whose total turnover exceeds a certain value X")]
		public void Linq1()
        {
            var turnoverArray = new int[] { 400, 1600, 10000 };

            foreach (var turnover in turnoverArray)
            {
                var customers = dataSource.Customers
                .Where(c => c.Orders.Sum(order => order.Total) > turnover);

                Console.WriteLine("Customers whose have total orders more than {0}:", turnover);
                foreach (var c in customers)
                {
                    var total = c.Orders.Sum(o => o.Total);
                    Console.WriteLine("Customer: {0} \t Total: {1}", c.CustomerID, total);
                }

                Console.WriteLine();
            }
		}

		[Category("Complete")]
		[Title("Task 2")]
		[Description("Get customers and list of suppliers located in the same country and the same city.")]
		public void Linq2()
        {
            #region Without grouping
            var customers = dataSource.Customers
                .Select(customer =>
                {
                    var suppliers = dataSource.Suppliers
                    .Where(supplier =>
                        supplier.City.Equals(customer.City) && supplier.Country.Equals(customer.Country));
                    return new { customer, suppliers };
                })
                .Where(customer => customer.suppliers.Count() > 0);

            Console.WriteLine("Customers and suppliers in the same location without grouping:");
            foreach (var customer in customers)
            {
                Console.WriteLine("Customer: {0}\t Location: {1} {2}", customer.customer.CustomerID, customer.customer.City, customer.customer.Country);
                foreach (var supplier in customer.suppliers)
                {
                    Console.WriteLine("Supplier: {0}\t Location: {1} {2}", supplier.SupplierName, supplier.City, supplier.Country);
                }

                Console.WriteLine();
            }
            #endregion

            #region With grouping

            var locationGroup =
                dataSource.Customers
                    .Join(
                        dataSource.Suppliers,
                        c => c.City + c.Country,
                        s => s.City + s.Country,
                        (c, s) => new {c, s})
                    .GroupBy(o => $"{o.c.City}, {o.c.Country}");

            Console.WriteLine();
            Console.WriteLine("Customers and suppliers in the same location with grouping:");
            foreach (var location in locationGroup)
            {
                Console.WriteLine("Location: {0}", location.Key );
                foreach (var customersAndSuppliers in location)
                {
                    Console.WriteLine("Customer: {0}\t Supplier: {1}", customersAndSuppliers.c.CustomerID, customersAndSuppliers.s.SupplierName);
                }

                Console.WriteLine();
            }
            #endregion
        }

        [Category("Complete")]
		[Title("Task 3")]
		[Description("Find all customers who had any order greater than 2000")]
		public void Linq3()
		{
            var x = 2000;

            var customers =
                from customer in dataSource.Customers
                where customer.Orders.Any(order => order.Total > x)
                let order = customer.Orders.FirstOrDefault(order => order.Total > x)?.Total
                select new {customer, order};

			Console.WriteLine("Customers");
            foreach (var c in customers)
			{
                Console.WriteLine("Customer: {0} \t Order: {1}", c.customer.CustomerID, c.order);
			}
		}

        [Category("Complete")]
        [Title("Task 4")]
        [Description("Client list and date when they became them")]
        public void Linq4()
        {
            var customers = dataSource.Customers.Select(customer =>
            {
                var date =
                customer.Orders
                .OrderBy(order => order.OrderDate)
                .FirstOrDefault()?
                .OrderDate;

                return new { customer, date };
            });

            // Print customers with date of first order
            Console.WriteLine("Customers and date of the first order");
            foreach (var c in customers)
            {
                Console.WriteLine("Customer: {0} \t date: {1:MM.yyyy}", c.customer.CustomerID, c.date);
            }
        }

        [Category("Complete")]
        [Title("Task 5")]
        [Description("Client list and date when they became them ordered by year, month, total order, and name")]
        public void Linq5()
        {
            var customers =
                dataSource.Customers.Select(customer =>
            {
                var date = customer.Orders
                .OrderBy(order => order.OrderDate)
                .FirstOrDefault()?
                .OrderDate;

                return new { customer, date };
            })
                .OrderBy(customer => customer.date?.Year)
                .ThenBy(customer => customer.date?.Month)
                .ThenByDescending(customer => customer.customer.Orders.Sum(order => order.Total))
                .ThenBy(customer => customer.customer.CustomerID);

            // Print customers with date of first order
            Console.WriteLine("Customers and date of the first order");
            foreach (var c in customers)
            {
                Console.WriteLine("Customer: {0} \t date: {1:MM.yyyy} \t total orders: {2}", c.customer.CustomerID, c.date, c.customer.Orders.Sum(order => order.Total));
            }
        }

        [Category("Complete")]
        [Title("Task 6")]
        [Description("Find all customers who have a non-digital postal code or an incomplete region or there is no operator code on the phone")]
        public void Linq6()
        {
            var customers = dataSource.Customers
                .Where(customer => 
                    string.IsNullOrEmpty(customer.PostalCode)
                    || string.IsNullOrEmpty(customer.Region)
                    || string.IsNullOrEmpty(customer.Phone)
                    || !customer.Phone.Contains("(")
                    || customer.PostalCode.Any(c => !char.IsDigit(c))
                )
                .Select(customer => customer);

            Console.WriteLine("Customers with non-digit postal code, or non region, or non operator code in the phone number:");
            foreach (var c in customers)
            {
                Console.WriteLine("Customer: {0} \t postal code: {1} \t region: {2} \t phone: {3}", c.CustomerID, c.PostalCode, c.Region, c.Phone);
            }
        }

        [Category("Complete")]
        [Title("Task 7")]
        [Description("Group products by category, then by in stock, then by price")]
        public void Linq7()
        {
            var products =
                from product in dataSource.Products
                group product by product.Category into ProductCategory
                select new
                {
                    Category = ProductCategory.Key,
                    Stock =
                    from product in ProductCategory
                    group product by product.UnitsInStock > 0 into InStock
                    select new
                    {
                        InStock.Key,
                        Products = InStock.OrderBy(c => c.UnitPrice)
                    }
                };

            foreach (var p in products)
            {
                Console.WriteLine();
                Console.WriteLine("Category: {0}", p.Category);
                foreach (var s in p.Stock)
                {
                    Console.WriteLine("Exists in Stock: {0}", s.Key);
                    foreach (var product in s.Products)
                    {
                        Console.WriteLine("Name: {0}\t\t Price: {1}", product.ProductName, product.UnitPrice);
                    }
                }
            }
        }

        [Category("Complete")]
        [Title("Task 8")]
        [Description("Group products by price into groups 'cheap' <= 20, 'average', 'expensive' > 100")]
        public void Linq8()
        {
            decimal cheap = 20;
            decimal average = 100;

            var productGroups = dataSource.Products
                .GroupBy(p => p.UnitPrice <= cheap ? "Cheap" : p.UnitPrice > average ? "Expensive" : "Average");

            foreach (var products in productGroups)
            {
                Console.WriteLine("Group: {0}", products.Key);
                foreach (var product in products)
                {
                    Console.WriteLine("Price: {0}\t name: {1}", product.UnitPrice, product.ProductName);
                }

                Console.WriteLine();
            }
        }

        [Category("Complete")]
        [Title("Task 9")]
        [Description("Average profitability of each city")]
        public void Linq9()
        {
            var cities =
                from customer in dataSource.Customers
                group customer by customer.City into CityGroup
                let average = CityGroup.Average(customer => customer.Orders?.Average(order => order?.Total))
                select new
                {
                    CityName = CityGroup.Key,
                    Profit = average == null ? 0 : Math.Truncate((decimal)average),
                    Intensity = Math.Round(CityGroup.Average(customer => customer.Orders.Count()), 2),
                    Customers = CityGroup
                };

            foreach (var city in cities)
            {
                Console.WriteLine("City: {0}\t profitability: {1}\t intensity: {2}", city.CityName, city.Profit, city.Intensity);
            }
        }

        [Category("Complete")]
        [Title("Task 10")]
        [Description("Average annual statistics of client activity by months (excluding the year), statistics by years, by years and months")]
        public void Linq10()
        {
            // Statistic by Month
            var monthStatistic = dataSource.Customers
                .SelectMany(c => c.Orders)
                .GroupBy(c => c.OrderDate.Month)
                .Select(m => new { Month = m.Key, Activity = m.Count() })
                .OrderBy(m => m.Month);

            Console.WriteLine("Statistic by Month");
            foreach (var month in monthStatistic)
            {
                Console.WriteLine(
                    "Month: {0}\t orders number: {1}", 
                    CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month.Month),
                    month.Activity
                    );
            }

            // Statistic by Year
            var yearStatistic = dataSource.Customers
                .SelectMany(c => c.Orders)
                .GroupBy(m => m.OrderDate.Year)
                .Select(m => new { Year = m.Key, Activity = m.Count() })
                .OrderBy(m => m.Year);

            Console.WriteLine();
            Console.WriteLine("Statistic by Year");
            foreach (var year in yearStatistic)
            {
                Console.WriteLine(
                    "Year: {0}\t orders number: {1}", year.Year, year.Activity);
            }

            // Statistic by Month and Year
            var statistic = dataSource.Customers
                .SelectMany(c => c.Orders)
                .GroupBy(m => new {Month = m.OrderDate.Month, Year = m.OrderDate.Year})
                .Select(m => new {Date = new DateTime(m.Key.Year, m.Key.Month, 1), Activity = m.Count()})
                .OrderBy(m => m.Date.Year)
                .ThenBy(m => m.Date.Month);

            Console.WriteLine();
            Console.WriteLine("Statistic by Month and Year");
            foreach (var period in statistic)
            {
                Console.WriteLine("Date: {0}\t orders number: {1}", period.Date.ToString("MMMM yyyy"), period.Activity);
            }
        }
    }
}
