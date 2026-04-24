using MicroCore.Basket.Api.Data;
using Microsoft.EntityFrameworkCore;
using MicroCore.Shared.Extensions;
using MicroCore.Basket.Api;
using MicroCore.Basket.Api.Features.Baskets;
using MicroCore.Bus;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddScoped<BasketService>();
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddDbContext<BasketDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));



var app = builder.Build();

app.UseExceptionHandler(x => { });

app.AddBasketGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () => "Hello World!");

app.Run();
