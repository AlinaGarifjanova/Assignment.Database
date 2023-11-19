namespace Assignment_04.Entities
{
    // En entitet som representerar en kundtyp
    public class CustomerTypeEntity
    {
        public int Id { get; set; }
        public string CustomerTypeName { get; set; } = null!;
        public ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
    }
}
