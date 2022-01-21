using System.CodeDom;
using System.Runtime.Serialization;

namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Task.Serialization;

    [DataContract]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [DataMember]
        public int CategoryID { get; set; }

        [DataMember]
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        [DataMember]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [DataMember]
        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [OnSerializing]
        private void OnSerializingCategory(StreamingContext context)
        {
            var products = new List<Product>();

            if (Products != null)
            {
                foreach (var product in Products)
                {
                    var productToSerialize = new Product();

                    Helper.SetProperty<Product>(product, productToSerialize);
                    productToSerialize.Category = null;
                    productToSerialize.Order_Details = null;
                    productToSerialize.Supplier = null;

                    products.Add(productToSerialize);
                }

                Products = products;
            }
        }
    }
}
