using Application.Enums;
using Application.Models;
using Application.Helpers;
using FluentAssertions;

namespace Application.Tests.Helpers
{
    public class CalculateDepthHelperTests
    {
        [Fact]
        public void CalculateDepthForAdd_ShouldAddPlayerToCorrectPosition()
        {
            // Arrange
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)PositionAbbre.LWR },
                new Depth { PlayerNumber = 11, PositionDepth = 2, PositionId = (int)PositionAbbre.LWR }
            };
            var newPlayerNumber = 12;
            var positionAbbre = PositionAbbre.LWR;
            var positionDepth = 2;

            // Act
            CalculateDepthHelper.CalculateDepthForAdd(existingDepthList, positionAbbre, newPlayerNumber, positionDepth);

            // Assert
            existingDepthList.Should().HaveCount(3);
            existingDepthList[1].PlayerNumber.Should().Be(newPlayerNumber);
            existingDepthList[1].PositionDepth.Should().Be(positionDepth);

            // Ensure that the depths are updated correctly
            existingDepthList[0].PositionDepth.Should().Be(1);
            existingDepthList[2].PositionDepth.Should().Be(3); // The player after should be moved down
        }

        [Fact]
        public void CalculateDepthForAdd_ShouldAddPlayerToEndWhenNoPositionDepthProvided()
        {
            // Arrange
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)PositionAbbre.LWR }
            };
            var newPlayerNumber = 12;
            var positionAbbre = PositionAbbre.LWR;
            var positionDepth = 2; // Indicating it should be added to the end

            // Act
            CalculateDepthHelper.CalculateDepthForAdd(existingDepthList, positionAbbre, newPlayerNumber, positionDepth);

            // Assert
            existingDepthList.Should().HaveCount(2);
            existingDepthList[1].PlayerNumber.Should().Be(newPlayerNumber);
            existingDepthList[1].PositionDepth.Should().Be(2); // Should be placed at the end
        }

        [Fact]
        public void CalculateDepthForDelete_ShouldAdjustPositionDepthsCorrectly()
        {
            // Arrange
            var existingDepthList = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1 },
                new Depth { PlayerNumber = 11, PositionDepth = 2 },
                new Depth { PlayerNumber = 12, PositionDepth = 3 }
            };

            // Simulate removal of the player at PositionDepth 2
            existingDepthList.RemoveAt(1);

            // Act
            CalculateDepthHelper.CalculateDepthForDelete(existingDepthList);

            // Assert
            existingDepthList.Should().HaveCount(2);
            existingDepthList[0].PositionDepth.Should().Be(1);
            existingDepthList[1].PositionDepth.Should().Be(2);
        }
    }
}