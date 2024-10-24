
using Application.Models;

namespace Application.Usecases.RemovePlayerFromDepthChart{
    public interface IOutputPort
    {
        void Success(Player player);
        void Failure(string errorMessage);
        void BadRequest(string errorMessage);
        void NotFound();

    }
}