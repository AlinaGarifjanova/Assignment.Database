using Assignment_04.Entities;
using Assignment_04.Models;
using Assignment_04.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Services
{
    public class ProductService
    {
        // Repositories för att hantera dataåtkomst för produkter, kategorier och leverantörer
        private readonly ProductRepository _productRepo;
        private readonly CategoryRepository _categoryRepo;
        private readonly SupplierRepository _supplierRepo;

        // Konstruktor som tar emot repositories som parametrar och tilldelar dem till privata fält
        public ProductService(ProductRepository productRepo, CategoryRepository categoryRepo, SupplierRepository supplierRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _supplierRepo = supplierRepo;
        }

        // Metod för att skapa en produkt baserat på inkommande formulärdata
        public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
         {
             if (!await _productRepo.ExistsAsync(x => x.ProductName == form.ProductName))
             {

                 // Kontrollera ifall Kategorin finns om inte skapa en ny
                 var categoryEntity = await _categoryRepo.GetAsync(x => x.CategoryName == form.ProductCategory);
                 categoryEntity ??= await _categoryRepo.CreateAsync(new CategoryEntity { CategoryName = form.ProductCategory });

                 // Kontrollera ifall Leverantören finns om inte skapa en ny
                 var supplierEntity = await _supplierRepo.GetAsync(x => x.SupplierName == form.SupplierName && x.Phone == form.SupplierPhone);
                 supplierEntity ??= await _supplierRepo.CreateAsync(new SupplierEntity
                 {
                     SupplierName = form.SupplierName,
                     ContactName = form.ContactName,
                     Phone = form.SupplierPhone,
                     Email = form.SupplierEmail,
                     Address = form.SupplierAddress
                 });

                 // Skapa produkten
                 var productEntity = await _productRepo.CreateAsync(new ProductEntity
                 {
                     ProductName = form.ProductName,
                     ProductDescription = form.ProductDescription,
                     ProductPrice = form.ProductPrice,
                     CategoryId = categoryEntity.Id,
                     SupplierId = supplierEntity.Id
                 });

                 if (productEntity != null)
                     return true;
             }
             
             return false;
         }

        // Hämta alla produkter från databasen
        public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetAllAsync();
            return products;
        }

        // Hämta en produkt baserat på produktnamnet
        public async Task<ProductEntity> GetProductAsync(string productName)
        {
            try
            {
                // Skapa ett uttryck för att söka efter produkten baserat på produktnamnet
                Expression<Func<ProductEntity, bool>> expression = (x => x.ProductName == productName);

                var product = await _productRepo.GetAsync(expression);

                if (product != null)
                    return product;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;

        }

        // Ta bort en produkt baserat på produktnamnet
        public async Task<bool> RemoveProductAsync(string productName)
        {
            try
            {
                if (await _productRepo.ExistsAsync(x => x.ProductName == productName))
                {
                    return await _productRepo.DeleteAsync(x => x.ProductName == productName);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
           
        }

        // Hämta alla produkter för en given kategori, används egentligen inte 
        public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryAsync(string categoryName)
        {
            return await _productRepo.GetProductsByCategoryAsync(categoryName);
        }
    }
}

