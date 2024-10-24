using Application.Enums;
using Application.Models;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace Infrastructure.Test.EntityFramework
{
    public class GetBackupsTests : IDisposable
    {
        private readonly TeamDepthChartRepo _repo;
        private readonly ApplicationDbContext _context;

        public GetBackupsTests()
        {
            // Set up in-memory database options
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase-GetBackups")
                .Options;

            // Create the in-memory context
            _context = new ApplicationDbContext(options);
            _repo = new TeamDepthChartRepo(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated(); 
        }


        [Fact]
        public async Task GetBackups_ShouldReturnEmptyList_WhenNoPlayersInDepth()
        {
            // Arrange
            var positionAbbre = PositionAbbre.QB;

            // Act
            var result = await _repo.GetBackups(positionAbbre, 12); // No players added yet

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBackups_ShouldReturnEmptyList_WhenPlayerNotFound()
        {
            // Arrange
            var positionAbbre = PositionAbbre.QB;
            var depthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 11, PositionDepth = 2, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(depthList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repo.GetBackups(positionAbbre, 12); // PlayerNumber 12 not found

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBackups_ShouldReturnBackups_WhenPlayerHasBackups()
        {
            // Arrange
            var positionAbbre = PositionAbbre.QB;
            var depthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 12, PositionDepth = 2, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 13, PositionDepth = 3, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(depthList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repo.GetBackups(positionAbbre, 12); // PlayerNumber 12 has backups

            // Assert
            result.Should().HaveCount(1);
            result[0].PlayerNumber.Should().Be(13); // Backup for PlayerNumber 12
        }

        [Fact]
        public async Task GetBackups_ShouldReturnEmptyList_WhenPlayerHasNoBackups()
        {
            // Arrange
            var positionAbbre = PositionAbbre.QB;
            var depthList = new List<Depth>
            {
                new Depth { PlayerNumber = 12, PositionDepth = 1, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(depthList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repo.GetBackups(positionAbbre, 12); // PlayerNumber 12 has no backups

            // Assert
            result.Should().BeEmpty();
        }
        
    }
}