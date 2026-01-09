using CManager2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager2.Application.Services;

public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);

    // Tagit hjälp av GPT
    CustomerModel? GetCustomerByEmail(string email, out bool hasError);
    bool DeleteCustomerByEmail(string email, out bool hasError);
}
