using AirportRegistration.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Application.Services
{
    // Interface that defines the available operations on Person data
    public interface IPersonService
    {
        Task<List<PersonDto>> GetAllAsync();
        Task<PersonDto?> GetByIdAsync(Guid id);
        Task<PersonDto> CreateAsync(PersonCreateDto dto);
        Task<PersonDto?> UpdateAsync(Guid id, PersonUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
