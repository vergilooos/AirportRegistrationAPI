using AirportRegistration.API.Middleware;
using AirportRegistration.Application.Mappings;
using AirportRegistration.Application.Services;
using AirportRegistration.Application.Validators;
using AirportRegistration.Infrastructure;
using AirportRegistration.Infrastructure.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructureServices();

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PersonCreateValidator>();

var app = builder.Build();

// Apply seed data at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Apply any pending migrations to the database //TODO
    await dbContext.Database.MigrateAsync();

    // Seed the airports
    await AirportSeeder.SeedAsync(dbContext);
}
// TEMP: Check if database contains seeded airports
//var airports = await dbContext.Airports.ToListAsync();
//Console.WriteLine($"Airport count in DB: {airports.Count}");
//foreach (var a in airports)
//{
//    Console.WriteLine($"- {a.Code}: {a.Name}");
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global error handling for unhandled exceptions
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
