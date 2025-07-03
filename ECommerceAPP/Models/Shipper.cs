using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ECommerceAPP.Models;

public partial class Shipper
{
    public int ShipperId { get; set; }

    public string? CompanyName { get; set; }

    public string? Phone { get; set; }


    [JsonIgnore]

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
