using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.Serialization
{
    public class OrderSurrogate : IDataContractSurrogate
    {
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public Type GetDataContractType(Type type)
        {
            if (type == (typeof(IEnumerable<Order>)))
            {
                return typeof(List<Order>);
            }

            return type;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj.GetType() == typeof(List<Order>))
            {
                var orders = (List<Order>)obj;
                var result = new List<Order>();

                foreach (var order in orders)
                {
                    var orderToSerialize = new Order();
                    Helper.SetProperty<Order>(order, orderToSerialize);

                    var customer = new Customer();
                    Helper.SetProperty<Customer>(order.Customer, customer);

                    var employee = new Employee();
                    Helper.SetProperty<Employee>(order.Employee, employee);

                    var shipper = new Shipper();
                    Helper.SetProperty<Shipper>(order.Shipper, shipper);

                    var orderDetails = new List<Order_Detail>();
                    if (order.Order_Details != null)
                    {
                        foreach (var orderDetail in order.Order_Details)
                        {
                            var orderDetailToSerialize = new Order_Detail();
                            Helper.SetProperty<Order_Detail>(orderDetail, orderDetailToSerialize);
                            orderDetailToSerialize.Order = null;
                            orderDetailToSerialize.Product = null;

                            orderDetails.Add(orderDetailToSerialize);
                        }
                    }

                    orderToSerialize.Customer = customer;
                    orderToSerialize.Employee = employee;
                    orderToSerialize.Shipper = shipper;
                    orderToSerialize.Order_Details = orderDetails;

                    result.Add(orderToSerialize);
                }

                return result;
            }

            return obj;
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}
