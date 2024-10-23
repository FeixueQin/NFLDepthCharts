using Application.Enums;
using Application.Models;

namespace Application.Repositories{
    public interface ITeamDepthChartRepo{
         public Task InsertPlayerToDepthChart(PositionAbbre positionAbbre, int playerNumber, int positionDepth);

         public Task<List<Depth>> GetPosisionDepth(PositionAbbre positionAbbre);

        //  public void UpdateItemOnDepthChart(Depth newEntry);
    }
}