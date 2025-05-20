using Core;
using DataBase;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GraphObjectFactory : IGraphObjectFactory
    {
        private readonly DiContainer _container;
        private readonly GameSettings _settings;

        public GraphObjectFactory(DiContainer container, GameSettings settings)
        {
            _container = container;
            _settings = settings;
        }

        public void CreateNodeView(GraphNodeModel node)
        {
            var prefab = _settings.GetNodePrefabByType(node.Type);

            var instance = _container.InstantiatePrefab(prefab, node.Position, Quaternion.identity, null);
            var view = instance.GetComponent<GraphNodeView>();
            view.Initialize(node);
        }

        public void CreateEdgeView(GraphEdgeModel edge)
        {
            var instance = _container.InstantiatePrefab(_settings.EdgePrefab);
            var view = instance.GetComponent<GraphEdgeView>();
            view.Initialize(edge);
        }
    }
}