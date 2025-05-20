using System;

namespace Core
{
    public class GraphService : IGraphService
    {
        private GraphModel _graph;

        public void SetGraph(GraphModel graph) =>
            _graph = graph;
        
        public GraphModel GetGraph()
        {
            if (_graph == null)
            {
                throw new InvalidOperationException("Graph has not been initialized yet.");
            }
            
            return _graph;
        }
    }
}