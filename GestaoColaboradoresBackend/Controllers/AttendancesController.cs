using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using GestaoColaboradoresBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradoresBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendancesController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances()
        {
            try
            {
                var attendances = await _attendanceService.GetAllAttendancesAsync();
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                // Log de erro (pode ser feito usando uma ferramenta de logging)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
                if (attendance == null)
                {
                    return NotFound($"Attendance with ID {id} not found.");
                }

                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Attendance>> CreateAttendance(CreateAttendanceDto attendanceDto)
        {
            try
            {
                var attendance = await _attendanceService.CreateAttendanceAsync(attendanceDto);
                return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Id }, attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendancesByMonth(int year, int month)
        {
            try
            {
                var attendances = await _attendanceService.GetAttendancesByMonthAsync(year, month);

                if (attendances == null || !attendances.Any())
                {
                    return NotFound($"No attendances found for {month}/{year}.");
                }

                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportAttendancesToExcel(int? year, int? month)
        {
            try
            {
                byte[] excelData;

                if (year.HasValue && month.HasValue)
                {
                    excelData = await _attendanceService.GenerateExcelReportAsync(year.Value, month.Value);
                }
                else
                {
                    excelData = await _attendanceService.GenerateExcelReportAsync();
                }

                if (excelData == null || excelData.Length == 0)
                {
                    return NotFound("No attendances found.");
                }

                string excelName = year.HasValue && month.HasValue
                    ? $"AttendanceReport-{year}-{month}.xlsx"
                    : "AttendanceReport-All.xlsx";

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
