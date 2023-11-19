

namespace Assignment_04.Models
{
    // Det som ska visas upp
    public class ProductRegistrationForm
    {
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public decimal ProductPrice { get; set; }
        public string UnitsInStock { get; set; } = null!;

        public string SupplierName { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string? SupplierPhone { get; set; } 
        public string SupplierEmail { get; set; } = null!;
        public string SupplierAddress { get; set; } = null!;
    }
}
