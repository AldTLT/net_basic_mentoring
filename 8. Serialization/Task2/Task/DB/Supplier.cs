namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Serializable]
    public partial class Supplier : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierID { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(30)]
        public string ContactName { get; set; }

        [StringLength(30)]
        public string ContactTitle { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        public Supplier(SerializationInfo info, StreamingContext context)
        {
            Products = new HashSet<Product>();
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
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                info.AddValue(property.Name, property.GetValue(this));
            }
        }
    }
}
