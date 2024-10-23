using Application.Enums;
using Application.Helpers;
using Application.Models;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework{
    public class TeamDepthChartRepo : ITeamDepthChartRepo
    {
        private readonly ApplicationDbContext _context;

        public TeamDepthChartRepo(ApplicationDbContext context){
            _context = context;
        }

        public async Task<List<Depth>> GetPosisionDepth(PositionAbbre positionAbbre)
        {
            return await _context.DepthChart
                .Where(dc => dc.PositionId == (int)positionAbbre)
                .OrderBy(dc => dc.PositionDepth)
                .ToListAsync();
        }

        public async Task InsertPlayerToDepthChart(PositionAbbre positionAbbre, int playerNumber, int positionDepth)
        {
            var depthList = await GetPosisionDepth(positionAbbre);

            if(positionDepth == 0){
                _context.DepthChart.Add(new Depth{
                    PlayerNumber = playerNumber,
                    PositionDepth = depthList.Count + 1,
                    PositionId = (int) positionAbbre
                });
                
            }else{
                depthList.OrderBy(d => d.PositionDepth);
                var fouondItem = depthList.FirstOrDefault(d => d.PlayerNumber == playerNumber);
                if(fouondItem != null){
                    _context.DepthChart.Remove(fouondItem);
                    depthList.Remove(fouondItem!);
                }
                
                CalculateDepthHelper.CalculateDepthForAdd(depthList, positionAbbre, playerNumber, positionDepth);

                for(var i = 1; i <= depthList.Count; i++ ){
                    var currentDepth = depthList[i - 1];
                    currentDepth.PositionDepth = i;
                    _context.DepthChart.Update(currentDepth);
                }
            }
            await _context.SaveChangesAsync();
            
        }
    }
}