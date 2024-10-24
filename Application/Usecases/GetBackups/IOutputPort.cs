
using Application.Models;

namespace Application.Usecases.GetBackups{
    public interface IOutputPort
    {
        void Success(List<Player> players);
        void Failure(string errorMessage);
        void BadRequest(string errorMessage);
    }
}