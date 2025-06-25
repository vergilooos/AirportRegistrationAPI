using AirportRegistration.Application.DTOs;
using AirportRegistration.Domain.Entities;
using AirportRegistration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportRegistration.Application.Services
{
    // person related business logic
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<PersonDto>> GetAllAsync()
        {
            var people = await _repository.GetAllAsync();

            // Manually map entities to DTOs
            return people.Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Phone = p.Phone,
                PassportNumber = p.PassportNumber,
                AirportCode = p.AirportCode,
                AirportName = p.Airport?.Name ?? ""
            }).ToList();
        }
        public async Task<PersonDto?> GetByIdAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null)
                return null;

            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Phone = person.Phone,
                PassportNumber = person.PassportNumber,
                AirportCode = person.AirportCode,
                AirportName = person.Airport?.Name ?? ""
            };
        }

        public async Task<PersonDto> CreateAsync(PersonCreateDto dto)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                PassportNumber = dto.PassportNumber,
                AirportCode = dto.AirportCode
            };

            await _repository.AddAsync(person);

            return await GetByIdAsync(person.Id) ?? throw new Exception("Person creation failed.");
        }

        public async Task<PersonDto?> UpdateAsync(Guid id, PersonUpdateDto dto)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null)
                return null;

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.Email = dto.Email;
            person.Phone = dto.Phone;
            person.PassportNumber = dto.PassportNumber;
            person.AirportCode = dto.AirportCode;

            await _repository.UpdateAsync(person);

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
