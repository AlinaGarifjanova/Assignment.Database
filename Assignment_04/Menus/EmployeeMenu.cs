using Assignment_04.Models;
using Assignment_04.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Menus
{
    public class EmployeeMenu
    {
        private readonly EmployeeService _employeeService;

        public EmployeeMenu(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        // Menu för anaställda
        public async Task ManageEmployees()
        {
            Console.Clear();
            Console.WriteLine("Anställda");
            Console.WriteLine("--------------------\n\n");
            Console.WriteLine("1. Lägg till en anställd\n");
            Console.WriteLine("2. Visa alla anställda\n");
            Console.WriteLine("3. Visa en anställd\n");
            Console.WriteLine("4. Ta bort en anställd\n");
            Console.WriteLine("0. Återvänd till huvudmenyn\n");

            Console.Write("Välj något av alternaitven ovan: ");

            string userInput = Console.ReadLine()!;

            if (int.TryParse(userInput, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        await CreateEmployeeAsync();
                        break;
                    case 2:
                        await ShowAllEmployeesAsync();
                        break;
                    case 3:
                        await ShowOneEmployeeAsync();
                        break;
                    case 4:
                        await RemoveEmployeeAsync();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning. Ange ett heltal.");
            }

            Console.WriteLine("Tryck på Enter för att fortsätta.");
            Console.ReadLine();
            Console.Clear();

        }
        // Skapa en anställd
        public async Task CreateEmployeeAsync()
        {
            var form = new EmployeeRegistrationForm();

            Console.Clear();
            Console.WriteLine("Skriv in den anställdas uppgifter nedan");

            Console.Write("Förnamn: ");
            form.EmployeeFirstName = Console.ReadLine()!;

            Console.Write("Efternamn: ");
            form.EmployeeLastName = Console.ReadLine()!;

            Console.Write("E-postadress: ");
            form.EmployeeEmail = Console.ReadLine()!;

            Console.Write("Telefonnummer: ");
            form.EmployeePhone = Console.ReadLine()!;

            Console.Write("Region: ");
            form.RegionName = Console.ReadLine()!;

            var createdEmployee = await _employeeService.CreateEmployeeAsync(form);

            
            if (createdEmployee != null)
            {
                Console.WriteLine($"Anställd är skapad med ID: {createdEmployee.Id}");
            }
            else
            {
                Console.WriteLine("Det gick inte att skapa en ny anställd");
            }
        }

        // Visa alla anställda
        public async Task ShowAllEmployeesAsync()
        {
            Console.Clear();

            var employees = await _employeeService.GetAllEmployeesAsync();
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName}");
                Console.WriteLine($"{employee.EmployeePhone} {employee.EmployeeEmail}");

                // Kontrollerar om Region inte är null
                if (employee.Region != null)
                {
                    Console.WriteLine($"{employee.Region.RegionName}");
                }
                else
                {
                    Console.WriteLine("Ingen region hittad");
                }

                Console.WriteLine("------------------------------");
            }

            Console.ReadKey();
        }

        //Visar upp en anställd
        public async Task ShowOneEmployeeAsync()
        {
            Console.Clear();
            Console.Write("Ange den anställdes e-postadress: ");
            string email = Console.ReadLine()!;

            var employee = await _employeeService.GetEmployeeByEmailAsync(email);
            if (employee != null)
            {
                Console.WriteLine($"Anställdas information: {employee.EmployeeFirstName} {employee.EmployeeLastName}, {employee.EmployeeEmail}, {employee.EmployeePhone}");
                Console.WriteLine($"Regionen den anställda arbetar på: {employee.Region}");
            }
            else
            {
                Console.WriteLine("Kunden kunde inte hittas.");
            }


        }

        // Ta bort en anställd
        public async Task RemoveEmployeeAsync()
        {
            Console.WriteLine("Ange den anställdes e-postadress:");
            string email = Console.ReadLine()!;

            var employeeToRemove = await _employeeService.GetEmployeeByEmailAsync(email);

            if (employeeToRemove != null)
            {
                Console.WriteLine($"Information för den anställdes som kommer att tas bort: {employeeToRemove.EmployeeFirstName} {employeeToRemove.EmployeeLastName}, {employeeToRemove.EmployeeEmail}, {employeeToRemove.EmployeePhone}");

                Console.WriteLine("Är du säker på att du vill ta bort denna kund? (Ja/Nej):");
                string confirmation = Console.ReadLine()!;

                if (confirmation?.Trim().Equals("Ja", StringComparison.OrdinalIgnoreCase) == true)
                {
                    await _employeeService.RemoveEmployeeAsync(email);

                    Console.WriteLine("Den anställde har tagits bort.");
                }
                else
                {
                    Console.WriteLine("Borttagning avbruten.");
                }
            }
            else
            {
                Console.WriteLine("Anställde kunde inte hittas.");
            }

        }


    }
}
