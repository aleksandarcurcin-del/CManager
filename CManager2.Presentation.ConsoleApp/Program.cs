using CManager2.Application.Services;
using CManager2.Infrastructure.Repos;
using CManager2.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<ICustomerRepo, CustomerRepo>()
    .AddScoped<MenuController>()
    .BuildServiceProvider();

var controller = services.GetRequiredService<MenuController>();
controller.ShowMenu();