using Core;

namespace Infrastructure
{
    public interface IGraphObjectFactory
    {
        public void CreateNodeView(GraphNodeModel node);
        public void CreateEdgeView(GraphEdgeModel edge);
    }
}