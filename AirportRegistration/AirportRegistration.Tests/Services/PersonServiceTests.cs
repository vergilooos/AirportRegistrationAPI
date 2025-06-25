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

            // We'll capture the person that gets passed into AddAsync
            Person? createdPerson = null;

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Person>()))
                .Callback<Person>(p => createdPerson = p)
                .Returns(Task.CompletedTask);

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() =>
                {
                    // Simulate what would be retrieved after creation
                    return new Person
                    {
                        Id = createdPerson?.Id ?? Guid.NewGuid(),
                        FirstName = createdPerson?.FirstName ?? "",
                        LastName = createdPerson?.LastName ?? "",
                        PassportNumber = createdPerson?.PassportNumber ?? "",
                        AirportCode = createdPerson?.AirportCode ?? "",
                        Airport = new Airport { Code = "BCN", Name = "Barcelona Airport" }
                    };
                });

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.AirportCode.Should().Be("BCN");
            result.FirstName.Should().Be("Sara");

            // Optionally: verify AddAsync was actually called
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Person>()), Times.Once);
        }


        [Fact]
        public async Task GetByAirportAsync_Should_Return_People_In_Specified_Airport()
        {
            // Arrange
            var airportCode = "BCN";
            var people = new List<Person>
    {
        new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Mehran",
            LastName = "Bayat",
            PassportNumber = "PB1234567",
            AirportCode = airportCode,
            Airport = new Airport { Code = airportCode, Name = "Barcelona" }
        }
    };

            _repoMock.Setup(r => r.GetByAirportCodeAsync(airportCode)).ReturnsAsync(people);

            // Act
            var result = await _service.GetByAirportAsync(airportCode);

            // Assert
            result.Should().HaveCount(1);
            result[0].AirportCode.Should().Be(airportCode);
            result[0].AirportName.Should().Be("Barcelona");
        }

    }
}
