using Microsoft.OpenApi.Models;
using WebApplication1.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<WeatherRepository>();
// Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API without EF",
        Version = "v1",
        Description = "A simple Web API without Entity Framework and using Swagger",
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
 builder => builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  // This makes Swagger UI available at the root URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Apply CORS policy
app.UseCors("AllowAll");
app.UseCors("AllowLocalhost");// Ensure CORS is used with the specified policy


app.MapControllers();

app.Run();