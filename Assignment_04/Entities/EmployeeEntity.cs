using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Entities;

[Index(nameof(EmployeeEmail), IsUnique = true)]

// En entitet som representerar en anställd
public class EmployeeEntity
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; } = null!;
    public string EmployeeLastName { get; set; } = null!;
    public string? EmployeePhone { get; set; } 
    public string EmployeeEmail { get; set; } = null!;

    public int RegionId { get; set; }
    public RegionEntity? Region { get; set; }

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
