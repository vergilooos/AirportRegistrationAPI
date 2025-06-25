using AirportRegistration.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirportRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportRepository _airportRepo;
        private readonly ILogger<AirportsController> _logger;

        public AirportsController(IAirportRepository airportRepo, ILogger<AirportsController> logger)
        {
            _airportRepo = airportRepo;
            _logger = logger;
        }

        /// <summary>
        /// Returns the list of all available airports
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all airports...");
            var airports = await _airportRepo.GetAllAsync();
            return Ok(airports);
        }
    }
}