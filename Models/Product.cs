using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [NotMapped]
        public EnumProductType ProductType { get; set; }

        [Column("ProductType")]
        public string ProductTypeString
        {
            get { return ProductType.ToString(); }
            private set { ProductType = value.ParseEnum<EnumProductType>(); }
        }


    }
}
