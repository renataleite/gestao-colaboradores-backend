using GestaoColaboradoresBackend.Data;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradoresBackend.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly CollaboratorManagementContext _context;

        public AttendanceService(CollaboratorManagementContext context)
        {
            _context = context;
        }

        // Obter todos os registros de ponto
        public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync()
        {
            return await _context.Attendances
                .Include(a => a.Collaborator)
                .ToListAsync();
        }

        // Obter um registro de ponto por ID
        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _context.Attendances
                .Include(a => a.Collaborator)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Criar um novo registro de ponto
        public async Task<Attendance> CreateAttendanceAsync(CreateAttendanceDto attendanceDto)
        {
            var attendance = new Attendance
            {
                CollaboratorId = attendanceDto.CollaboratorId,
                CheckInTime = attendanceDto.CheckInTime,
                CheckOutTime = attendanceDto.CheckOutTime
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return attendance;
        }
    }
}