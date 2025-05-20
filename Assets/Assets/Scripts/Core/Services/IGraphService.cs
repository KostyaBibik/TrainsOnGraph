using Assets.Scripts.Core.Graph;

namespace Core
{
    public interface IGraphService
    {
        public void SetGraph(GraphModel graph);
        public GraphModel GetGraph();
    }
}