using Assignment_04.Entities;
using Assignment_04.Models;
using Assignment_04.Repositories;
using Assignment_04.Services;

namespace Assignment_04.Menus
{
    public class OrderMenu
    {
        private readonly OrderService _orderService;
        private readonly OrderDetailsService _orderDetailsService;
        private readonly EmployeeRepository _employeeRepo;
        private readonly ProductService _productService;
        private readonly RegionRepository _regionRepo;
        private readonly EmployeeService _employeeService;
        private ShoppingCart _shoppingCart;

        public OrderMenu(OrderService orderService, OrderDetailsService orderDetailsService, EmployeeRepository employeeRepo, ProductService productService, RegionRepository regionRepo, EmployeeService employeeService, ShoppingCart shoppingCart)
        {
            _orderService = orderService;
            _orderDetailsService = orderDetailsService;
            _employeeRepo = employeeRepo;
            _productService = productService;
            _regionRepo = regionRepo;
            _employeeService = employeeService;
            _shoppingCart = shoppingCart;
        }

        public async Task ManageOrder()
        {
            Console.Clear();
            Console.WriteLine("Beställningar");
            Console.WriteLine("--------------------\n\n");
            Console.WriteLine("1. Lägg till en beställning\n");
            Console.WriteLine("2. Visa alla beställningar\n");
            Console.WriteLine("0. Återvänd till huvudmenyn\n");


            Console.Write("Välj något av alternativen ovan: ");

            string userInput = Console.ReadLine()!;

            if (int.TryParse(userInput, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        await CreateOrderAsync();
                        break;
                    case 2:
                        await ShowAllOrdersAsync();
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

        public async Task CreateOrderAsync()
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("Skapa en beställning");
            Console.WriteLine("--------------------");

            Console.Write("Ange den anställdas e-postadress: ");
            var employeeEmail = Console.ReadLine();

            Console.Write("Ange den anställdas förnamn: ");
            var employeeFirstName = Console.ReadLine();

            Console.Write("Ange den anställdas efternamn: ");
            var employeeLastName = Console.ReadLine();

            Console.Write("Ange regionen anställda arbetar i: ");
            var regionName = Console.ReadLine();

            Console.Write("Ange kundens ID ");
            var customerId = int.Parse(Console.ReadLine()!);

            Console.Write("Ange produktens ID: ");
            var productId = int.Parse(Console.ReadLine()!);

            Console.Write("Ange antal: ");
            var quantityInput = Console.ReadLine();
            int quantity;

            if (!int.TryParse(quantityInput, out quantity))
            {
                Console.WriteLine("Ogiltigt val gör ett nytt försök.");
                return;
            }

            // Kontrollera om regionen redan finns
            var regionEntity = await _regionRepo.GetAsync(x => x.RegionName == regionName);

            // Om regionen inte finns, skapa den
            if (regionEntity == null)
            {
                regionEntity = await _regionRepo.CreateAsync(new RegionEntity { RegionName = regionName!});
            }

            // Check if the employee exists, otherwise create a new employee
            var employeeEntity = await _employeeService.GetEmployeeByEmailAsync(employeeEmail!);
            if (employeeEntity == null)
            {
                Console.WriteLine($"Anställda med {employeeEmail} kunde inte hittas. Skapa anställda nedan");

                var newEmployee = await _employeeRepo.CreateAsync (new EmployeeEntity
                {
                    EmployeeFirstName = employeeFirstName!,  
                    EmployeeLastName = employeeLastName!,
                    EmployeeEmail = employeeEmail!,
                    RegionId = regionEntity.Id
              
                });

                employeeEntity = await _employeeRepo.CreateAsync(newEmployee);
            }

            var shoppingCart = new ShoppingCart
            {
                EmployeeEmail = employeeEmail!,
                CustomerId = customerId,
                Items = new List<CartItem>
        {
            new CartItem
            {
                ProductId = productId,
                Quantity = quantity
            }
        }
            };

            // Call the order service to create the order
            await _orderService.CreateOrderAsync(shoppingCart);
            Console.WriteLine("Beställningen är klar.");

        }



        public async Task ShowAllOrdersAsync()
        {
            var orders = await _orderService.GetAllOrderAsync();

            if (orders.Any())
            {
                Console.WriteLine("Alla beställningar:");

                foreach (var order in orders)
                {
                    Console.WriteLine($"BeställningsID: {order.Id}, Datum: {order.OrderDate}, KundID: {order.CustomerId}, Totalpris: {order.TotalAmount}");

                    var orderDetails = await _orderDetailsService.GetOrderDetailsByOrderIdAsync(order.Id);

                    foreach (var orderDetail in orderDetails)
                    {
                        Console.WriteLine($"  Produkt: {orderDetail.ProductId}, Antal: {orderDetail.Quantity}, Pris: {orderDetail.UnitPrice}");
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Inga beställningar hittades.");
            }
        }


    }
}
