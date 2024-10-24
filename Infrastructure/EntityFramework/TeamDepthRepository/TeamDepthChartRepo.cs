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

        public async Task<List<Depth>> GetAllPosisionDepth()
        {
            return await _context.DepthChart
                .OrderBy(dc => dc.PositionId)
                .ThenBy(dc => dc.PositionDepth)
                .ToListAsync();
        }

        public async Task<Depth?> GetPlayerFromDepthChart(PositionAbbre positionAbbre, int playerNumber)
        {
            return await _context.DepthChart
                .FirstOrDefaultAsync(x => x.PlayerNumber == playerNumber && x.PositionId == (int)positionAbbre);
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

        public async Task RemovePlayerFromDepthChart(PositionAbbre positionAbbre, int playerNumber){
            var depthList = await GetPosisionDepth(positionAbbre);
            depthList.OrderBy(d => d.PositionDepth);

            var currentDepth = depthList.FirstOrDefault(x => x.PlayerNumber == playerNumber);
            depthList.Remove(currentDepth!);
            _context.DepthChart.Remove(currentDepth!);

            if(depthList.Count > 0){
                CalculateDepthHelper.CalculateDepthForDelete(depthList);
                foreach(var depth in depthList){
                    _context.DepthChart.Update(depth);
                }
            }
            await _context.SaveChangesAsync();
        }

         public async Task<List<Depth>> GetBackups(PositionAbbre positionAbbre, int playerNumber){
            
            var depthOnPostion = await GetPosisionDepth(positionAbbre);

            if(depthOnPostion.Count == 0 || !depthOnPostion.Any(x => x.PlayerNumber == playerNumber)){
                return new List<Depth>();
            }

            var currentPlayerPriority = depthOnPostion.FirstOrDefault(x => x.PlayerNumber == playerNumber)!.PositionDepth;

            return depthOnPostion.Where(d => d.PositionDepth > currentPlayerPriority).ToList();
        }
    }
}