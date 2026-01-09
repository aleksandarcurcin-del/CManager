using CManager2.Application.Services;
using CManager2.Domain.Models;
using CManager2.Infrastructure.Repos;
using Moq;


namespace CManager2.Tests.ServiceTests;

public class CustomerServiceTest
{
    [Fact]
    public void DeleteCustomer_WithEmptyEmail_ReturnsFalse()
    {
        // Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(new List<CustomerModel>());

        var service = new CustomerService(mockCustomerRepo.Object);

        // Act
        bool hasError;
        var result = service.DeleteCustomerByEmail(string.Empty, out hasError);

        // Assert
        Assert.False(result);               // metoden ska returnera false
        Assert.False(hasError);             // inget fel ska ha markerats
        mockCustomerRepo.Verify(r => r.GetAllCustomers(), Times.Never);


    }


    [Fact]
    public void DeleteCustomer_WhenCustomerExists_ReturnsTrue()
    {
        // Arrange
        var testCustomer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = "test",
            LastName = "testsson",
            Email = "test@domain.com",
            PhoneNumber = "1234567890",
            Address = new AddressModel
            {
                StreetAddress = "Street",
                City = "City",
                PostalCode = "12345",
            }
        };

        var testCustomers = new List<CustomerModel> { testCustomer };

        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns(testCustomers);
        mockCustomerRepo.Setup(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>())).Returns(true);

        var service = new CustomerService(mockCustomerRepo.Object);

        // Act
        bool hasError;
        var result = service.DeleteCustomerByEmail(testCustomer.Email, out hasError);

        // Assert
        Assert.True(result);
        mockCustomerRepo.Verify(r => r.SaveCustomers(It.IsAny<List<CustomerModel>>()), Times.Once);
    }



    [Fact]
    public void DeleteCustomer_WhenRepositoryReturnsNull_ReturnsFalse()
    {
        // Arrange
        var mockCustomerRepo = new Mock<ICustomerRepo>();
        mockCustomerRepo.Setup(r => r.GetAllCustomers()).Returns((List<CustomerModel>)null!);

        var service = new CustomerService(mockCustomerRepo.Object);

        // Act
        bool hasError;
        var result = service.DeleteCustomerByEmail("test@domain.com", out hasError);

        // Assert
        Assert.False(result);
    }
}
