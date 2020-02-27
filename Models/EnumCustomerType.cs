using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementApi.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EnumCustomerType
    {
        SmallCompany = 1,
        LargeCompany = 2,
        PrivateCustomer = 3
    }
}
