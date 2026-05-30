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
        var busOptions = configuration.GetSection("AwsBusOption").Get<AwsBusOption>()!;

        services.AddMassTransit(configure =>
        {
            if (assembly != null)
            {
                configure.AddConsumers(assembly);
            }

            if (busOptions.Provider.Equals("AmazonSqs", StringComparison.OrdinalIgnoreCase))
            {
                configure.UsingAmazonSqs((ctx, cfg) =>
                {
                    cfg.Host(busOptions.Region, host =>
                    {
                        if (!string.IsNullOrEmpty(busOptions.AccessKey) && !string.IsNullOrEmpty(busOptions.SecretKey))
                        {
                            host.AccessKey(busOptions.AccessKey);
                            host.SecretKey(busOptions.SecretKey);
                        }
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            }
            else
            {
                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            }
        });

        return services;
    }
}