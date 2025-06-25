using AirportRegistration.Application.DTOs;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace AirportRegistration.IntegrationTests
{
    public class PeopleApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PeopleApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_And_Then_Get_Person_Should_Work_Correctly()
        {
            // Arrange: create a new person
            var dto = new PersonCreateDto
            {
                FirstName = "Mona",
                LastName = "Ahmadi",
                PassportNumber = "PLA7654321",
                AirportCode = "MAD"
            };

            // Act 1: POST to /api/people
            var postResponse = await _client.PostAsJsonAsync("/api/people", dto);
            postResponse.EnsureSuccessStatusCode();

            var created = await postResponse.Content.ReadFromJsonAsync<PersonDto>();

            // Assert 1: POST response content
            created.Should().NotBeNull();
            created!.FirstName.Should().Be("Mona");

            // Act 2: GET that person by ID
            var getResponse = await _client.GetAsync($"/api/people/{created.Id}");
            getResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var fetched = await getResponse.Content.ReadFromJsonAsync<PersonDto>();

            // Assert 2: Fetched data matches what we posted
            fetched.Should().NotBeNull();
            fetched!.PassportNumber.Should().Be(dto.PassportNumber);
            fetched.AirportCode.Should().Be(dto.AirportCode);
        }
    }
}
