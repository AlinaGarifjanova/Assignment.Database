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
    public class OrderService
    {
        private readonly OrderRepository _orderRepo;
        private readonly CustomerRepository _customerRepo;
        private readonly ProductRepository _productRepo;
        private readonly OrderDetailsRepository _orderDetailsRepo;
        private readonly EmployeeRepository _employeeRepo;
        private readonly RegionRepository _regionRepo;
        private readonly EmployeeService _employeeService;

        public OrderService(OrderRepository orderRepo, CustomerRepository customerRepo, ProductRepository productRepo, OrderDetailsRepository orderDetailsRepo, EmployeeRepository employeeRepo, RegionRepository regionRepo, EmployeeService employeeService)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _productRepo = productRepo;
            _orderDetailsRepo = orderDetailsRepo;
            _employeeRepo = employeeRepo;
            _regionRepo = regionRepo;
            _employeeService = employeeService;
        }
       /* public async Task<bool> CreateOrderAsync(ShoppingCart shoppingCart)
        {
            try
            {
                // Check if an employee with the given email exists
                var existingEmployee = await _employeeService.GetEmployeeByEmailAsync(shoppingCart.EmployeeEmail);

                if (existingEmployee == null)
                {
                    // Create a new employee if not found
                    var employeeForm = new EmployeeRegistrationForm
                    {
                        EmployeeFirstName = shoppingCart.EmployeeFirstName,
                        EmployeeLastName = shoppingCart.EmployeeLastName,
                        EmployeeEmail = shoppingCart.EmployeeEmail,
                        // ... other employee details
                    };

                    existingEmployee = await _employeeService.CreateEmployeeAsync(employeeForm);
                }

                // Continue with creating the order
                var customerEntity = await _customerRepo.GetAsync(x => x.Id == shoppingCart.CustomerId);

                if (customerEntity != null)
                {
                    var orderEntity = await _orderRepo.CreateAsync(new OrderEntity()
                    {
                        OrderDate = DateTime.Now,
                        CustomerId = customerEntity.Id,
                        EmployeeId = existingEmployee.Id,
                    });

                    if (orderEntity != null)
                    {
                        foreach (var item in shoppingCart.Items)
                        {
                            var productEntity = await _productRepo.GetAsync(x => x.Id == item.ProductId);

                            if (productEntity != null)
                            {
                                await _orderDetailsRepo.CreateAsync(new OrderDetailsEntity
                                {
                                    OrderId = orderEntity.Id,
                                    ProductId = productEntity.Id,
                                    UnitPrice = item.UnitPrice,
                                    Quantity = item.Quantity,
                                });

                                orderEntity.TotalAmount += (item.UnitPrice * item.Quantity);
                            }
                        }

                        await _orderRepo.UpdateAsync(orderEntity);
                        return true;
                    }
                }

                return false; // Order creation failed
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false; // Handle exceptions and return false if something goes wrong
            }
        }*/



         public async Task CreateOrderAsync(ShoppingCart shoppingCart)
         {
            var employeeEntity = await _employeeRepo.GetAsync(x => x.EmployeeEmail == shoppingCart.EmployeeEmail);
            if (employeeEntity == null!) 
            {
                 employeeEntity ??= await _employeeRepo.CreateAsync(new EmployeeEntity
                 {
                     EmployeeFirstName = shoppingCart.EmployeeFirstName,
                     EmployeeLastName = shoppingCart.EmployeeLastName,
                     EmployeeEmail = shoppingCart.EmployeeEmail
                 });


                 if (employeeEntity != null)
                 {
                     var customerEntity = await _customerRepo.GetAsync(x => x.Id == shoppingCart.CustomerId);
                     if (customerEntity != null)
                     {
                         var orderEntity = await _orderRepo.CreateAsync(new OrderEntity()
                         {
                             OrderDate = DateTime.Now,
                             CustomerId = customerEntity.Id,
                             EmployeeId = employeeEntity.Id,
                         });

                         if (orderEntity != null)
                         {
                             foreach (var item in shoppingCart.Items)
                             {
                                 var productEntity = await _productRepo.GetAsync(x => x.Id == item.ProductId);

                                 if (productEntity != null)
                                 {
                                     await _orderDetailsRepo.CreateAsync(new OrderDetailsEntity
                                     {
                                         OrderId = orderEntity.Id,
                                         ProductId = productEntity.Id,
                                         UnitPrice = productEntity.ProductPrice,
                                         Quantity = item.Quantity,
                                     });

                                     orderEntity.TotalAmount += (productEntity.ProductPrice * item.Quantity);
                                 }
                             }

                             await _orderRepo.UpdateAsync(orderEntity);
                         }
                 }
                }
            }
         }


        /*public async Task CreateOrderAsync(ShoppingCart shoppingCart)
    {
      var customerEntity = await _customerRepo.GetAsync(x => x.Id == shoppingCart.CustomerId);
      if (customerEntity != null)
      {
          EmployeeEntity employeeEntity = await GetOrCreateEmployeeAsync(shoppingCart.EmployeeEmail);
          var orderEntity = await _orderRepo.CreateAsync(new OrderEntity()
          {
              OrderDate = DateTime.Now,
              CustomerId = customerEntity.Id,
              EmployeeId = employeeEntity.Id
          });

          if (orderEntity != null)
          {
              foreach (var item in shoppingCart.Items)
              {
                  var productEntity = await _productRepo.GetAsync(x => x.Id == item.ProductId);

                  if (productEntity != null)
                  {
                      await _orderDetailsRepo.CreateAsync(new OrderDetailsEntity
                      {
                          OrderId = orderEntity.Id,
                          ProductId = productEntity.Id,
                          UnitPrice = productEntity.ProductPrice,
                          Quantity = item.Quantity,
                      });

                      orderEntity.TotalAmount += (productEntity.ProductPrice * item.Quantity);
                  }
              }

              await _orderRepo.UpdateAsync(orderEntity);
          }
      }
    }*/



        /*public async Task CreateOrderAsync(ShoppingCart shoppingCart)
        {
            var customerEntity = await _customerRepo.GetAsync(x => x.Id == shoppingCart.CustomerId);
            if (customerEntity != null)
            {
                EmployeeEntity employeeEntity = await GetOrCreateEmployeeAsync(new EmployeeRegistrationForm { EmployeeEmail = shoppingCart.EmployeeEmail});
                var orderEntity = await _orderRepo.CreateAsync(new OrderEntity()
                {
                    OrderDate = DateTime.Now,
                    CustomerId = customerEntity.Id,
                    EmployeeId = employeeEntity.Id
                });

                if (orderEntity != null)
                {
                    foreach (var item in shoppingCart.Items)
                    {
                        var productEntity = await _productRepo.GetAsync(x => x.Id == item.ProductId);

                        if (productEntity != null)
                        {
                            await _orderDetailsRepo.CreateAsync(new OrderDetailsEntity
                            {
                                OrderId = orderEntity.Id,
                                ProductId = productEntity.Id,
                                UnitPrice = productEntity.ProductPrice,
                                Quantity = item.Quantity,
                            });

                            orderEntity.TotalAmount += (productEntity.ProductPrice * item.Quantity);
                        }
                    }

                    await _orderRepo.UpdateAsync(orderEntity);
                }
            }
        */

        public async Task<EmployeeEntity> GetOrCreateEmployeeAsync(EmployeeRegistrationForm form)
        {

            // Försök att hämta en befintlig anställd
            var existingEmployee = await _employeeRepo.GetAsync(x => x.EmployeeEmail == form.EmployeeEmail);

            if (existingEmployee != null)
            {
                return existingEmployee!;
            }
            else
            {

                // Om ingen befintlig anställd hittas, skapa en ny
                var newEmployee = await _employeeRepo.CreateAsync(new EmployeeEntity
                {
                    EmployeeFirstName = form.EmployeeFirstName,
                    EmployeeLastName = form.EmployeeLastName,
                    EmployeeEmail = form.EmployeeEmail,
                    EmployeePhone = form.EmployeePhone,

                });

                return newEmployee;
            }

        }

        public async Task<IEnumerable<OrderEntity>> GetAllOrderAsync()
        {
            try
            {
                var orders = await _orderRepo.GetAllAsync();
                return orders;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return Enumerable.Empty<OrderEntity>();

        }

        public async Task<OrderEntity> GetOrderByIdAsync(int orderId)
        {
            try
            {
                Expression<Func<OrderEntity, bool>> expression = (x => x.Id == orderId);

                var order = await _orderRepo.GetAsync(expression);

                if (order != null)
                    return order!;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null!;

        }

    }
}
