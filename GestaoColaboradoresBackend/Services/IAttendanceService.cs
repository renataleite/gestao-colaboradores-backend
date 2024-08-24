using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;

namespace GestaoColaboradoresBackend.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync();
        Task<Attendance?> GetAttendanceByIdAsync(int id);
        Task<Attendance> CreateAttendanceAsync(CreateAttendanceDto attendanceDto);
    }
}
