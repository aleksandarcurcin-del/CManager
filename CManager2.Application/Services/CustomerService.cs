using CManager2.Domain.Models;
using CManager2.Infrastructure.Repos;
using CManager2.Application.Services;

namespace CManager2.Application.Services;

public class CustomerService(ICustomerRepo customerRepo) : ICustomerService
{

    private readonly ICustomerRepo _customerRepo = customerRepo;

    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            }
        };

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }



    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            return customers;
        }
        catch (Exception)
        {
            // här kommer throw hamna från customerrepo - getallcustomers
            hasError = true;
            return [];

        }

    }


    // Tagit hjälp av GPT 
    public CustomerModel? GetCustomerByEmail(string email, out bool hasError)
    {
        hasError = false;

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            return customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        catch (Exception)
        {
            hasError = true;
            return null;
        }
    }

    public bool DeleteCustomerByEmail(string email, out bool hasError)
    {
        hasError = false;

        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (customer == null)
                return false;

            customers.Remove(customer);
            return _customerRepo.SaveCustomers(customers);

        }
        catch (Exception)
        {
            hasError = true;
            return false;
        }
    }

}
