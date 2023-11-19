using Assignment_04.Contexts;
using Assignment_04.Menus;
using Assignment_04.Models;
using Assignment_04.Repositories;
using Assignment_04.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_04
{
   public static class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Skolan\Databasteknik\Repetion\Assignment_04\Assignment_04\Assignment_04\Contexts\Assingment_04_local_db.mdf;Integrated Security=True;Connect Timeout=30"));

            services.AddScoped<AddressRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<CustomerTypeRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<OrderDetailsRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<RegionRepository>();
            services.AddScoped<SupplierRepository>();

            services.AddScoped<CustomerService>();
            services.AddScoped<EmployeeService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
            services.AddScoped<OrderDetailsService>();
            services.AddScoped<ShoppingCart>();

            services.AddScoped<CustomerMenu>();
            services.AddScoped<EmployeeMenu>();
            services.AddScoped<MainMenu>();
            services.AddScoped<OrderMenu>();
            services.AddScoped<ProductMenu>();


            using var sp = services.BuildServiceProvider();
            var mainMenu = sp.GetRequiredService<MainMenu>();
            await mainMenu.Run();
        }
    }
}