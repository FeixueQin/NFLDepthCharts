using Application.Models;
using Application.Enums;

namespace Web.Test.Mappers{
    public class FullDepthChartMapperTests
    {
        [Fact]
        public void MapDepthChart_ReturnsEmptyDictionary_WhenDepthChartEntriesIsNull()
        {
            // Arrange
            List<Depth> depthChartEntries = null;

            // Act
            var result = FullDepthChartMapper.MapDepthChart(depthChartEntries);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.TeamDepthData);
        }

        [Fact]
        public void MapDepthChart_ReturnsEmptyDictionary_WhenDepthChartEntriesIsEmpty()
        {
            // Arrange
            var depthChartEntries = new List<Depth>();

            // Act
            var result = FullDepthChartMapper.MapDepthChart(depthChartEntries);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.TeamDepthData);
        }

        [Fact]
        public void MapDepthChart_GroupsPlayersByPositionAbbreviation()
        {
            // Arrange
            var depthChartEntries = new List<Depth>
            {
                new Depth { PlayerNumber = 12, PositionId = (int)PositionAbbre.QB, PositionDepth = 1 },
                new Depth { PlayerNumber = 13, PositionId = (int)PositionAbbre.QB, PositionDepth = 2 },
                new Depth { PlayerNumber = 10, PositionId = (int)PositionAbbre.LWR, PositionDepth = 1 }
            };

            // Act
            var result = FullDepthChartMapper.MapDepthChart(depthChartEntries);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TeamDepthData.Count); // 2 positions: QB, LWR

            Assert.Contains("QB", result.TeamDepthData.Keys);
            Assert.Contains("LWR", result.TeamDepthData.Keys);

            var qbDepth = result.TeamDepthData["QB"];
            var lwrDepth = result.TeamDepthData["LWR"];

            Assert.Equal(2, qbDepth.Count); // 2 players in QB position
            Assert.Single(lwrDepth); // 1 player in LWR position

            // Check the players in the QB position
            Assert.Equal(12, qbDepth[0].PlayerNumber);
            Assert.Equal(1, qbDepth[0].PositionDepth);

            Assert.Equal(13, qbDepth[1].PlayerNumber);
            Assert.Equal(2, qbDepth[1].PositionDepth);

            // Check the player in the LWR position
            Assert.Equal(10, lwrDepth[0].PlayerNumber);
            Assert.Equal(1, lwrDepth[0].PositionDepth);
        }
    }

}