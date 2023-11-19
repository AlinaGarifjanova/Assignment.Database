using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Entities
{
    // En entitet som representerar en Order
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
        public int EmployeeId { get; set; }
        public EmployeeEntity Employee { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderDetailsEntity> OrderDetails { get; set; } = new List<OrderDetailsEntity>();
    }
}
