using Microsoft.EntityFrameworkCore;

// En entitet  som representerar en Kategori
namespace Assignment_04.Entities
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
