using Application.Enums;
using Application.Models;

namespace Application.Helpers{

    public static class CalculateDepthHelper{
        public static void CalculateDepthForAdd(List<Depth> existingDepthList, PositionAbbre positionAbbre, int playerNumber, int positionDepth){
            var newEntry = new Depth{
                    PlayerNumber = playerNumber,
                    PositionDepth = positionDepth,
                    PositionId = (int) positionAbbre
                };

                for(var i = 0; i < existingDepthList.Count; i++){
                    if(positionDepth <= existingDepthList[i].PositionDepth){
                        existingDepthList.Insert(i, newEntry);
                        break;
                    }
                }

                if (!existingDepthList.Contains(newEntry))
                {
                    existingDepthList.Add(newEntry);
                }
        }
    }
    

}