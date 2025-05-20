using Enums;
using UnityEngine;

namespace Core
{
    public class EmptyNodeModel : GraphNodeModel
    {
        public EmptyNodeModel(int id, Vector3 position)
            : base(id, EGraphNodeType.Empty, position)
        {
        }
    }
}