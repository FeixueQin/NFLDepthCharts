
using Application.Enums;

namespace Application.Usecases.RemovePlayerFromDepthChart{
    public interface IRemovePlayerFromDepthChartUsecase : IUseCase<IOutputPort>
    {
        public Task Execute(PositionAbbre positionAbbre, int playerNumber);
    }

}