using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AirportRegistration.Application.DTOs;
using AirportRegistration.Application.Services;

namespace AirportRegistration.API.Controllers
{
    /// <summary>
    /// API for managing people registrations per airport
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(IPersonService personService, ILogger<PeopleController> logger)
        {
            _personService = personService;
            _logger = logger;
        }


        /// <summary>
        /// Get all registered people
        /// </summary>
        // GET: /api/people
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all registered people...");
            var result = await _personService.GetAllAsync();
            return Ok(result);
        }

        // GET: /api/people/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Fetching person with ID {Id}", id);

            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                _logger.LogWarning("Person with ID {Id} not found", id);
                return NotFound();
            }

            return Ok(person);
        }

        // POST: /api/people
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonCreateDto dto)
        {
            _logger.LogInformation("Creating new person with passport {Passport}", dto.PassportNumber);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Create request: {@Dto}", dto);
                return BadRequest(ModelState);
            }

            var created = await _personService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: /api/people/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonUpdateDto dto)
        {
            _logger.LogInformation("Updating person with ID {Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Update request: {@Dto}", dto);
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                _logger.LogWarning("Mismatched IDs in URL ({UrlId}) and body ({BodyId})", id, dto.Id);
                return BadRequest("ID in URL and payload must match.");
            }

            var updated = await _personService.UpdateAsync(id, dto);
            if (updated == null)
            {
                _logger.LogWarning("Person with ID {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }

        // DELETE: /api/people/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting person with ID {Id}", id);

            var deleted = await _personService.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Attempted to delete non-existing person with ID {Id}", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
