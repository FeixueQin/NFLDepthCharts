namespace Application.Usecases.AddPlayerToDepthChart{
    public interface IOutputPort
    {
        void Success();
        void Failure(string errorMessage);
        void BadRequest(string errorMessage);
    }
}