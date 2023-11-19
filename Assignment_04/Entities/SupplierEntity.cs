using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Entities;
// En entitet som representerar leverantören
public class SupplierEntity
{
    public int Id { get; set; }
    public string SupplierName { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}