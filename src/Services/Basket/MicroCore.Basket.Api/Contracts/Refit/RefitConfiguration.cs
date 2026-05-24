using MicroCore.Basket.Api.Contracts.Refit.OrderService;
using MicroCore.Shared.Options;
using Microsoft.Extensions.Options;
using Refit;

namespace MicroCore.Basket.Api.Contracts.Refit;


public static class RefitConfiguration
{
    public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuthenticatedHttpClientHandler>();
        services.AddRefitClient<IOrderService>().ConfigureHttpClient(configure =>
        {
            var addressUrlOption = configuration.GetSection("AddressUrlOption").Get<OrderAddressUrlOption>();


            configure.BaseAddress = new Uri(addressUrlOption!.OrderUrl);
        }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


        return services;
    }
}