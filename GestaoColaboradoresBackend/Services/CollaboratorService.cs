﻿using GestaoColaboradoresBackend.Data;
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

        public async Task<bool> UpdateCollaboratorAsync(int id, UpdateCollaboratorDto collaboratorDto)
        {
            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
                return false;

            collaborator.Name = collaboratorDto.Name;
            collaborator.RegistrationNumber = collaboratorDto.RegistrationNumber;
            collaborator.Position = collaboratorDto.Position;
            collaborator.Salary = collaboratorDto.Salary;

            _context.Entry(collaborator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CollaboratorExistsAsync(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteCollaboratorAsync(int id)
        {
            var collaborator = await _context.Collaborators.FindAsync(id);
            if (collaborator == null)
            {
                return false;
            }

            _context.Collaborators.Remove(collaborator);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CollaboratorExistsAsync(int id)
        {
            return await _context.Collaborators.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<CollaboratorReportDto>> GetReportAsync(int month, int year)
        {
            var report = await _context.Collaborators
                .Include(c => c.Attendances)
                .Where(c => c.Attendances.Any(a => a.CheckInTime.Month == month && a.CheckInTime.Year == year))
                .ToListAsync();

            var reportDto = report.Select(c => new CollaboratorReportDto
            {
                CollaboratorId = c.Id,
                Name = c.Name,
                RegistrationNumber = c.RegistrationNumber,
                Position = c.Position,
                Salary = c.Salary,
                Attendances = c.Attendances
                    .Where(a => a.CheckInTime.Month == month && a.CheckInTime.Year == year)
                    .Select(a => new AttendanceDto
                    {
                        CheckInTime = a.CheckInTime,
                        CheckOutTime = a.CheckOutTime
                    }).ToList()
            }).ToList();

            return reportDto;
        }


    }
}
