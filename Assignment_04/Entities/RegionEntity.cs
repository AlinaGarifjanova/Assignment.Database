using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Entities
{
    // En entitet som representerar en Region för Anställda
    public class RegionEntity
    {
        public int Id { get; set; }
        public string RegionName { get; set; } = null!;
        public ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
    }
}
