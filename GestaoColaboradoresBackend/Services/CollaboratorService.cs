using GestaoColaboradoresBackend.Data;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradoresBackend.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly CollaboratorManagementContext _context;

        public CollaboratorService(CollaboratorManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Collaborator>> GetCollaboratorsAsync()
        {
            return await _context.Collaborators.Include(c => c.Attendances).ToListAsync();
        }

        public async Task<Collaborator> GetCollaboratorByIdAsync(int id)
        {
            return await _context.Collaborators.Include(c => c.Attendances).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Collaborator> AddCollaboratorAsync(CreateCollaboratorDto collaboratorDto)
        {
            var collaborator = new Collaborator
            {
                Name = collaboratorDto.Name,
                RegistrationNumber = collaboratorDto.RegistrationNumber,
                Position = collaboratorDto.Position,
                Salary = collaboratorDto.Salary
            };

            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            return collaborator;
        }
    }
}
