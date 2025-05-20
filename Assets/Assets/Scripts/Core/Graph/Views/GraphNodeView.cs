using Core;
using UnityEngine;

namespace Assets.Scripts.Core.Graph.Views
{
    public class GraphNodeView : MonoBehaviour
    {
        private GraphNodeModel _model;

        public void Initialize(GraphNodeModel model)
        {
            _model = model;
            name = $"Node_{model.NodeType}[{model.Id}]";
            transform.position = model.Position;
        }
    }
}