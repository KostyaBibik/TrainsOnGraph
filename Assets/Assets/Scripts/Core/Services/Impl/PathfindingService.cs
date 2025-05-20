using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Graph;
using Enums;
using UnityEngine;

namespace Core
{
   public class PathfindingService : IPathfindingService
    {
        private readonly IGraphService _graphService;

        public PathfindingService(IGraphService graphService)
        {
            _graphService = graphService;
        }

        public GraphEdgeModel GetEdge(int fromNodeId, int toNodeId)
        {
            var edges = _graphService.GetGraph().Edges;
            
            var edge = edges.FirstOrDefault(e => e.From.Id == fromNodeId && e.To.Id == toNodeId);

            if (edge != null)
                return edge;

            edge = edges.FirstOrDefault(e => e.From.Id == toNodeId && e.To.Id == fromNodeId);

            return edge;
        }
        
        public Vector3 GetNodePosition(int nodeId)
        {
            var nodes = _graphService.GetGraph().Nodes;
            var node = nodes.FirstOrDefault(n => n.Id == nodeId);
            return node?.Position ?? Vector3.zero;
        }
        
        public IReadOnlyList<GraphNodeModel> CalculateRoute(int startNodeId, EGraphNodeType nodeType)
        {
            var graph = _graphService.GetGraph();
            var startNode = graph.GetNodeById(startNodeId);

            if (startNode == null)
                return new List<GraphNodeModel>();

            var targetNodes = graph.Nodes.Where(n => n.Type == nodeType).ToList();

            if (!targetNodes.Any())
                return new List<GraphNodeModel>();

            var pathfindingResult = FindShortestPathsFromNode(startNode, graph.Nodes);

            var closestTarget = FindClosestTargetNode(targetNodes, pathfindingResult.DistanceToNode);

            if (closestTarget == null)
                return new List<GraphNodeModel>();

            var path = BuildPathFromResults(pathfindingResult.PreviousNode, startNode, closestTarget);
            return path;
        }

        private PathfindingResult FindShortestPathsFromNode(GraphNodeModel startNode, IEnumerable<GraphNodeModel> allNodes)
        {
            var distances = new Dictionary<GraphNodeModel, float>();
            var previousNodes = new Dictionary<GraphNodeModel, GraphNodeModel>();
            var unvisitedNodes = new HashSet<GraphNodeModel>(allNodes);

            foreach (var node in allNodes)
            {
                distances[node] = float.MaxValue;
                previousNodes[node] = null;
            }

            distances[startNode] = 0f;

            while (unvisitedNodes.Count > 0)
            {
                var currentNode = unvisitedNodes.OrderBy(n => distances[n]).FirstOrDefault();

                if (currentNode == null || Mathf.Approximately(distances[currentNode], float.MaxValue))
                    break;

                unvisitedNodes.Remove(currentNode);

                foreach (var edge in currentNode.Edges)
                {
                    var neighbor = edge.To;
                    if (!unvisitedNodes.Contains(neighbor))
                        continue;

                    var tentativeDistance = distances[currentNode] + edge.Length;

                    if (tentativeDistance < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDistance;
                        previousNodes[neighbor] = currentNode;
                    }
                }
            }

            return new PathfindingResult(distances, previousNodes);
        }

        private GraphNodeModel FindClosestTargetNode(List<GraphNodeModel> targets, Dictionary<GraphNodeModel, float> distances)
        {
            GraphNodeModel closest = null;
            var minDistance = float.MaxValue;

            foreach (var target in targets)
            {
                if (distances.TryGetValue(target, out var dist) && dist < minDistance)
                {
                    minDistance = dist;
                    closest = target;
                }
            }

            return closest;
        }

        private List<GraphNodeModel> BuildPathFromResults(Dictionary<GraphNodeModel, GraphNodeModel> previousNodes,
            GraphNodeModel startNode, GraphNodeModel endNode)
        {
            var path = new List<GraphNodeModel>();
            var current = endNode;

            while (current != null)
            {
                path.Add(current);
                if (current == startNode)
                    break;
                current = previousNodes[current];
            }

            path.Reverse();
            return path;
        }
    }
}