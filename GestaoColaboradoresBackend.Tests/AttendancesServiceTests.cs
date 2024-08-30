using GestaoColaboradoresBackend.Data;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using GestaoColaboradoresBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradoresBackend.Tests
{
    public class AttendanceServiceTests : IDisposable
    {
        private readonly CollaboratorManagementContext _context;
        private readonly AttendanceService _service;

        public AttendanceServiceTests()
        {
            var options = new DbContextOptionsBuilder<CollaboratorManagementContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Gera um novo banco de dados para cada teste
                .Options;

            _context = new CollaboratorManagementContext(options);
            _service = new AttendanceService(_context);
        }

        [Fact]
        public async Task CreateAttendanceAsync_CreatesNewAttendance()
        {
            // Arrange
            var collaborator = new Collaborator { Name = "Test Collaborator", RegistrationNumber = "12345", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            var createDto = new CreateAttendanceDto
            {
                CollaboratorId = collaborator.Id,
                CheckInTime = DateTime.Now.AddHours(-8).ToString(),
                CheckOutTime = DateTime.Now.ToString()
            };

            // Act
            var result = await _service.CreateAttendanceAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(collaborator.Id, result.CollaboratorId);
        }

        [Fact]
        public async Task GetAllAttendancesAsync_ReturnsAllAttendances()
        {
            // Arrange
            var collaborator = new Collaborator { Name = "Test Collaborator", RegistrationNumber = "12345", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            var attendance1 = new Attendance { CollaboratorId = collaborator.Id, CheckInTime = DateTime.Now.AddHours(-8), CheckOutTime = DateTime.Now };
            var attendance2 = new Attendance { CollaboratorId = collaborator.Id, CheckInTime = DateTime.Now.AddDays(-1).AddHours(-8), CheckOutTime = DateTime.Now.AddDays(-1) };

            _context.Attendances.AddRange(attendance1, attendance2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllAttendancesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAttendanceByIdAsync_ReturnsCorrectAttendance()
        {
            // Arrange
            var collaborator = new Collaborator { Name = "Test Collaborator", RegistrationNumber = "12345", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            var attendance = new Attendance { CollaboratorId = collaborator.Id, CheckInTime = DateTime.Now.AddHours(-8), CheckOutTime = DateTime.Now };
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAttendanceByIdAsync(attendance.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(attendance.Id, result.Id);
        }

        [Fact]
        public async Task GetAttendancesByMonthAsync_ReturnsAttendancesForGivenMonth()
        {
            // Arrange
            var collaborator = new Collaborator { Name = "Test Collaborator", RegistrationNumber = "12345", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            var attendance1 = new Attendance { CollaboratorId = collaborator.Id, CheckInTime = new DateTime(2024, 5, 15, 9, 0, 0), CheckOutTime = new DateTime(2024, 5, 15, 17, 0, 0) };
            var attendance2 = new Attendance { CollaboratorId = collaborator.Id, CheckInTime = new DateTime(2024, 4, 15, 9, 0, 0), CheckOutTime = new DateTime(2024, 4, 15, 17, 0, 0) };

            _context.Attendances.AddRange(attendance1, attendance2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAttendancesByMonthAsync(2024, 5);

            // Assert
            Assert.Single(result);
            Assert.Equal(attendance1.Id, result.First().Id);
        }

        [Fact]
        public async Task GenerateExcelReportAsync_ReturnsExcelFileData()
        {
            // Arrange
            var collaborator = new Collaborator { Name = "Test Collaborator", RegistrationNumber = "12345", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            var attendance = new Attendance { CollaboratorId = collaborator.Id, CheckInTime = DateTime.Now.AddHours(-8), CheckOutTime = DateTime.Now };
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GenerateExcelReportAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
