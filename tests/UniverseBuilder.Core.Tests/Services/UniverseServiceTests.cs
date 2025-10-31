using System;
using System.Threading.Tasks;
using FluentAssertions;
using UniverseBuilder.Core.Models;
using UniverseBuilder.Core.Services;
using UniverseBuilder.Core.Repositories;
using Xunit;
using Moq;

namespace UniverseBuilder.Core.Tests.Services
{
    public class UniverseServiceTests
    {
        private readonly Mock<IUniverseRepository> _mockRepository;
        private readonly UniverseService _service;

        public UniverseServiceTests()
        {
            _mockRepository = new Mock<IUniverseRepository>();
            _service = new UniverseService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateUniverseAsync_WithValidData_ShouldCreateUniverse()
        {
            // Arrange
            var universe = new Universe
            {
                Name = "Test Universe",
                Description = "A test universe"
            };

            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Universe>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.CreateUniverseAsync(universe);

            // Assert
            _mockRepository.Verify(r => r.CreateAsync(It.Is<Universe>(u =>
                u.Name == "Test Universe" &&
                u.Description == "A test universe"
            )), Times.Once);
        }

        [Fact]
        public async Task CreateUniverseAsync_WithEmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var universe = new Universe
            {
                Name = "",
                Description = "A test universe"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _service.CreateUniverseAsync(universe)
            );

            exception.Message.Should().Contain("Universe name cannot be empty");
        }

        [Fact]
        public async Task CreateUniverseAsync_WithNullName_ShouldThrowArgumentException()
        {
            // Arrange
            var universe = new Universe
            {
                Name = null!,
                Description = "A test universe"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.CreateUniverseAsync(universe)
            );
        }

        [Fact]
        public async Task CreateUniverseAsync_WithWhitespaceName_ShouldThrowArgumentException()
        {
            // Arrange
            var universe = new Universe
            {
                Name = "   ",
                Description = "A test universe"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _service.CreateUniverseAsync(universe)
            );

            exception.Message.Should().Contain("Universe name cannot be empty");
        }

        [Fact]
        public async Task CreateUniverseAsync_ShouldSetCreatedAndModifiedDates()
        {
            // Arrange
            var universe = new Universe
            {
                Name = "Test Universe",
                Description = "A test universe"
            };

            Universe? capturedUniverse = null;
            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Universe>()))
                .Callback<Universe>(u => capturedUniverse = u)
                .Returns(Task.CompletedTask);

            var beforeCreate = DateTime.UtcNow;

            // Act
            await _service.CreateUniverseAsync(universe);

            var afterCreate = DateTime.UtcNow;

            // Assert
            capturedUniverse.Should().NotBeNull();
            capturedUniverse!.CreatedDate.Should().BeOnOrAfter(beforeCreate);
            capturedUniverse.CreatedDate.Should().BeOnOrBefore(afterCreate);
            capturedUniverse.ModifiedDate.Should().BeOnOrAfter(beforeCreate);
            capturedUniverse.ModifiedDate.Should().BeOnOrBefore(afterCreate);
        }

        [Fact]
        public async Task UpdateUniverseAsync_WithValidData_ShouldUpdateUniverse()
        {
            // Arrange
            var universe = new Universe
            {
                Id = Guid.NewGuid(),
                Name = "Updated Universe",
                Description = "Updated description",
                CreatedDate = DateTime.UtcNow.AddDays(-1)
            };

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Universe>()))
                .Returns(Task.CompletedTask);

            var beforeUpdate = DateTime.UtcNow;

            // Act
            await _service.UpdateUniverseAsync(universe);

            // Assert
            universe.ModifiedDate.Should().BeOnOrAfter(beforeUpdate);
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<Universe>(u =>
                u.Id == universe.Id &&
                u.Name == "Updated Universe"
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateUniverseAsync_WithEmptyName_ShouldThrowArgumentException()
        {
            // Arrange
            var universe = new Universe
            {
                Id = Guid.NewGuid(),
                Name = "",
                Description = "Updated description"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.UpdateUniverseAsync(universe)
            );
        }

        [Fact]
        public async Task GetAllUniversesAsync_ShouldReturnAllUniverses()
        {
            // Arrange
            var universes = new System.Collections.Generic.List<Universe>
            {
                new Universe { Name = "Universe 1" },
                new Universe { Name = "Universe 2" }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(universes);

            // Act
            var result = await _service.GetAllUniversesAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(u => u.Name == "Universe 1");
            result.Should().Contain(u => u.Name == "Universe 2");
        }

        [Fact]
        public async Task GetUniverseByIdAsync_WithValidId_ShouldReturnUniverse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var universe = new Universe
            {
                Id = id,
                Name = "Test Universe"
            };

            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(universe);

            // Act
            var result = await _service.GetUniverseByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be("Test Universe");
        }

        [Fact]
        public async Task DeleteUniverseAsync_WithValidId_ShouldDeleteUniverse()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockRepository.Setup(r => r.DeleteAsync(id))
                .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteUniverseAsync(id);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}
