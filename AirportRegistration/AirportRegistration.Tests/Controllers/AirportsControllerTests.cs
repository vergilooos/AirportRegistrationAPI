using AirportRegistration.API.Controllers;
using AirportRegistration.Domain.Entities;
using AirportRegistration.Domain.Repositories;
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
    public class AirportsControllerTests
    {
        private readonly Mock<IAirportRepository> _airportRepoMock = new();
        private readonly Mock<ILogger<AirportsController>> _loggerMock = new();
        private readonly AirportsController _controller;

        public AirportsControllerTests()
        {
            _controller = new AirportsController(_airportRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithAirportList()
        {
            // Arrange
            var airports = new List<Airport>
        {
            new Airport { Code = "MAD", Name = "Madrid Airport" },
            new Airport { Code = "BCN", Name = "Barcelona Airport" }
        };

            _airportRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(airports);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedAirports = okResult.Value.Should().BeAssignableTo<List<Airport>>().Subject;
            returnedAirports.Should().HaveCount(2);
            returnedAirports.Select(a => a.Code).Should().Contain("MAD");
        }
    }
}
