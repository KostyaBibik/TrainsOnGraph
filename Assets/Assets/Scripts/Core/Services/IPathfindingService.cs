using System.Collections.Generic;
using Assets.Scripts.Core.Graph;
using Enums;
using UnityEngine;

namespace Core
{
    public interface IPathfindingService
    {
        public IReadOnlyList<GraphNodeModel> CalculateRoute(int startNodeId, EGraphNodeType nodeType);
        public GraphEdgeModel GetEdge(int fromNodeId, int toNodeId);
        public Vector3 GetNodePosition(int nodeId);
    }
}