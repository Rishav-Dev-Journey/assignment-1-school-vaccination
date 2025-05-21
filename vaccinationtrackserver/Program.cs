// Using statements: Import namespaces for ASP.NET Core and EF Core
using Microsoft.EntityFrameworkCore;
using vaccinationtrackserver.Data;

// CreateBuilder: Initializes the web application with default services
var builder = WebApplication.CreateBuilder(args);

// AddControllers: Enables MVC controllers for handling HTTP requests
builder.Services.AddControllers();

// AddDbContext: Registers ApplicationDbContext with dependency injection
// UseNpgsql: Configures EF Core to use PostgreSQL with the connection string from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// AddEndpointsApiExplorer: Enables API metadata for Swagger
builder.Services.AddEndpointsApiExplorer();

// AddSwaggerGen: Configures Swagger for API documentation
builder.Services.AddSwaggerGen();

// Build: Creates the web application instance
var app = builder.Build();

// Configure the HTTP request pipeline
// UseSwagger: Enables Swagger middleware in development
// UseSwaggerUI: Provides the Swagger UI at /swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// UseHttpsRedirection: Redirects HTTP requests to HTTPS
app.UseHttpsRedirection();

// UseAuthorization: Enables authorization middleware (not used yet, but included for future)
app.UseAuthorization();

// MapControllers: Maps controller routes to handle API requests
app.MapControllers();

// Run: Starts the web application
app.Run();