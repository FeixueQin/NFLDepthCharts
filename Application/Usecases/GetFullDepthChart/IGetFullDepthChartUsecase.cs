
using Application.Enums;

namespace Application.Usecases.GetFullDepthChart{
    public interface IGetFullDepthChartUsecase : IUseCase<IOutputPort>
    {
        public Task Execute();
    }

}