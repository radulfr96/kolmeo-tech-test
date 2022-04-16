using Application.Queries.GetProductQuery;
using Domain;
using KolmeoTestAPI;
using KolmeoTestAPI.Controllers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using UnitOfWork;
using UnitOfWork.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(typeof(GetProductQuery));
builder.Services.AddDbContext<KolmeoContext>(options => options.UseInMemoryDatabase("KolmeoTestDatabase"));
builder.Services.AddScoped<ApiExceptionFilterAttribute>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddTransient<IProductUnitofWork, ProductUnitOfWork>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
