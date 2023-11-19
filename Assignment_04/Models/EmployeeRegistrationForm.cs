namespace Assignment_04.Models;

// Det som ska visas upp
public class EmployeeRegistrationForm
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; } = null!;
    public string EmployeeLastName { get; set; } = null!;
    public string ?EmployeePhone { get; set; }
    public string EmployeeEmail { get; set; } = null!;
    public string? RegionName { get; set; } 

}
