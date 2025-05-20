using UnityEngine;

namespace Core
{
    public class GraphEdgeView : MonoBehaviour
    {
        private LineRenderer _line;

        public void Initialize(GraphEdgeModel edge)
        {
            if (_line == null)
                _line = GetComponent<LineRenderer>();

            _line.positionCount = 2;
            _line.SetPosition(0, edge.From.Position);
            _line.SetPosition(1, edge.To.Position);
            name = $"Edge_{edge.From.Id}_{edge.To.Id}";
        }
    }
}