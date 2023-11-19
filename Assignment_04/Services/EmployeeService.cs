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

namespace Assignment_04.Services;
public class EmployeeService
{
    private readonly EmployeeRepository _employeeRepo;
    private readonly RegionRepository _regionRepo;

    // Konstruktor som tar emot två repository som beroenden för att hantera Employee- och Region-entiteter
    public EmployeeService(EmployeeRepository employeeRepo, RegionRepository regionRepo)
    {
        _employeeRepo = employeeRepo;
        _regionRepo = regionRepo;
    }

    // Metod för att skapa en ny Employee-entitet
    public async Task<EmployeeEntity> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        try
        {
            // Kontrollera om det redan finns en anställd med samma e-postadress
            if (!await _employeeRepo.ExistsAsync(x => x.EmployeeEmail == form.EmployeeEmail))
            {
                // Hämta befintlig eller skapa en ny RegionEntity baserat på formuläret
                RegionEntity regionEntity = await _regionRepo.GetAsync(x => x.RegionName == form.RegionName);
                regionEntity ??= await _regionRepo.CreateAsync(new RegionEntity { RegionName = form.RegionName! });

                // Skapa en ny EmployeeEntity baserat på formuläret och den associerade RegionEntity
                EmployeeEntity employeeEntity = await _employeeRepo.CreateAsync(
                    new EmployeeEntity
                    {
                        EmployeeFirstName = form.EmployeeFirstName,
                        EmployeeLastName = form.EmployeeLastName,
                        EmployeeEmail = form.EmployeeEmail,
                        EmployeePhone = form.EmployeePhone,
                        RegionId = regionEntity.Id,
                    });

                if (employeeEntity != null)
                    return employeeEntity;
            }

            return null!; 
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
         
            throw;
        }
    }

    // Metod för att hämta alla anställda från databasen
    public async Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepo.GetAllAsync();
        return employees;
    }

    // Metod för att hämta en anställd baserat på e-postadress
    public async Task<EmployeeEntity> GetEmployeeByEmailAsync(string email)
    {
        try
        {
            Expression<Func<EmployeeEntity, bool>> expression = (x => x.EmployeeEmail == email);

            var employee = await _employeeRepo.GetAsync(expression);

            if (employee != null)
                return employee;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }

    // Metod för att ta bort en anställd baserat på e-postadress
    public async Task<bool> RemoveEmployeeAsync(string email)
    {
        if (await _employeeRepo.ExistsAsync(x => x.EmployeeEmail == email))
        {
            return await _employeeRepo.DeleteAsync(x => x.EmployeeEmail == email);
        }
        return false;

    }


}


