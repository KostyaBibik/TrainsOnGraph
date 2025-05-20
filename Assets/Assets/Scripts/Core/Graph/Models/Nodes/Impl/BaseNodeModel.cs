using Enums;
using UnityEngine;

namespace Core
{
    public class BaseNodeModel : GraphNodeModel
    {
        public float ResourceMultiplier { get; set; }

        public BaseNodeModel(int id, Vector3 position, float resourceMultiplier)
            : base(id, EGraphNodeType.Base, position)
        {
            ResourceMultiplier = resourceMultiplier;
        }
    }
}