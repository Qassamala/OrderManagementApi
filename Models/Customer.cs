using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        [NotMapped]
        public EnumCustomerType CustomerType { get; set; }

        [Column("CustomerType")]
        public string CustomerTypeString
        {
            get { return CustomerType.ToString(); }
            private set { CustomerType = value.ParseEnum<EnumCustomerType>(); }
        }

    }
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
