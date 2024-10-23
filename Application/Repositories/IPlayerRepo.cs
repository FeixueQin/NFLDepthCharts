using Application.Models;

namespace Application.Repositories{
    public interface IPlayerRepo{
         public Task<Player> GetPlayer(int playerNumber);
    }
}