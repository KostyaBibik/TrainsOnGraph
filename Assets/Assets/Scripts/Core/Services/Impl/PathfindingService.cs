using System.Collections.Generic;
using System.Linq;
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
        
        public IReadOnlyList<GraphNodeModel> CalculateRoute(int startNodeId, EGraphNodeType targetType, TrainModel train)
        {
            var graph = _graphService.GetGraph();
            var startNode = graph.GetNodeById(startNodeId);

            if (startNode == null)
                return new List<GraphNodeModel>();

            var targetNodes = graph.Nodes.Where(n => n.Type == targetType).ToList();
            if (!targetNodes.Any())
                return new List<GraphNodeModel>();

            var pathfindingResult = FindShortestPathsFromNode(startNode, graph.Nodes);

            GraphNodeModel bestTarget = null;
            var bestScore = targetType == EGraphNodeType.MineStation ? float.MaxValue : float.MinValue;

            foreach (var target in targetNodes)
            {
                if (!pathfindingResult.DistanceToNode.TryGetValue(target, out var distance))
                    continue;

                float travelTime = distance / train.SpeedMoving;
                float score;

                if (targetType == EGraphNodeType.MineStation && target is MineStationNodeModel mine)
                {
                    var miningTime = train.TimeMining * mine.MiningTimeMultiplier;
                    score = travelTime + miningTime;

                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestTarget = target;
                    }
                }
                else if (targetType == EGraphNodeType.Base && target is BaseNodeModel baseNode)
                {
                    score = baseNode.ResourceMultiplier / travelTime;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestTarget = target;
                    }
                }
            }

            if (bestTarget == null)
                return new List<GraphNodeModel>();

            var path = BuildPathFromResults(pathfindingResult.PreviousNode, startNode, bestTarget);
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