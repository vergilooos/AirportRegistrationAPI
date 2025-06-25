using AirportRegistration.Domain.Entities;
using AirportRegistration.Domain.Repositories;
using AirportRegistration.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportRegistration.Infrastructure.Repositories
{
    // EF Core-based implementation of the person repository interface
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            // Includes Airport so we can access its name in the DTO
            return await _db.People.Include(p => p.Airport).ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            // Find person by ID and include related airport
            return await _db.People.Include(p => p.Airport)
                                   .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Person person)
        {
            _db.People.Add(person);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _db.People.Update(person);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var person = await _db.People.FindAsync(id);
            if (person != null)
            {
                _db.People.Remove(person);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Person>> GetByAirportCodeAsync(string code)
        {
            return await _db.People
                .Include(p => p.Airport)
                .Where(p => p.AirportCode == code)
                .ToListAsync();
        }
    }
}
