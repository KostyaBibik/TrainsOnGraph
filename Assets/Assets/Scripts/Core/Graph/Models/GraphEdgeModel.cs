using Core;

namespace Assets.Scripts.Core.Graph
{
    public class GraphEdgeModel
    {
        public GraphNodeModel From;
        public GraphNodeModel To;
        public float Length;
        
        public GraphEdgeModel(GraphNodeModel from, GraphNodeModel to, float length)
        {
            From = from;
            To = to;
            Length = length;
        }
    }
}