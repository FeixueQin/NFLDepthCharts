namespace Application.Usecases
{
    public interface IUseCase<in T>{
        public void SetOutputPort(T outputPort);
    }
}