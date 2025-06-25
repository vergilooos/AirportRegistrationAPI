using AirportRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Domain.Repositories
{
    public interface IAirportRepository
    {
        Task<List<Airport>> GetAllAsync();
        Task<Airport?> GetByCodeAsync(string code);
    }
}
