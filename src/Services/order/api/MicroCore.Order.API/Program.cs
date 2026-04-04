using MicroCore.Order.API.Orders;
using MicroCore.Order.Application.Contracts.Repositories;
using MicroCore.Order.Application.Contracts.UnitOfWork;
using MicroCore.Order.Application;
using MicroCore.Order.Persistence.Repositories;
using MicroCore.Order.Persistence.UnitOfWork;
using MicroCore.Order.Persistence;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();


if (app.Environment.IsDevelopment())
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();