using GestaoColaboradoresBackend.Data;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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
            DateTime checkInTime = DateTime.Parse(attendanceDto.CheckInTime);
            DateTime checkOutTime = DateTime.Parse(attendanceDto.CheckOutTime);
            
            var attendance = new Attendance
            {
                CollaboratorId = attendanceDto.CollaboratorId,
                CheckInTime = checkInTime,
                CheckOutTime = checkOutTime
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return attendance;
        }

        public async Task<IEnumerable<Attendance>> GetAttendancesByMonthAsync(int year, int month)
        {
            return await _context.Attendances
                .Include(a => a.Collaborator)
                .Where(a => a.CheckInTime.Year == year && a.CheckInTime.Month == month)
                .ToListAsync();
        }

        public async Task<byte[]> GenerateExcelReportAsync(int? year = null, int? month = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<Attendance> attendances;

            if (year.HasValue && month.HasValue)
            {
                attendances = (await GetAttendancesByMonthAsync(year.Value, month.Value)).ToList();
            }
            else
            {
                attendances = (await GetAllAttendancesAsync()).ToList();
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Attendances");

                worksheet.Cells["A1"].Value = "Nome do Colaborador";
                worksheet.Cells["B1"].Value = "Data";
                worksheet.Cells["C1"].Value = "Entrada";
                worksheet.Cells["D1"].Value = "Saída";

                int row = 2;
                foreach (var attendance in attendances)
                {
                    worksheet.Cells[row, 1].Value = attendance.Collaborator?.Name;
                    worksheet.Cells[row, 2].Value = attendance.CheckInTime.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 3].Value = attendance.CheckInTime.ToString("HH:mm:ss");
                    worksheet.Cells[row, 4].Value = attendance.CheckOutTime.ToString("HH:mm:ss");
                    row++;
                }

                return package.GetAsByteArray();
            }
        }



    }
}