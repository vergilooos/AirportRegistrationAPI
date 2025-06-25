using AirportRegistration.Application.DTOs;
using AirportRegistration.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AirportRegistration.Tests.Validators
{
    public class PersonCreateValidatorTests
    {
        private readonly PersonCreateValidator _validator = new();

        [Fact]
        public void Should_Fail_When_Required_Fields_Are_Missing()
        {
            // Arrange
            var dto = new PersonCreateDto(); // Empty DTO

            // Act
            var result = _validator.Validate(dto);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "FirstName");
            result.Errors.Should().Contain(e => e.PropertyName == "LastName");
            result.Errors.Should().Contain(e => e.PropertyName == "PassportNumber");
        }

        [Theory]
        [InlineData("X1234567")]  // starts with wrong letter
        [InlineData("P1234567")]  // missing second letter
        [InlineData("PL123456")]  // only 6 digits
        public void Should_Fail_When_PassportNumber_Is_Invalid(string invalidPassport)
        {
            var dto = new PersonCreateDto
            {
                FirstName = "Ali",
                LastName = "Test",
                PassportNumber = invalidPassport,
                AirportCode = "MAD"
            };

            var result = _validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "PassportNumber");
        }

        [Fact]
        public void Should_Pass_When_Data_Is_Valid()
        {
            var dto = new PersonCreateDto
            {
                FirstName = "Sara",
                LastName = "Ahmadi",
                PassportNumber = "PA1234567",
                Email = "sara@example.com",
                Phone = "+34912345678",
                AirportCode = "BCN"
            };

            var result = _validator.Validate(dto);

            result.IsValid.Should().BeTrue();
        }
    }
}
