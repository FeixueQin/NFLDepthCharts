using Application.Models;

namespace Application.Helpers{
    public static class PriorityQueueHelper{
        public static PriorityQueue<Depth, int> BuildPriorityQueue(List<Depth> list){
            var priorityQueue = new PriorityQueue<Depth, int>();
            foreach(var depth in list){
                priorityQueue.Enqueue(depth, depth.PositionDepth);
            }
            // var result = new List<Depth>();
            // for(var i = 1 ; i <= priorityQueue.Count; i++){
            //     var depth = priorityQueue.Dequeue();
            //     depth.PositionDepth = i;
            //     result.Add(depth);
            // }
            return priorityQueue;
        }
        
    }
}