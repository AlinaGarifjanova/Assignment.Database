using Assignment_04.Models;
using Assignment_04.Services;

namespace Assignment_04.Menus
{

    public class CustomerMenu
    {
        private readonly CustomerService _customerService;

        public CustomerMenu(CustomerService customerService)
        {
            _customerService = customerService;
        }
        
        //Meny för kunddelen
        public async Task ManageCustomers()
        {
            Console.Clear();
            Console.WriteLine("Kunder");
            Console.WriteLine("--------------------\n\n");
            Console.WriteLine("1. Lägg till kund\n");
            Console.WriteLine("2. Visa alla kunder\n");
            Console.WriteLine("3. Visa en kund\n");
            Console.WriteLine("4. Ta bort kund\n");
            Console.WriteLine("0. Återvänd till huvudmenyn\n");

            Console.Write("Välj något av alternativen ovan: ");

            string userInput = Console.ReadLine()!;

            if (int.TryParse(userInput, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        await CreateCustomerAsync();
                        break;
                    case 2:
                        await ShowAllCustomersAsync();
                        break;
                    case 3:
                        await ShowOneCustomerAsync();
                        break;
                    case 4:
                        await RemoveCustomerAsync();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("\nOgiltigt val. Försök igen.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nOgiltig inmatning. Ange ett heltal.");
            }

            Console.WriteLine("\nTryck på Enter för att fortsätta.");
            Console.ReadLine();
            Console.Clear();

        }

        //Skapa en kund
        public async Task CreateCustomerAsync()
        {
            var form = new CustomerRegistrationForm();

            Console.Clear();
            Console.WriteLine("\nSkriv in kunduppgifterna nedan\n");

            Console.Write("\nFörnamn: ");
            form.FirstName = Console.ReadLine()!;

            Console.Write("\nEfternamn: ");
            form.LastName = Console.ReadLine()!;

            Console.Write("\nE-postadress: ");
            form.Email = Console.ReadLine()!;

            Console.Write("\nGatunamn: ");
            form.StreetName = Console.ReadLine()!;

            Console.Write("\nPostnummer: ");
            form.PostalCode = Console.ReadLine()!;

            Console.Write("\nOrt: ");
            form.City = Console.ReadLine()!;

            Console.Write("\nKundtyp: ");
            form.CustomerType = Console.ReadLine()!;

            var result = await _customerService.CreateCustomerAsync(form);
            if (result)
            {
                Console.WriteLine("Kunden är skapad");
            }

            else
            {
                Console.WriteLine("Det gick inte att skapa kunden");
            }
        }

        //Visa alla kunder
        public async Task ShowAllCustomersAsync()
        {
            Console.Clear();

            var customers = await _customerService.GetAllCustomersAsync();
            foreach (var customer in customers)
            {
                Console.WriteLine("\nAlla kunder:\n");
                Console.WriteLine($"\nNamn:{customer.FirstName} {customer.LastName}");
                Console.WriteLine($"\n Kundinformation: , {customer.Email}, {customer.Phone}");
                Console.WriteLine($"\nAdress:{customer.Address.StreetName}, {customer.Address.PostalCode}, {customer.Address.City}");
                Console.WriteLine($"\nKundTyp:{customer.CustomerType.CustomerTypeName}");
            }

            Console.ReadKey();
        }

        //Visa en kund
        public async Task ShowOneCustomerAsync()
        {
            Console.Clear();
            Console.Write("\nAnge kundens e-postadress: ");
            string email = Console.ReadLine()!;

            var customer = await _customerService.GetCustomersByEmailAsync(email);
            if (customer != null)
            {
                Console.WriteLine($"\nKundens information: {customer.FirstName} {customer.LastName}, {customer.Email}, {customer.Phone}");
                Console.WriteLine($"\nKundens Adress: {customer.Address.StreetName} {customer.Address.PostalCode}, {customer.Address.City}");
                Console.WriteLine($"\nKundtyp: {customer.CustomerType.CustomerTypeName}");
            }
            else
            {
                Console.WriteLine("Kunden kunde inte hittas.");
            }


        }

        //Ta bort en kund
        public async Task RemoveCustomerAsync()
        {
            Console.Clear();
            Console.WriteLine("\n\nAnge kundens e-postadress:\n");
            string email = Console.ReadLine()!;

            var customerToRemove = await _customerService.GetCustomersByEmailAsync(email);

            if (customerToRemove != null)
            {
                Console.WriteLine($"\nInformation för kunden som kommer att tas bort: {customerToRemove.FirstName} {customerToRemove.LastName}, {customerToRemove.Email}, {customerToRemove.Phone}");

                Console.WriteLine("\nÄr du säker på att du vill ta bort denna kund? (Ja/Nej):");
                string confirmation = Console.ReadLine()!;

                if (confirmation?.Trim().Equals("Ja", StringComparison.OrdinalIgnoreCase) == true)
                {
                    await _customerService.RemoveCustomerAsync(email);

                    Console.WriteLine("\nKunden har tagits bort.");
                }
                else
                {
                    Console.WriteLine("\nBorttagning avbruten.");
                }
            }
            else
            {
                Console.WriteLine("\nKunden kunde inte hittas.");
            }

        }

    }
}
