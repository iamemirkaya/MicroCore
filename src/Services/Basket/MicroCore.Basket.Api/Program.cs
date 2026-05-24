using MicroCore.Basket.Api.Data;
using Microsoft.EntityFrameworkCore;
using MicroCore.Shared.Extensions;
using MicroCore.Basket.Api;
using MicroCore.Basket.Api.Features.Baskets;
using MicroCore.Bus;
using MicroCore.Basket.Api.Contracts.Refit;
using MicroCore.Shared.Options;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddScoped<BasketService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});


builder.Services.AddDbContext<BasketDbContext>(opts =>
    opts.UseMySql(builder.Configuration.GetConnectionString("Database"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database"))));

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddRefitConfigurationExt(builder.Configuration);

builder.Services.AddCommonMasstransitExt(builder.Configuration, typeof(BasketAssembly).Assembly);

builder.Services.AddGrpcClient<MicroCore.Discount.Grpc.DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    var discountUrl = builder.Configuration["GrpcSettings:DiscountUrl"];
    options.Address = new Uri(discountUrl!);
});

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
