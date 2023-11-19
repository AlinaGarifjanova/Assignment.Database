using Assignment_04.Entities;
using Assignment_04.Models;
using Assignment_04.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Assignment_04.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepo;
    private readonly AddressRepository _addressRepo;
    private readonly CustomerTypeRepository _customerTypeRepo;

    public CustomerService(CustomerRepository customerRepo, AddressRepository addressRepo, CustomerTypeRepository customerTypeRepo)
    {
        _customerRepo = customerRepo;
        _addressRepo = addressRepo;
        _customerTypeRepo = customerTypeRepo;
    }


    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (!await _customerRepo.ExistsAsync(x => x.Email == form.Email))
        {
            AddressEntity addressEntity = await _addressRepo.GetAsync(x => x.StreetName == form.StreetName && x.PostalCode == form.PostalCode);
            addressEntity ??= await _addressRepo.CreateAsync(new AddressEntity { StreetName = form.StreetName, PostalCode = form.PostalCode, City = form.City });

            CustomerTypeEntity customerTypeEntity = await _customerTypeRepo.GetAsync(x => x.CustomerTypeName == form.CustomerType);
            customerTypeEntity ??= await _customerTypeRepo.CreateAsync(new CustomerTypeEntity { CustomerTypeName = form.CustomerType });

            if (customerTypeEntity != null)
            {
                CustomerEntity customerEntity = await _customerRepo.CreateAsync(
                    new CustomerEntity
                    {
                        FirstName = form.FirstName,
                        LastName = form.LastName,
                        Email = form.Email,
                        AddressId = addressEntity.Id,
                        CustomerTypeId = customerTypeEntity.Id
                    });

                if (customerEntity != null)
                    return true;
            }
        }

        return false;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        var customers = await _customerRepo.GetAllAsync();
        return customers;
    }

    public async Task<CustomerEntity> GetCustomersByEmailAsync(string email)
    {
        try
        {
            Expression<Func<CustomerEntity, bool>> expression = (x => x.Email == email);

            var customer = await _customerRepo.GetAsync(expression);

            if (customer != null)
                return customer;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }

   
    public async Task<bool> RemoveCustomerAsync(string email)
    {
        try
        {
            if (await _customerRepo.ExistsAsync(x => x.Email == email))
            {
                return await _customerRepo.DeleteAsync(x => x.Email == email);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }

}
