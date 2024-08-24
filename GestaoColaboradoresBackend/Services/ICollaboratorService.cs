using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;

namespace GestaoColaboradoresBackend.Services
{
    public interface ICollaboratorService
    {
        Task<IEnumerable<Collaborator>> GetCollaboratorsAsync();
        Task<Collaborator> GetCollaboratorByIdAsync(int id);
        Task<Collaborator> AddCollaboratorAsync(CreateCollaboratorDto collaboratorDto);
        Task<bool> UpdateCollaboratorAsync(int id, UpdateCollaboratorDto collaboratorDto);
        Task<bool> CollaboratorExistsAsync(int id);
        Task<bool> DeleteCollaboratorAsync(int id);
        Task<IEnumerable<CollaboratorReportDto>> GetReportAsync(int month, int year);
    }
}
