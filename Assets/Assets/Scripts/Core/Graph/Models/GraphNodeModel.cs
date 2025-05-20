using System.Collections.Generic;
using Assets.Scripts.Core.Graph;
using Enums;
using UnityEngine;

namespace Core
{
    public class GraphNodeModel
    {
        public readonly string Id;
        public EGraphNodeType NodeType;
        public Vector3 Position;
        public float Multiplier; 

        public List<GraphEdgeModel> Edges = new();

        public GraphNodeModel(string id, EGraphNodeType nodeType, Vector3 position, float multiplier)
        {
            Id = id;
            NodeType = nodeType;
            Position = position;
            Multiplier = multiplier;
        }
    }
}