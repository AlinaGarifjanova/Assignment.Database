using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Entities
{
    // En entitet som representerar en produkt
    public class ProductEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public int UnitsInStock { get; set; }

        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; } = null!;
        public int SupplierId { get; set; }
        public SupplierEntity Supplier { get; set; } = null!;


    }
}
