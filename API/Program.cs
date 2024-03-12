using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API; // Assuming API is the namespace for your application
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt =>
{
  opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowSpecificOrigin",
      builder =>
      {
        builder.WithOrigins("https://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
      });
});

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
  });
}

app.UseHttpsRedirection();
app.UseRouting();

// Use CORS middleware
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

// Add endpoint routing
app.MapControllers();

// Add console log to indicate that the application has started
Console.WriteLine("Application started.");

app.Run();
