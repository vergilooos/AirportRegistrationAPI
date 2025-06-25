using AirportRegistration.API.Controllers;
using AirportRegistration.Application.DTOs;
using AirportRegistration.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AirportRegistration.Tests.Controllers
{
    public class PeopleControllerTests
    {
        private readonly Mock<IPersonService> _serviceMock = new();
        private readonly Mock<ILogger<PeopleController>> _loggerMock = new();
        private readonly PeopleController _controller;

        public PeopleControllerTests()
        {
            _controller = new PeopleController(_serviceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithListOfPeople()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<PersonDto>
                {
                new() { Id = Guid.NewGuid(), FirstName = "Ali", LastName = "Rezaei", AirportCode = "MAD", AirportName = "Madrid" }
                });

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var people = okResult.Value.Should().BeAssignableTo<List<PersonDto>>().Subject;
            people.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_IfPersonDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((PersonDto?)null);

            var result = await _controller.GetById(Guid.NewGuid());

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResult()
        {
            var dto = new PersonCreateDto
            {
                FirstName = "Sara",
                LastName = "Ahmadi",
                PassportNumber = "PA1234567",
                AirportCode = "BCN"
            };

            _serviceMock.Setup(s => s.CreateAsync(dto))
                .ReturnsAsync(new PersonDto
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sara",
                    LastName = "Ahmadi",
                    AirportCode = "BCN",
                    AirportName = "Barcelona"
                });

            var result = await _controller.Create(dto);

            result.Should().BeOfType<CreatedAtActionResult>();
        }
        [Fact]
        public async Task GetByAirport_ShouldReturnOk_WithFilteredPeople()
        {
            // Arrange
            var airportCode = "MAD";
            _serviceMock.Setup(s => s.GetByAirportAsync(airportCode)).ReturnsAsync(new List<PersonDto>
    {
        new PersonDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Ali",
            LastName = "Test",
            AirportCode = airportCode,
            AirportName = "Madrid"
        }
    });

            // Act
            var result = await _controller.GetByAirport(airportCode);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var people = okResult.Value.Should().BeAssignableTo<List<PersonDto>>().Subject;
            people.Should().OnlyContain(p => p.AirportCode == airportCode);
        }

    }
}
