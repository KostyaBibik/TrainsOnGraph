using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Core
{
    public interface IPathfindingService
    {
        public IReadOnlyList<GraphNodeModel> CalculateRoute(int startNodeId, EGraphNodeType targetType, TrainModel train);
        public GraphEdgeModel GetEdge(int fromNodeId, int toNodeId);
        public Vector3 GetNodePosition(int nodeId);
    }
}