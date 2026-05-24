using MicroCore.Catalog.API;
using MicroCore.Catalog.API.Data;
using MicroCore.Catalog.API.Features.Categories;
using Microsoft.EntityFrameworkCore;
using MicroCore.Shared.Extensions;
using MicroCore.Bus;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));

builder.Services.AddDbContext<CatalogDbContext>(opts =>
    opts.UseMySql(builder.Configuration.GetConnectionString("Database"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Database"))));


builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddCommonMasstransitExt(builder.Configuration, typeof(CatalogAssembly).Assembly);

var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });

app.Run();
