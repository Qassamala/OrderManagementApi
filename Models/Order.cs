using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal TotalSum { get; set; }

        public decimal TotalDiscount { get; set; }

        public virtual ICollection<OrderRow> Rows { get; set; }
    }
}
