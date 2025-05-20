using System.Collections.Generic;

namespace Core
{
    public class PathfindingResult
    {
        public Dictionary<GraphNodeModel, float> DistanceToNode { get; }
        public Dictionary<GraphNodeModel, GraphNodeModel> PreviousNode { get; }

        public PathfindingResult(
            Dictionary<GraphNodeModel, float> distanceToNode,
            Dictionary<GraphNodeModel, GraphNodeModel> previousNode
        )
        {
            DistanceToNode = distanceToNode;
            PreviousNode = previousNode;
        }
    }
}