using Application.Models;
using Application.Enums;
using Web.DTO;

public static class PlayerMapper
{
    public static PlayerResponseData MapToResponseData(Player player)
    {
        return new PlayerResponseData
        {
            PlayerNumber = player.PlayerNumber,
            Name = player.Name,
            PositionAbbre = ((PositionAbbre)player.PositionId).ToString()
        };
    }
}