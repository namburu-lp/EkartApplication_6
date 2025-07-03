using System;
using System.Collections.Generic;

namespace ECommerceAPP.Models;

public partial class Territory
{
    public string TerritoryId { get; set; } = null!;

    public string? TerritoryDescription { get; set; }

    public int? RegionId { get; set; }

    public virtual Region? Region { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
