using MicroCore.Order.Application.Contracts.Repositories;
using MicroCore.Order.Application.Contracts.UnitOfWork;
using MicroCore.Order.Persistence.Repositories;
using MicroCore.Order.Persistence.UnitOfWork;
using MicroCore.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using MicroCore.Bus;
using MicroCore.Shared.Extensions;
using MicroCore.Order.Application.Contracts.Refit;
using MicroCore.Order.API.Orders;
using MicroCore.Order.Application.Orders.CreateOrder;
using MicroCore.Order.Application;




var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseMySql(builder.Configuration.GetConnectionString("Database"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database"))));


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddRefitConfigurationExt(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.AddOrderGroupEndpointExt();
app.UseAuthentication();
app.UseAuthorization();

app.Run();