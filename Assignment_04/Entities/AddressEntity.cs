namespace Assignment_04.Entities;

// En entitet  som representerar en Adress
public class AddressEntity
{
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public ICollection<CustomerEntity> Customers { get; set; } = null!;
}
