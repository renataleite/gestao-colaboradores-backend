using GestaoColaboradoresBackend.Data;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using GestaoColaboradoresBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradoresBackend.Tests
{
    public class CollaboratorServiceTests : IDisposable
    {
        private readonly CollaboratorService _service;
        private readonly CollaboratorManagementContext _context;

        public CollaboratorServiceTests()
        {
            var options = new DbContextOptionsBuilder<CollaboratorManagementContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Gera um novo banco de dados para cada teste
                .Options;

            _context = new CollaboratorManagementContext(options);
            _service = new CollaboratorService(_context);
        }

        [Fact]
        public async Task GetCollaboratorsAsync_ReturnsAllCollaborators()
        {
            // Arrange
            _context.Collaborators.Add(new Collaborator { Id = 1, Name = "John Doe", RegistrationNumber = "001", Position = "Developer", Salary = 5000 });
            _context.Collaborators.Add(new Collaborator { Id = 2, Name = "Jane Doe", RegistrationNumber = "002", Position = "Manager", Salary = 6000 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetCollaboratorsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCollaboratorByIdAsync_ReturnsCollaborator_WhenFound()
        {
            // Arrange
            var collaborator = new Collaborator { Id = 1, Name = "John Doe", RegistrationNumber = "001", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetCollaboratorByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetCollaboratorByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Act
            var result = await _service.GetCollaboratorByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddCollaboratorAsync_CreatesNewCollaborator()
        {
            // Arrange
            var collaboratorDto = new CreateCollaboratorDto
            {
                Name = "John Doe",
                RegistrationNumber = "001",
                Position = "Developer",
                Salary = 5000
            };

            // Act
            var result = await _service.AddCollaboratorAsync(collaboratorDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("001", result.RegistrationNumber);
        }

        [Fact]
        public async Task UpdateCollaboratorAsync_UpdatesExistingCollaborator()
        {
            // Arrange
            var collaborator = new Collaborator { Id = 1, Name = "John Doe", RegistrationNumber = "001", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            var collaboratorDto = new UpdateCollaboratorDto
            {
                Name = "John Smith",
                RegistrationNumber = "001",
                Position = "Senior Developer",
                Salary = 7000
            };

            // Act
            var result = await _service.UpdateCollaboratorAsync(1, collaboratorDto);

            // Assert
            Assert.True(result);
            var updatedCollaborator = await _context.Collaborators.FindAsync(1);
            Assert.Equal("John Smith", updatedCollaborator.Name);
            Assert.Equal("Senior Developer", updatedCollaborator.Position);
        }

        [Fact]
        public async Task UpdateCollaboratorAsync_ReturnsFalse_WhenCollaboratorNotFound()
        {
            // Arrange
            var collaboratorDto = new UpdateCollaboratorDto
            {
                Name = "John Smith",
                RegistrationNumber = "001",
                Position = "Senior Developer",
                Salary = 7000
            };

            // Act
            var result = await _service.UpdateCollaboratorAsync(999, collaboratorDto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteCollaboratorAsync_DeletesCollaborator_WhenFound()
        {
            // Arrange
            var collaborator = new Collaborator { Id = 1, Name = "John Doe", RegistrationNumber = "001", Position = "Developer", Salary = 5000 };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DeleteCollaboratorAsync(1);

            // Assert
            Assert.True(result);
            Assert.Null(await _context.Collaborators.FindAsync(1));
        }

        [Fact]
        public async Task DeleteCollaboratorAsync_ReturnsFalse_WhenCollaboratorNotFound()
        {
            // Act
            var result = await _service.DeleteCollaboratorAsync(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetReportAsync_ReturnsReport_ForSpecificMonthAndYear()
        {
            // Arrange
            var collaborator = new Collaborator
            {
                Id = 1,
                Name = "John Doe",
                RegistrationNumber = "001",
                Position = "Developer",
                Salary = 5000,
                Attendances = new List<Attendance>
                {
                    new Attendance { Id = 1, CheckInTime = new DateTime(2024, 5, 1, 9, 0, 0), CheckOutTime = new DateTime(2024, 5, 1, 17, 0, 0) }
                }
            };
            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetReportAsync(5, 2024);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result.First().CollaboratorId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
