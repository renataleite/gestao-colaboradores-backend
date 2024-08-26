using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;

namespace GestaoColaboradoresBackend.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync();
        Task<Attendance?> GetAttendanceByIdAsync(int id);
        Task<Attendance> CreateAttendanceAsync(CreateAttendanceDto attendanceDto);
        Task<IEnumerable<Attendance>> GetAttendancesByMonthAsync(int year, int month);
        Task<byte[]> GenerateExcelReportAsync(int? year = null, int? month = null);
    }
}
