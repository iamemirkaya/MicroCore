using MicroCore.Order.Application.Contracts.Refit.PaymentService;
using MicroCore.Shared.Options; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace MicroCore.Order.Application.Contracts.Refit;

public static class RefitConfiguration
{
    public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuthenticatedHttpClientHandler>();

        services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
        {
            var addressUrlOption = configuration.GetSection("AddressUrlOption").Get<PaymentAddressUrlOption>();

            configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
        }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

        return services;
    }
}
