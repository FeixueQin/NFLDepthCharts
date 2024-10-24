using Application.Models;
using Application.Enums;


namespace Web.Test.Mappers{
    public class PlayerMapperTests
    {
        [Fact]
        public void MapToResponseData_ShouldMapPlayerToPlayerResponseData()
        {
            // Arrange
            var player = new Player
            {
                PlayerNumber = 12,
                Name = "Tom Brady",
                PositionId = (int)PositionAbbre.QB // Assume player is a QB
            };

            // Act
            var result = PlayerMapper.MapToResponseData(player);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(12, result.PlayerNumber);
            Assert.Equal("Tom Brady", result.Name);
            Assert.Equal("QB", result.PositionAbbre); // PositionAbbre enum maps to "QB"
        }
    }
}

