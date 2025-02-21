using LearnDapper.DapperContext;
using LearnDapper.Interface;
using LearnDapper.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register the DapperDbContext and DapperRepo services
builder.Services.AddTransient<DapperDbContext>();
builder.Services.AddTransient<IDapperService, DapperRepo>();

// Add CORS policy to allow requests from specific origins (or all origins for testing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Allows requests from any origin (for testing purposes)
              .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader(); // Allows all headers
    });
});

// Add Swagger/OpenAPI for API documentation (for testing and development)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure CORS
app.UseCors("AllowAllOrigins"); // Enable the CORS policy globally

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
