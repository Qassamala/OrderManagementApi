using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Models
{
    public class OrderRow
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public decimal? SingleProductPrice { get; set; }

        public decimal TotalSum { get; set; }
        public decimal TotalDiscount { get; set; }

        [Required]
        [ForeignKey("Product")]
        public long? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [ForeignKey("Order")]
        public long? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
