
using Application.Enums;

namespace Application.Usecases.AddPlayerToDepthChart{
    public interface IAddPlayerToDepthChartUsecase : IUseCase<IOutputPort>
    {
        public Task Execute(PositionAbbre positionAbbre, int playerNumber, int? positionDepth = null);
    }

}