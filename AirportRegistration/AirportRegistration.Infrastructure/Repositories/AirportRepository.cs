using AirportRegistration.Domain.Entities;
using AirportRegistration.Domain.Repositories;
using AirportRegistration.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AirportRegistration.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly ApplicationDbContext _db;

        public AirportRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Airport>> GetAllAsync()
        {
            return await _db.Airports.ToListAsync();
        }

        public async Task<Airport?> GetByCodeAsync(string code)
        {
            return await _db.Airports.FindAsync(code);
        }
    }
}
