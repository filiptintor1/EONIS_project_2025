
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WebShop.Domain.Repositories;

namespace WebShopProject.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));
        services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);
    }
}
