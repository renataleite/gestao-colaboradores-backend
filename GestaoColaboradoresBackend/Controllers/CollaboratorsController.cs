using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using GestaoColaboradoresBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradoresBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorsController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorsController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }

        // GET: api/Collaborators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collaborator>>> GetCollaborators()
        {
            try
            {
                var collaborators = await _collaboratorService.GetCollaboratorsAsync();
                return Ok(collaborators);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Collaborators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collaborator>> GetCollaborator(int id)
        {
            try
            {
                var collaborator = await _collaboratorService.GetCollaboratorByIdAsync(id);

                if (collaborator == null)
                {
                    return NotFound($"Collaborator with ID {id} not found.");
                }

                return Ok(collaborator);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Collaborators
        [HttpPost]
        public async Task<ActionResult<Collaborator>> PostCollaborator(CreateCollaboratorDto collaboratorDto)
        {
            try
            {
                var collaborator = await _collaboratorService.AddCollaboratorAsync(collaboratorDto);
                return CreatedAtAction(nameof(GetCollaborator), new { id = collaborator.Id }, collaborator);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Collaborators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollaborator(int id, UpdateCollaboratorDto collaboratorDto)
        {
            try
            {
                if (!await _collaboratorService.CollaboratorExistsAsync(id))
                {
                    return NotFound($"Collaborator with ID {id} not found.");
                }

                var result = await _collaboratorService.UpdateCollaboratorAsync(id, collaboratorDto);

                if (!result)
                {
                    return NotFound($"Collaborator with ID {id} could not be updated.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Collaborators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborator(int id)
        {
            try
            {
                var result = await _collaboratorService.DeleteCollaboratorAsync(id);

                if (!result)
                {
                    return NotFound($"Collaborator with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<CollaboratorReportDto>>> GetReport(int month, int year)
        {
            try
            {
                var report = await _collaboratorService.GetReportAsync(month, year);

                if (!report.Any())
                {
                    return NotFound("Nenhum registro encontrado para o mês e ano fornecidos.");
                }

                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
