using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Graph;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Infrastructure.Factories;
using Core;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Systems
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
                var node = new GraphNodeModel(
                    waypoint.Id,
                    waypoint.Type,
                    waypoint.Position,
                    waypoint.Multiplier
                );

                graph.AddNode(node);
                _waypointToNodeMap[waypoint] = node;

                _factory.CreateNodeView(node);
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

                    if (String.CompareOrdinal(fromNode.Id, toNode.Id) >= 0)
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