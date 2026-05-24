using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace MicroCore.Bus;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddCommonMasstransitExt(this IServiceCollection services,
        IConfiguration configuration, Assembly? assembly = null)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

        services.AddMassTransit(configure =>
        {
            if (assembly != null)
            {
                configure.AddConsumers(assembly);
            }

            configure.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}