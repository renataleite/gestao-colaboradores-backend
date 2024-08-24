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
            return Ok(await _collaboratorService.GetCollaboratorsAsync());
        }

        // GET: api/Collaborators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collaborator>> GetCollaborator(int id)
        {
            var collaborator = await _collaboratorService.GetCollaboratorByIdAsync(id);

            if (collaborator == null)
            {
                return NotFound();
            }

            return Ok(collaborator);
        }

        // POST: api/Collaborators
        [HttpPost]
        public async Task<ActionResult<Collaborator>> PostCollaborator(CreateCollaboratorDto collaboratorDto)
        {
            var collaborator = await _collaboratorService.AddCollaboratorAsync(collaboratorDto);

            return CreatedAtAction(nameof(GetCollaborator), new { id = collaborator.Id }, collaborator);
        }
    }
}
