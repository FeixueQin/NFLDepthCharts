
using Application.Enums;

namespace Application.Usecases.GetBackups{
    public interface IGetBackupsUsecase : IUseCase<IOutputPort>
    {
        public Task Execute(PositionAbbre positionAbbre, int playerNumber);
    }

}