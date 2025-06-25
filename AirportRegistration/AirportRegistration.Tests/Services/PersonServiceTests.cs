using AirportRegistration.Application.DTOs;
using AirportRegistration.Application.Services;
using AirportRegistration.Domain.Entities;
using AirportRegistration.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AirportRegistration.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _repoMock = new();
        private readonly Mock<ILogger<PersonService>> _loggerMock = new();
        private readonly IMapper _mapper;
        private readonly PersonService _service;

        public PersonServiceTests()
        {
            // Configure AutoMapper manually for test
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, PersonDto>()
                    .ForMember(dest => dest.AirportName, opt => opt.MapFrom(src => src.Airport.Name));
                cfg.CreateMap<PersonCreateDto, Person>();
                cfg.CreateMap<PersonUpdateDto, Person>();
            });

            _mapper = config.CreateMapper();
            _service = new PersonService(_repoMock.Object, _loggerMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_List_Of_DTOs()
        {
            var people = new List<Person>
        {
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Ali",
                LastName = "Zarei",
                PassportNumber = "PA1234567",
                AirportCode = "MAD",
                Airport = new Airport { Code = "MAD", Name = "Madrid Airport" }
            }
        };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(people);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].FirstName.Should().Be("Ali");
            result[0].AirportName.Should().Be("Madrid Airport");
        }

        [Fact]
        public async Task CreateAsync_Should_Call_Repository_And_Return_DTO()
        {
            var dto = new PersonCreateDto
            {
                FirstName = "Sara",
                LastName = "Amini",
                PassportNumber = "PLB7654321",
                AirportCode = "BCN"
            };

            // Mimic "GetById" after create
            var id = Guid.NewGuid();
            var created = new Person
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PassportNumber = dto.PassportNumber,
                AirportCode = dto.AirportCode,
                Airport = new Airport { Code = "BCN", Name = "Barcelona Airport" }
            };

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Person>())).Returns(Task.CompletedTask);
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(created);

            // Simulate service
            var result = await _service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.AirportCode.Should().Be("BCN");
            result.FirstName.Should().Be("Sara");
        }
    }
}
