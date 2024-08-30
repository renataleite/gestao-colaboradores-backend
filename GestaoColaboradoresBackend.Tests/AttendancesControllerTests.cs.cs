using GestaoColaboradoresBackend.Controllers;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using GestaoColaboradoresBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoColaboradoresBackend.Tests
{
    public class AttendancesControllerTests
    {
        private readonly Mock<IAttendanceService> _mockAttendanceService;
        private readonly AttendancesController _controller;

        public AttendancesControllerTests()
        {
            _mockAttendanceService = new Mock<IAttendanceService>();
            _controller = new AttendancesController(_mockAttendanceService.Object);
        }

        [Fact]
        public async Task GetAttendances_ReturnsOkResult_WithListOfAttendances()
        {
            var attendances = new List<Attendance>
            {
                new Attendance { Id = 1, CollaboratorId = 1, CheckInTime = DateTime.Parse("2024-08-01T08:00:00"), CheckOutTime = DateTime.Parse("2024-08-01T17:00:00") },
                new Attendance { Id = 2, CollaboratorId = 2, CheckInTime = DateTime.Parse("2024-08-02T08:00:00"), CheckOutTime = DateTime.Parse("2024-08-02T17:00:00") }
            };
            _mockAttendanceService.Setup(service => service.GetAllAttendancesAsync()).ReturnsAsync(attendances);

            var result = await _controller.GetAttendances();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Attendance>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAttendance_ReturnsNotFound_WhenAttendanceDoesNotExist()
        {
            int attendanceId = 1;
            _mockAttendanceService.Setup(service => service.GetAttendanceByIdAsync(attendanceId)).ReturnsAsync((Attendance)null);

            var result = await _controller.GetAttendance(attendanceId);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAttendance_ReturnsOkResult_WithAttendance()
        {
            int attendanceId = 1;
            var attendance = new Attendance { Id = attendanceId, CollaboratorId = 1, CheckInTime = DateTime.Parse("2024-08-01T08:00:00"), CheckOutTime = DateTime.Parse("2024-08-01T17:00:00") };
            _mockAttendanceService.Setup(service => service.GetAttendanceByIdAsync(attendanceId)).ReturnsAsync(attendance);

            var result = await _controller.GetAttendance(attendanceId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Attendance>(okResult.Value);
            Assert.Equal(attendanceId, returnValue.Id);
        }

        [Fact]
        public async Task CreateAttendance_ReturnsCreatedAtAction_WithAttendance()
        {
            var attendanceDto = new CreateAttendanceDto
            {
                CollaboratorId = 1,
                CheckInTime = "2024-08-01T08:00:00",
                CheckOutTime = "2024-08-01T17:00:00"
            };

            var createdAttendance = new Attendance { Id = 1, CollaboratorId = 1, CheckInTime = DateTime.Parse("2024-08-01T08:00:00"), CheckOutTime = DateTime.Parse("2024-08-01T17:00:00") };
            _mockAttendanceService.Setup(service => service.CreateAttendanceAsync(attendanceDto)).ReturnsAsync(createdAttendance);

            var result = await _controller.CreateAttendance(attendanceDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Attendance>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetAttendancesByMonth_ReturnsNotFound_WhenNoAttendancesExist()
        {
            int year = 2024;
            int month = 8;
            _mockAttendanceService.Setup(service => service.GetAttendancesByMonthAsync(year, month)).ReturnsAsync(new List<Attendance>());

            var result = await _controller.GetAttendancesByMonth(year, month);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public async Task ExportAttendancesToExcel_ReturnsFileResult_WhenDataExists()
        {
            var excelData = new byte[] { 1, 2, 3, 4, 5 };

            _mockAttendanceService
                .Setup(service => service.GenerateExcelReportAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(excelData);

            _mockAttendanceService
                .Setup(service => service.GenerateExcelReportAsync(It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync((int? year, int? month) => excelData);

            var result = await _controller.ExportAttendancesToExcel(null, null);

            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileResult.ContentType);
            Assert.Equal(excelData, fileResult.FileContents);
        }


    }
}
