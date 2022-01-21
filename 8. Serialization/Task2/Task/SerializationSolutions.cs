using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using Task.Serialization;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		Northwind dbContext;

		[TestInitialize]
		public void Initialize()
		{
			dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;
            var serializer = new NetDataContractSerializer();
			serializer.Binder = new CustomerBinder();

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(serializer, true);
			var categories = dbContext.Categories.ToList();

			// Serializes proxy object from the Nortwind database and then deserializes to the List<Categories>
			var result = tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
			var products = dbContext.Products
				.Include("Category")
				.Include("Supplier")
				.Include("Order_Details")
				.ToList();

			// Serializes proxy object from the Nortwind database and then deserializes to the List<Product>
			var result = tester.SerializeAndDeserialize(products);
		}

		[TestMethod]
		public void ISerializationSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;

            var surrogateSelector = new CustomSurrogateSelector();
            var serializer = new NetDataContractSerializer() {SurrogateSelector = surrogateSelector};

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(serializer, true);
			var orderDetails = dbContext.Order_Details.ToList();

			// Serializes proxy object from the Nortwind database and then deserializes to the List<Order_Detail>
			var result = tester.SerializeAndDeserialize(orderDetails);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;
			dbContext.Configuration.LazyLoadingEnabled = true;

			var surrogate = new OrderSurrogate();
			var knownTypes = new List<Type>();
			var serializer = new DataContractSerializer(typeof(IEnumerable<Order>), knownTypes, Int32.MaxValue, false, false, surrogate);

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);
			var orders = dbContext.Orders.ToList();

			// Serializes proxy object from the Nortwind database and then deserializes to the List<Order>
			var result = tester.SerializeAndDeserialize(orders);
		}
	}
}
