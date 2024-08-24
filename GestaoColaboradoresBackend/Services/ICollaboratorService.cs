﻿using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;

namespace GestaoColaboradoresBackend.Services
{
    public interface ICollaboratorService
    {
        Task<IEnumerable<Collaborator>> GetCollaboratorsAsync();
        Task<Collaborator> GetCollaboratorByIdAsync(int id);
        Task<Collaborator> AddCollaboratorAsync(CreateCollaboratorDto collaboratorDto);
    }
}
