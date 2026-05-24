using MicroCore.Discount.Grpc.Data;
using MicroCore.Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseMySql(builder.Configuration.GetConnectionString("Database"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database"))));

builder.Services.AddGrpc();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    dbContext.Database.Migrate();
}

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
