using Assets.Scripts.Core.Graph;
using Core;

namespace Assets.Scripts.Infrastructure.Factories
{
    public interface IGraphObjectFactory
    {
        public void CreateNodeView(GraphNodeModel node);
        public void CreateEdgeView(GraphEdgeModel edge);
    }
}