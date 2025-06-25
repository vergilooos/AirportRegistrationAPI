using AirportRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportRegistration.Infrastructure.Persistence;

public static class AirportSeeder
{
    // List of airports for seed into the database
    private static readonly List<Airport> DefaultAirports = new()
    {
        new Airport { Code = "MAD", Name = "Adolfo Suárez Madrid–Barajas Airport" },
        new Airport { Code = "BCN", Name = "Barcelona–El Prat Airport" },
        new Airport { Code = "AGP", Name = "Málaga Airport" },
        new Airport { Code = "PMI", Name = "Palma de Mallorca Airport" },
        new Airport { Code = "ALC", Name = "Alicante–Elche Airport" },
        new Airport { Code = "LPA", Name = "Gran Canaria Airport" },
        new Airport { Code = "TFS", Name = "Tenerife South Airport" },
        new Airport { Code = "VLC", Name = "Valencia Airport" },
        new Airport { Code = "SVQ", Name = "Seville Airport" },
        new Airport { Code = "BIO", Name = "Bilbao Airport" },
        new Airport { Code = "IBZ", Name = "Ibiza Airport" },
        new Airport { Code = "MAH", Name = "Menorca Airport" },
        new Airport { Code = "SCQ", Name = "Santiago de Compostela Airport" },
        new Airport { Code = "OVD", Name = "Asturias Airport" },
        new Airport { Code = "GRO", Name = "Girona–Costa Brava Airport" }
    };

    // seed into the database if none exist
    public static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        if (!await dbContext.Airports.AnyAsync())
        {
            dbContext.Airports.AddRange(DefaultAirports);
            await dbContext.SaveChangesAsync();
        }
    }
}
