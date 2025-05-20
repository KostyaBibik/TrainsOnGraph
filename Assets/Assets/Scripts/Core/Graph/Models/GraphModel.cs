using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class GraphModel
    {
        private readonly List<GraphNodeModel> _nodes = new();
        private readonly List<GraphEdgeModel> _edges = new();

        public IReadOnlyList<GraphNodeModel> Nodes => _nodes;
        public IReadOnlyList<GraphEdgeModel> Edges => _edges;

        public void AddNode(GraphNodeModel node)
        {
            _nodes.Add(node);
        }

        public void AddEdge(GraphEdgeModel edge)
        {
            _edges.Add(edge);
            edge.From.Edges.Add(edge);
            edge.To.Edges.Add(edge);
        }

        public GraphNodeModel GetNodeById(int id) => 
            _nodes.FirstOrDefault(n => n.Id == id);
    }
}