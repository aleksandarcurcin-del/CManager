using CManager2.Application.Services;
using CManager2.Presentation.ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace CManager2.Presentation.ConsoleApp.Controllers;

public class MenuController
{
    private readonly ICustomerService _customerService;

    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void ShowMenu()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. View Specific customer");
            Console.WriteLine("4. Remove specific customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");


            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "3":
                    ViewSpecificCustomer();
                    break;

                case "4":
                    RemoveSpecificCustomer();
                    break;

                case "0":
                    return;

                default:
                    OutputDialog("Invalid option! Press any key to continue...");
                    break;
            }
        }

    }



    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");

        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("PostalCode", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("Customer created");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again");
        }
        OutputDialog("Press any key to continue...");
    }



    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All Customers");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine("---------------------------");
            }
        }

        OutputDialog("Press any key to continue...");
    }


    // Tagit hjälp av GPT
    private void ViewSpecificCustomer()
    {
        Console.Clear();
        Console.WriteLine("View Specific Customer");

        Console.WriteLine("Enter Customer email: ");
        var email = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(email))
        {
            OutputDialog("Email is required. Press any key to continue...");
            return;
        }

        var customers = _customerService.GetCustomerByEmail(email, out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
            return;
        }

        if (customers == null)
        {
            Console.WriteLine("Customer not found, try again");
            return;
        }
        else
        {
            Console.WriteLine($"Name: {customers.FirstName} {customers.LastName}");
            Console.WriteLine($"ID: {customers.Id}");
            Console.WriteLine($"Phone: {customers.PhoneNumber}");
            Console.WriteLine($"Email: {customers.Email}");
            Console.WriteLine($"Address: {customers.Address.StreetAddress} {customers.Address.PostalCode} {customers.Address.City}");
            Console.WriteLine("---------------------------");
        }

        OutputDialog("Press any key to continue...");

    }

    private void RemoveSpecificCustomer()
    {
        Console.Clear();
        Console.WriteLine("Remove Specific Customer");

        Console.WriteLine("Enter Customer email: ");
        var email = Console.ReadLine()!;

        if (string.IsNullOrWhiteSpace(email))
        {
            OutputDialog("Email is required. Press any key to continue...");
            return;
        }

        var customerDeleted = _customerService.DeleteCustomerByEmail(email, out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
            return;
        }

        if (!customerDeleted)
        {
            Console.WriteLine("Customer not found, try again");
            return;
        }
        else
        {
            Console.WriteLine("Customer deleted successfully");
        }

        OutputDialog("Press any key to continue...");
    }


    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }





}
