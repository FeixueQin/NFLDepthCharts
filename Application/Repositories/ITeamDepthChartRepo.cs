using Application.Enums;
using Application.Models;

namespace Application.Repositories{
    public interface ITeamDepthChartRepo{
        
         public Task<List<Depth>> GetAllPosisionDepth();
         public Task InsertPlayerToDepthChart(PositionAbbre positionAbbre, int playerNumber, int positionDepth);

         public Task<List<Depth>> GetPosisionDepth(PositionAbbre positionAbbre);

         public Task<Depth?> GetPlayerFromDepthChart(PositionAbbre positionAbbre, int playerNumber);

         public Task RemovePlayerFromDepthChart(PositionAbbre positionAbbre, int playerNumber);

         public Task<List<Depth>> GetBackups(PositionAbbre positionAbbre, int playerNumber);
    }
}