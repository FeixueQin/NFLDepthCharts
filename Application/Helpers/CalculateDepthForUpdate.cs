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

            // Insert player at the correct position
            bool inserted = false;
            for (var i = 0; i < existingDepthList.Count; i++) {
                if (positionDepth <= existingDepthList[i].PositionDepth) {
                    existingDepthList.Insert(i, newEntry);
                    inserted = true;
                    break;
                }
            }

            // If the new entry wasn't inserted (i.e., player goes at the end of the list)
            if (!inserted) {
                existingDepthList.Add(newEntry);
            }

            // Adjust the PositionDepth for all players in the list
            for (var i = 0; i < existingDepthList.Count; i++) {
                existingDepthList[i].PositionDepth = i + 1;
            }
        }


        public static void CalculateDepthForDelete(List<Depth> existingDepthList){
            // Adjust the PositionDepth for all players in the list
            for (var i = 0; i < existingDepthList.Count; i++) {
                existingDepthList[i].PositionDepth = i + 1;
            }
        }
    }
}