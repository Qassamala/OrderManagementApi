using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public EnumProductType ProductType { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
