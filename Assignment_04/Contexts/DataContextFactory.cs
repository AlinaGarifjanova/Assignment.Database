using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Contexts
{
    // En Factory-klass som implementerar IDesignTimeDbContextFactory för design-tid skapande av DbContext
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        // Metod som används vid design-tid för att skapa en instans av DataContext
        public DataContext CreateDbContext(string[] args)
        {
            // Skapa en DbContextOptionsBuilder för att konfigurera inställningarna för DbContext
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            // Ange anslutningssträngen för att ansluta till databasen (lokalt i detta fall)
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Skolan\Databasteknik\Repetion\Assignment_04\Assignment_04\Assignment_04\Contexts\Assingment_04_local_db.mdf;Integrated Security=True;Connect Timeout=30");

            // Skapa en ny instans av DataContext med de konfigurerade inställningarna
            return new DataContext(optionsBuilder.Options);
        }
    }
}
