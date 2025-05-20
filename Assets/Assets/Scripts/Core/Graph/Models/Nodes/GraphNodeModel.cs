using System.Collections.Generic;
using Assets.Scripts.Core.Graph;
using Enums;
using UnityEngine;

namespace Core
{
    public abstract class GraphNodeModel
    {
        public string Id { get; }
        public EGraphNodeType Type { get; }
        public Vector3 Position { get; }
        public List<GraphEdgeModel> Edges { get; } = new();

        protected GraphNodeModel(string id, EGraphNodeType type, Vector3 position)
        {
            Id = id;
            Type = type;
            Position = position;
        }
    }
}