using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Models
{
    // Det som ska visas upp
    public class ShoppingCart
    {
        public int CustomerId { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public string EmployeeFirstName { get; set; } = null!;
        public string EmployeeLastName { get; set; } = null!;
        public string EmployeeEmail { get; set; } = null!;
        public string RegionName { get; set; } = null!;
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
