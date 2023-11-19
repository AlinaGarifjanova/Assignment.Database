
namespace Assignment_04.Menus
{
    public class MainMenu
    {
        private readonly CustomerMenu _customerMenu;
        private readonly OrderMenu _orderMenu;
        private readonly EmployeeMenu _employeeMenu;
        private readonly ProductMenu _productMenu;


        public MainMenu(CustomerMenu customerMenu, OrderMenu orderMenu, EmployeeMenu employeeMenu, ProductMenu productMenu) //SupplierMenu supplierMenu)
        {
            _customerMenu = customerMenu;
            _orderMenu = orderMenu;
            _employeeMenu = employeeMenu;
            _productMenu = productMenu;

        }

        public async Task Run()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Välkommen till Kobra Sales");
                Console.WriteLine("--------------------\n\n");
                Console.WriteLine("1. Produkter\n");
                Console.WriteLine("2. Kunder\n");
                Console.WriteLine("3. Anställda\n");
                Console.WriteLine("4. Beställningar\n");
                Console.WriteLine("0. Avsluta\n");

                Console.Write("Välj ett av alternativen ovan: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await _productMenu.ManageProducts();
                        break;
                    case "2":
                        await _customerMenu.ManageCustomers();
                        break;
                    case "3":
                        await _employeeMenu.ManageEmployees();
                        break;
                    case "4":
                        await _orderMenu.ManageOrder();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Du gjorde ett ogiltigt val");
                        break;
                }

            } while (true);
        }
    }

}
