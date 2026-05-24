using MicroCore.Payment.Api;
using MicroCore.Payment.Api.Data;
using MicroCore.Payment.Api.Feature.Create;       
using MicroCore.Shared.Extensions;
using MicroCore.Bus;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));

builder.Services.AddDbContext<PaymentDbContext>(opts =>
    opts.UseMySql(builder.Configuration.GetConnectionString("Database"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database"))));

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddCommonMasstransitExt(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });


app.UseAuthentication(); 
app.UseAuthorization();     

app.MapGroup("api/payments")
   .CreatePaymentGroupItemEndpoint();


app.Run();