using Application.Models;

namespace Application.Usecases.GetFullDepthChart{
     public interface IOutputPort
    {
        void Success(List<Depth> teamDepth);
        void Failure(string errorMessage);
    }
}