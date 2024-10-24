using Application.Enums;
using Application.Models;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace Infrastructure.Test.EntityFramework
{
    public class RemoveDepthChartTests : IDisposable
    {
        private readonly TeamDepthChartRepo _repo;
        private readonly ApplicationDbContext _context;

        public RemoveDepthChartTests()
        {
            // Set up in-memory database options
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseForRemove")
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
        public async Task RemovePlayerToDepthChart_ShouldRemovePlayerAtCorrectPosition()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 11, PositionDepth = 2, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 12, PositionDepth = 3, PositionId = (int)positionAbbre },
            };

            _context.DepthChart.AddRange(existingDepthList);
            await _context.SaveChangesAsync();

            var playerNumber = 11;

            // Act
            await _repo.RemovePlayerFromDepthChart(positionAbbre, playerNumber);

            // Assert
            var updatedDepthList = await _repo.GetPosisionDepth(positionAbbre);
            updatedDepthList.Should().HaveCount(2);
            updatedDepthList[0].PlayerNumber.Should().Be(10); // The player with higher priority should remain same
            updatedDepthList[0].PositionDepth.Should().Be(1);
            updatedDepthList[1].PlayerNumber.Should().Be(12); // The lower priority player should be shifted up
            updatedDepthList[1].PositionDepth.Should().Be(2);
        }

        [Fact]
        public async Task RemovePlayerToDepthChart_WhenOnlyPlayerExist_ShouldRemovePlayer()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(existingDepthList);
            await _context.SaveChangesAsync();

            var playerNumber = 10;

            // Act
            await _repo.RemovePlayerFromDepthChart(positionAbbre, playerNumber);

            // Assert
            var updatedDepthList = await _repo.GetPosisionDepth(positionAbbre);
            updatedDepthList.Should().HaveCount(0);
        }
    }
}