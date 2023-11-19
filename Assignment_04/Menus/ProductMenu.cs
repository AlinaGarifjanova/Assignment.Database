using Assignment_04.Entities;
using Assignment_04.Models;
using Assignment_04.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Menus
{
    public class ProductMenu
    {
        private readonly ProductService _productService;

        public ProductMenu(ProductService productService)
        {
            _productService = productService;
        }

        // Hantera produktmenyn
        public async Task ManageProducts()
        {
            Console.Clear();
            Console.WriteLine("Produkter");
            Console.WriteLine("--------------------");
            Console.WriteLine("1. Lägg till en produkt\n");
            Console.WriteLine("2. Visa alla produkter\n");
            Console.WriteLine("3. Visa en produkt\n");
            Console.WriteLine("4. Ta bort en produkt\n");
            Console.WriteLine("0. Avsluta\n");

            Console.Write("Välj något av ovanstående valen: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateProductAsync();
                    break;

                case "2":
                    await ShowAllProductsAsync();
                    break;

                case "3":
                    await ShowOneProductAsync();
                    break;

                case "4":
                    await RemoveProductAsync();
                    break;
                case "0":
                    return;

                default:
                    Console.WriteLine("Du gjorde ett ogiltigt val");
                    break;

            }

        }
        // Skapa en ny produkt
        public async Task CreateProductAsync()
        {
            var form = new ProductRegistrationForm();

            Console.Clear();
            Console.WriteLine("\nSkapa en produkt:\n");
            Console.Write("Produktens namn : ");
            form.ProductName = Console.ReadLine()!;

            Console.Write("\nProduktens beskrivning: ");
            form.ProductDescription = Console.ReadLine()!;

            Console.Write("\nProduktens kategori: ");
            form.ProductCategory = Console.ReadLine()!;

            Console.Write("\nProduct Price (SEK): ");
            form.ProductPrice = decimal.Parse(Console.ReadLine()!);

            Console.Write("\nAntal i lager: ");
            form.UnitsInStock = Console.ReadLine()!;

            Console.Write("\nLeverantören namn: ");
            form.SupplierName = Console.ReadLine()!;

            Console.Write("\nLeverantörens kontaktperson: ");
            form.ContactName = Console.ReadLine()!;

            Console.Write("\nLeverantörens telefonnummer: ");
            form.SupplierEmail = Console.ReadLine()!;

            Console.Write("\nLeverantörens e-postadress: ");
            form.SupplierEmail = Console.ReadLine()!;

            Console.Write("\nLeverantörens adress: ");
            form.SupplierAddress = Console.ReadLine()!;


            var result = await _productService.CreateProductAsync(form);
            if (result)
                Console.WriteLine("\n\nProdukten är skapad.");
            else
                Console.WriteLine("\n\nKunde inte skapa produkten");

            Console.ReadKey();

        }

        // Visa alla produkter
        public async Task ShowAllProductsAsync()
        {
            Console.Clear();

            var products = await _productService.GetAllProductsAsync();

            foreach (var product in products)
            {
                Console.WriteLine($"\n{product.ProductName} ({product.Category.CategoryName})\n");
                Console.WriteLine($"\n{product.ProductPrice} ,{product.UnitsInStock}\n");
                Console.WriteLine($"\n{product.Supplier.SupplierName}, {product.Supplier.ContactName}, {product.Supplier.Phone}, {product.Supplier.Email}, {product.Supplier.Address} ");
                Console.WriteLine("----------------------------------------------");

            }
            Console.ReadKey();

        }

        // Visa information för en specifik produkt
        public async Task ShowOneProductAsync()
        {
            Console.Clear();
            Console.Write("\nAnge produktens namn: \n");
            string productName = Console.ReadLine()!;

            var product = await _productService.GetProductAsync(productName);
            if (productName != null)
            {
                Console.WriteLine($"\nProduktens information: {product.ProductName}, {product.ProductPrice}, {product.UnitsInStock}, {product.ProductDescription}");
                Console.WriteLine($"\nLeverantör: {product.Supplier.SupplierName},{product.Supplier.ContactName},{product.Supplier.Email} ,{product.Supplier.Phone}");
                Console.WriteLine($"\nKategorin: {product.Category.CategoryName}");
            }
            else
            {
                Console.WriteLine("Produkten kunde inte hittas.");
            }


        }

        // Ta bort en produkt
        public async Task RemoveProductAsync()
        {
            Console.Clear();
            Console.Write("Ange produktens namn som du vill ta bort: ");
            string productName = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(productName))
            {
                var existingProduct = await _productService.GetProductAsync(productName);

                if (existingProduct != null)
                {
                    Console.WriteLine($"Är du säker på att du vill ta bort produkten med namnet {productName}? (ja/nej)");
                    var confirmation = Console.ReadLine();

                    if (confirmation?.ToLower() == "ja")
                    {
                        var result = await _productService.RemoveProductAsync(productName);

                        if (result)
                            Console.WriteLine($"Produkten med namnet {productName} har tagits bort.");
                        else
                            Console.WriteLine($"Kunde inte ta bort produkten med namnet {productName}.");
                    }
                    else
                    {
                        Console.WriteLine($"Produkten med namnet {productName} har inte tagits bort.");
                    }
                }
                else
                {
                    Console.WriteLine($"Produkten med namnet {productName} hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning. Ange ett giltigt produktnamn.");
            }

            Console.WriteLine("Tryck på valfri tangent för att fortsätta.");
            Console.ReadKey();

        }
    }
}
