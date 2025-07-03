using AutoMapper;
using ECommerce_Project.Repository;
using ECommerceApp.Middleware;
using ECommerceApp.Repository;
using ECommerceAPP.AutoMapper;
using ECommerceAPP.FluentValidation;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using ECommerceAPP.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. iakidkd
builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // This is fine as-is

builder.Services.AddDbContext<Zecommerce123Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMappingAll));

// Register the repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SuppliersRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();
builder.Services.AddScoped<ITerritoryRepository, TerritoryRepository>();

// Register the Validators
builder.Services.AddScoped<IValidator<Shipper>, ShipperValidator>();
builder.Services.AddScoped<IValidator<Supplier>, SupplierValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();

