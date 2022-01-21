using System.Runtime.Serialization;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Task.Serialization;

    [Serializable]
    public partial class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        [DataMember]
        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        private Product(SerializationInfo info, StreamingContext context)
        {
            Order_Details = new HashSet<Order_Detail>();

            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                try
                {
                    var value = info.GetValue(property.Name, property.PropertyType);
                    var pi = GetType().GetProperty(property.Name);
                    pi.SetValue(this, value);
                }
                catch
                {
                    continue;
                }               
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProductID", ProductID);
            info.AddValue("ProductName", ProductName);
            info.AddValue("SupplierID", SupplierID);
            info.AddValue("CategoryID", CategoryID);
            info.AddValue("QuantityPerUnit", QuantityPerUnit);
            info.AddValue("UnitPrice", UnitPrice);
            info.AddValue("UnitsInStock", UnitsInStock);
            info.AddValue("UnitsOnOrder", UnitsOnOrder);
            info.AddValue("ReorderLevel", ReorderLevel);
            info.AddValue("Discontinued", Discontinued);

            if (Supplier != null)
            {
                var supplier = new Supplier();
                Helper.SetProperty<Supplier>(Supplier, supplier);
                supplier.Products = null;
                info.AddValue("Supplier", supplier);
            }

            if (Category != null)
            {
                var category = new Category();
                Helper.SetProperty<Category>(Category, category);
                category.Products = null;
                info.AddValue("Category", category);
            }

            if (Order_Details != null)
            {
                var orderDetails = new List<Order_Detail>();
                foreach (var od in Order_Details)
                {
                    var orderDetail = new Order_Detail();
                    Helper.SetProperty<Order_Detail>(od, orderDetail);
                    orderDetail.Order = null;
                    orderDetail.Product = null;
                    orderDetails.Add(orderDetail);
                }

                info.AddValue("Order_Details", orderDetails);
            }
        }
    }
}
