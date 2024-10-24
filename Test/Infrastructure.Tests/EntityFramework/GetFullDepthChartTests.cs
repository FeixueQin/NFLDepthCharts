using Application.Enums;
using Application.Models;
using FluentAssertions;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.EntityFramework
{
    public class TeamDepthChartRepoTests : IDisposable
    {
        private readonly TeamDepthChartRepo _repo;
        private readonly ApplicationDbContext _context;

        public TeamDepthChartRepoTests()
        {
            // Set up in-memory database options
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase-GetFullDepth")
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
        public async Task GetAllPosisionDepth_ShouldReturnAllDepthEntriesInOrder()
        {
            // Arrange
            var depthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 2, PositionId = (int)PositionAbbre.LWR },
                new Depth { PlayerNumber = 12, PositionDepth = 1, PositionId = (int)PositionAbbre.QB },
                new Depth { PlayerNumber = 11, PositionDepth = 1, PositionId = (int)PositionAbbre.LWR },
                new Depth { PlayerNumber = 13, PositionDepth = 2, PositionId = (int)PositionAbbre.QB }
            };

            _context.DepthChart.AddRange(depthList);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repo.GetAllPosisionDepth();

            // Assert
            result.Should().HaveCount(4);
            result[0].PlayerNumber.Should().Be(11); // LWR PositionDepth 1 should come first
            result[1].PlayerNumber.Should().Be(10); // LWR PositionDepth 2 should come second
            result[2].PlayerNumber.Should().Be(12); // QB PositionDepth 1 should come next
            result[3].PlayerNumber.Should().Be(13); // QB PositionDepth 2 should be last
        }

        [Fact]
        public async Task GetAllPosisionDepth_ShouldReturnEmptyList_WhenNoEntriesExist()
        {
            // Act
            var result = await _repo.GetAllPosisionDepth();

            // Assert
            result.Should().BeEmpty();
        }
    }
}