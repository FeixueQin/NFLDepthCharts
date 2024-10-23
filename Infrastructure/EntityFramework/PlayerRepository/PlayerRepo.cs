using Application.Models;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework{
    public class PlayerRepo : IPlayerRepo
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepo(ApplicationDbContext context){
            _context = context;
        }

        public async Task<Player> GetPlayer(int playerNumber)
        {
            var player = await _context.Players.Where(p => p.PlayerNumber == playerNumber).FirstOrDefaultAsync();

            return player!;
        }
    }
}