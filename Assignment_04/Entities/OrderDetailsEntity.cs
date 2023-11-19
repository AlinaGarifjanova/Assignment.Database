using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Entities
{
    // En entitet som representerar en beställnings detaljer 
    public class OrderDetailsEntity
    {
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
    }
}
