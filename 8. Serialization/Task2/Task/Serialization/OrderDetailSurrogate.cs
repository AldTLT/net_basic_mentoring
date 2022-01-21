using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Serialization
{
    public class OrderDetailSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            var index = 0;

            foreach (var item in (IEnumerable)obj)
            {
                var orderDetailSource = (Order_Detail)item;
                var orderDetail = new Order_Detail();
                Helper.SetProperty<Order_Detail>(orderDetailSource, orderDetail);

                if (orderDetailSource.Order != null)
                {
                    var order = new Order();
                    Helper.SetProperty<Order>(orderDetailSource.Order, order);
                    order.Customer = null;
                    order.Employee = null;
                    order.Shipper = null;
                    order.Order_Details = null;

                    orderDetail.Order = order;
                }

                if (orderDetailSource.Product != null)
                {
                    var product = new Product();
                    Helper.SetProperty<Product>(orderDetailSource.Product, product);

                    product.Category = null;
                    product.Order_Details = null;
                    product.Supplier = null;

                    orderDetail.Product = product;
                }

                info.AddValue($"OrderDetail{index}", orderDetail, typeof(Order_Detail));

                index++;
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            var index = 0;
            var getNext = true;
            var orderDetails = new List<Order_Detail>();

            do
            {
                try
                {                    
                    var orderDetail = (Order_Detail)info.GetValue($"OrderDetail{index}", typeof(Order_Detail));

                    orderDetails.Add(orderDetail);

                    index++;
                }
                catch
                {
                    getNext = false;
                }

            } while (getNext);

            return orderDetails;
        }
    }
}