using MicroCore.Payment.Api;
using Microsoft.EntityFrameworkCore;
using MicroCore.Shared.Extensions;
using MicroCore.Payment.Api.Data;
using MicroCore.Bus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddCommonMasstransitExt(builder.Configuration);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });

app.Run();
