using System.Collections.Generic;
using Assets.Scripts.Core.Graph;
using Enums;
using UnityEngine;

namespace Core
{
    public abstract class GraphNodeModel
    {
        public int Id { get; }
        public EGraphNodeType Type { get; }
        public Vector3 Position { get; }
        public List<GraphEdgeModel> Edges { get; } = new();

        protected GraphNodeModel(int id, EGraphNodeType type, Vector3 position)
        {
            Id = id;
            Type = type;
            Position = position;
        }
    }
}