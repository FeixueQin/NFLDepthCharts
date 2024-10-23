
using Application.Models;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.UseCases.Player1{
    [ApiController]
    [Route("[controller]")]
    public class GetPlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GetPlayerController(ApplicationDbContext context){
            _context = context;
        }

        [HttpGet]
        [Route("{playerNumber:int}")]
        public async Task<Player> GetPlayer(int playerNumber)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.PlayerNumber == playerNumber);

            return player;
        }
    }
}