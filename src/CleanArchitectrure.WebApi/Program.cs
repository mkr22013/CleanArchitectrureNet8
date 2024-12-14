using CleanArchitectrure.Application.UseCases;
using CleanArchitectrure.Persistence;
using CleanArchitectrure.WebApi.Extensions.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add methods Extensions
builder.Services.AddInjectionPersistence();
builder.Services.AddInjectionApplication();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Details API", Version = "v1" });
});
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Details API With CQRS Pattern V.1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddMiddleware();

app.MapControllers();

app.Run();
