using CManager2.Domain.Models;

namespace CManager2.Infrastructure.Repos;

public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();
    bool SaveCustomers(List<CustomerModel> customers);
}

