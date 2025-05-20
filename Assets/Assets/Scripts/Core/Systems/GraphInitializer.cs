using System;
using System.Collections.Generic;
using Enums;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GraphInitializer : IInitializable
    {
        private readonly IGraphObjectFactory _factory;
        private readonly IGraphService _graphService;
        private readonly List<GraphWaypoint> _waypoints;
        
        private readonly Dictionary<GraphWaypoint, GraphNodeModel> _waypointToNodeMap = new();

        public GraphInitializer(IGraphObjectFactory factory, SceneHandler sceneHandler, IGraphService graphService)
        {
            _factory = factory;
            _graphService = graphService;
            _waypoints = sceneHandler.Waypoints;
        }

        public void Initialize()
        {
            var graph = new GraphModel();
            
            CreateGraphNodes(graph);
            CreateGraphEdges(graph);
            
            _graphService.SetGraph(graph);
        }

        private void CreateGraphNodes(GraphModel graph)
        {
            foreach (var waypoint in _waypoints)
            {
                var nodeModel = CreateNodeModel(waypoint);

                graph.AddNode(nodeModel);
                _waypointToNodeMap[waypoint] = nodeModel;

                _factory.CreateNodeView(nodeModel);
            }
        }
        
        private GraphNodeModel CreateNodeModel(GraphWaypoint waypoint)
        {
            switch (waypoint.Type)
            {
                case EGraphNodeType.Base:
                    return new BaseNodeModel(waypoint.Id, waypoint.Position, waypoint.Multiplier);

                case EGraphNodeType.MineStation:
                    return new MineStationNodeModel(waypoint.Id, waypoint.Position, waypoint.Multiplier);

                case EGraphNodeType.Empty:
                    return new EmptyNodeModel(waypoint.Id, waypoint.Position);

                default:
                    throw new ArgumentException($"Unsupported node type {waypoint.Type}");
            }
        }
 
        
        private void CreateGraphEdges(GraphModel graph)
        {
            foreach (var waypoint in _waypoints)
            {
                var fromNode = _waypointToNodeMap[waypoint];

                foreach (var connection in waypoint.Neighbors)
                {
                    var neighborWaypoint = connection.Neighbor;
                    var toNode = _waypointToNodeMap[neighborWaypoint];

                    if (fromNode.Id == toNode.Id)
                        continue;

                    var length = connection.EdgeLength > 0f
                        ? connection.EdgeLength
                        : Vector3.Distance(fromNode.Position, toNode.Position);

                    var edge = new GraphEdgeModel(fromNode, toNode, length);

                    fromNode.Edges.Add(edge);
                    toNode.Edges.Add(edge);

                    graph.AddEdge(edge);
                    _factory.CreateEdgeView(edge);
                }
            }
        }

    }
}