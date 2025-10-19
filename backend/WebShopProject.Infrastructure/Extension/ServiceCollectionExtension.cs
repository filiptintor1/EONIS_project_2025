using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShop.Infrastructure.Repositories;
using WebShop.Infrastructure.Seeders;
using WebShopProject.Domain.Authorization;
using WebShopProject.Domain.Repositories;
using WebShopProject.Infrastructure.Authorization;
using WebShopProject.Infrastructure.Database;
using WebShopProject.Infrastructure.Repositories;


namespace WebShopProject.Infrastructure.Extension;

public static class ServiceCollectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WebShopProjectDb");
        services.AddDbContext<ProjectDbContext>(options =>
        options
        .UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());

        services.AddScoped<IDataSeeder, DataSeeder>();

        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IAuthorizationHelper,AuthorizationHelper>();

    }

}
