using Microsoft.EntityFrameworkCore;

// En entitet som representerar en Kund
namespace Assignment_04.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;
        public int CustomerTypeId { get; set; }
        public CustomerTypeEntity CustomerType { get; set; } = null!;

        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    }
}
