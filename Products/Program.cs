using Microsoft.EntityFrameworkCore;
using Products.Application.Middleware;
using Products.Application.Services.Implementation;
using Products.Application.Services.Interface;
using Products.Infrastruture.Data;
using Products.Infrastruture.Repository.implementation;
using Products.Infrastruture.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


// builder dbcontext
builder.Services.AddDbContext<AppDbContext>(op => op.UseInMemoryDatabase("Armazem"));


// add repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();


// Add cors especify
builder.Services.AddCors(op => {op.AddPolicy(
    "AllowSpecifyOrigin", builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Ensure CORS is enabled before routing
app.UseCors("AllowSpecifyOrigin");
app.UseHttpsRedirection();
// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();
// Map controllers and endpoints
app.MapControllers();

app.Run();