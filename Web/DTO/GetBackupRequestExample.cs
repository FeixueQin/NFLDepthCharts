using Application.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Web.DTO{

    public class GetBackupRequestExample : IExamplesProvider<GetBackupRequest>
    {
        public GetBackupRequest GetExamples()
        {
            return new GetBackupRequest(){
                PlayerNumber = 2,
                PositionAbbre = PositionAbbre.C
            };
        }
    }
}