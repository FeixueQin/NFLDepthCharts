using Application.Enums;
using Application.Models;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace Infrastructure.Test.EntityFramework
{
    public class InsertDepthChartTests : IDisposable
    {
        private readonly TeamDepthChartRepo _repo;
        private readonly ApplicationDbContext _context;

        public InsertDepthChartTests()
        {
            // Set up in-memory database options
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
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
        public async Task GetPosisionDepth_ShouldReturnCorrectDepthList()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var depthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 11, PositionDepth = 2, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(depthList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repo.GetPosisionDepth(positionAbbre);

            // Assert
            result.Should().HaveCount(2);
            result[0].PlayerNumber.Should().Be(10);
            result[1].PlayerNumber.Should().Be(11);
        }

        [Fact]
        public async Task InsertPlayerToDepthChart_ShouldAddPlayerAtCorrectPosition()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 11, PositionDepth = 2, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(existingDepthList);
            await _context.SaveChangesAsync();

            var newPlayerNumber = 12;
            var positionDepth = 1; // Insert at the beginning

            // Act
            await _repo.InsertPlayerToDepthChart(positionAbbre, newPlayerNumber, positionDepth);

            // Assert
            var updatedDepthList = await _repo.GetPosisionDepth(positionAbbre);
            updatedDepthList.Should().HaveCount(3);
            updatedDepthList[0].PlayerNumber.Should().Be(newPlayerNumber); // New player should be first
            updatedDepthList[0].PositionDepth.Should().Be(1);
            updatedDepthList[1].PlayerNumber.Should().Be(10); // Original players should be shifted down
            updatedDepthList[1].PositionDepth.Should().Be(2);
            updatedDepthList[2].PlayerNumber.Should().Be(11);
            updatedDepthList[2].PositionDepth.Should().Be(3);
        }

        [Fact]
        public async Task InsertPlayerToDepthChart_ShouldRemoveExistingPlayerAndAddNew()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)positionAbbre },
                new Depth { PlayerNumber = 12, PositionDepth = 2, PositionId = (int)positionAbbre }
            };

            _context.DepthChart.AddRange(existingDepthList);
            await _context.SaveChangesAsync();

            var newPlayerNumber = 12; // This player already exists
            var positionDepth = 1; 

            // Act
            await _repo.InsertPlayerToDepthChart(positionAbbre, newPlayerNumber, positionDepth);

            // Assert
            var updatedDepthList = await _repo.GetPosisionDepth(positionAbbre);
            updatedDepthList.Should().HaveCount(2);
            updatedDepthList[0].PlayerNumber.Should().Be(newPlayerNumber); // The player should still be present
            updatedDepthList[0].PositionDepth.Should().Be(1);
            updatedDepthList[1].PlayerNumber.Should().Be(10); // Original player should be shifted down
            updatedDepthList[1].PositionDepth.Should().Be(2);
        }
    }
}