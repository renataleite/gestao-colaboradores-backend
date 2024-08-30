using GestaoColaboradoresBackend.Controllers;
using GestaoColaboradoresBackend.Dtos;
using GestaoColaboradoresBackend.Models;
using GestaoColaboradoresBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestaoColaboradoresBackend.Tests
{
    public class CollaboratorsControllerTests
    {
        private readonly Mock<ICollaboratorService> _mockCollaboratorService;
        private readonly CollaboratorsController _controller;

        public CollaboratorsControllerTests()
        {
            _mockCollaboratorService = new Mock<ICollaboratorService>();
            _controller = new CollaboratorsController(_mockCollaboratorService.Object);
        }

        [Fact]
        public async Task GetCollaborators_ReturnsOkResult_WithListOfCollaborators()
        {
            // Arrange
            var collaborators = new List<Collaborator>
            {
                new Collaborator { Id = 1, Name = "John Doe", RegistrationNumber = "123", Position = "Developer", Salary = 1000 },
                new Collaborator { Id = 2, Name = "Jane Doe", RegistrationNumber = "124", Position = "Designer", Salary = 1200 }
            };
            _mockCollaboratorService.Setup(service => service.GetCollaboratorsAsync()).ReturnsAsync(collaborators);

            // Act
            var result = await _controller.GetCollaborators();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Collaborator>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCollaborator_ReturnsNotFound_WhenCollaboratorDoesNotExist()
        {
            // Arrange
            int collaboratorId = 1;
            _mockCollaboratorService.Setup(service => service.GetCollaboratorByIdAsync(collaboratorId)).ReturnsAsync((Collaborator)null);

            // Act
            var result = await _controller.GetCollaborator(collaboratorId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetCollaborator_ReturnsOkResult_WithCollaborator()
        {
            // Arrange
            int collaboratorId = 1;
            var collaborator = new Collaborator { Id = collaboratorId, Name = "John Doe", RegistrationNumber = "123", Position = "Developer", Salary = 1000 };
            _mockCollaboratorService.Setup(service => service.GetCollaboratorByIdAsync(collaboratorId)).ReturnsAsync(collaborator);

            // Act
            var result = await _controller.GetCollaborator(collaboratorId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Collaborator>(okResult.Value);
            Assert.Equal(collaboratorId, returnValue.Id);
        }

        [Fact]
        public async Task PostCollaborator_ReturnsCreatedAtActionResult_WithNewCollaborator()
        {
            // Arrange
            var collaboratorDto = new CreateCollaboratorDto { Name = "John Doe", RegistrationNumber = "123", Position = "Developer", Salary = 1000 };
            var newCollaborator = new Collaborator { Id = 1, Name = "John Doe", RegistrationNumber = "123", Position = "Developer", Salary = 1000 };

            _mockCollaboratorService.Setup(service => service.AddCollaboratorAsync(collaboratorDto)).ReturnsAsync(newCollaborator);

            // Act
            var result = await _controller.PostCollaborator(collaboratorDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Collaborator>(createdAtActionResult.Value);
            Assert.Equal(newCollaborator.Id, returnValue.Id);
        }

        [Fact]
        public async Task PutCollaborator_ReturnsNotFound_WhenCollaboratorDoesNotExist()
        {
            // Arrange
            int collaboratorId = 1;
            var collaboratorDto = new UpdateCollaboratorDto { Name = "Updated Name", Position = "Updated Position", Salary = 1500 };

            _mockCollaboratorService.Setup(service => service.CollaboratorExistsAsync(collaboratorId)).ReturnsAsync(false);

            // Act
            var result = await _controller.PutCollaborator(collaboratorId, collaboratorDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task PutCollaborator_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            int collaboratorId = 1;
            var collaboratorDto = new UpdateCollaboratorDto { Name = "Updated Name", Position = "Updated Position", Salary = 1500 };

            _mockCollaboratorService.Setup(service => service.CollaboratorExistsAsync(collaboratorId)).ReturnsAsync(true);
            _mockCollaboratorService.Setup(service => service.UpdateCollaboratorAsync(collaboratorId, collaboratorDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutCollaborator(collaboratorId, collaboratorDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCollaborator_ReturnsNotFound_WhenCollaboratorDoesNotExist()
        {
            // Arrange
            int collaboratorId = 1;
            _mockCollaboratorService.Setup(service => service.DeleteCollaboratorAsync(collaboratorId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCollaborator(collaboratorId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCollaborator_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            int collaboratorId = 1;
            _mockCollaboratorService.Setup(service => service.DeleteCollaboratorAsync(collaboratorId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCollaborator(collaboratorId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetReport_ReturnsOkResult_WithReport()
        {
            // Arrange
            int month = 5;
            int year = 2024;
            var report = new List<CollaboratorReportDto>
        {
            new CollaboratorReportDto
            {
                CollaboratorId = 1,
                Name = "John Doe",
                RegistrationNumber = "123",
                Position = "Developer",
                Salary = 1000,
                Attendances = new List<AttendanceDto>
                {
                    new AttendanceDto { CheckInTime = new DateTime(2024, 5, 1, 9, 0, 0), CheckOutTime = new DateTime(2024, 5, 1, 17, 0, 0) },
                    new AttendanceDto { CheckInTime = new DateTime(2024, 5, 2, 9, 0, 0), CheckOutTime = new DateTime(2024, 5, 2, 17, 0, 0) }
                }
            },
            new CollaboratorReportDto
            {
                CollaboratorId = 2,
                Name = "Jane Doe",
                RegistrationNumber = "124",
                Position = "Designer",
                Salary = 1200,
                Attendances = new List<AttendanceDto>
                {
                    new AttendanceDto { CheckInTime = new DateTime(2024, 5, 1, 8, 0, 0), CheckOutTime = new DateTime(2024, 5, 1, 16, 0, 0) },
                    new AttendanceDto { CheckInTime = new DateTime(2024, 5, 2, 8, 0, 0), CheckOutTime = new DateTime(2024, 5, 2, 16, 0, 0) }
                }
            }
            };

            _mockCollaboratorService.Setup(service => service.GetReportAsync(month, year)).ReturnsAsync(report);

            // Act
            var result = await _controller.GetReport(month, year);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CollaboratorReportDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

            // Adicionando validações adicionais para garantir que os dados retornados estejam corretos
            Assert.Equal("John Doe", returnValue[0].Name);
            Assert.Equal(2, returnValue[0].Attendances.Count);
            Assert.Equal("Jane Doe", returnValue[1].Name);
            Assert.Equal(2, returnValue[1].Attendances.Count);
        }

        [Fact]
        public async Task GetReport_ReturnsNotFound_WhenNoRecordsFound()
        {
            // Arrange
            int month = 5;
            int year = 2024;

            _mockCollaboratorService.Setup(service => service.GetReportAsync(month, year)).ReturnsAsync(new List<CollaboratorReportDto>());

            // Act
            var result = await _controller.GetReport(month, year);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
