using Assets.Scripts.Core.Graph;
using Assets.Scripts.Core.Graph.Views;
using Core;
using DataBase;
using Enums;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Factories.Impl
{
    public class GraphObjectFactory : IGraphObjectFactory
    {
        private readonly DiContainer _container;
        private readonly GraphViewSettings _settings;

        public GraphObjectFactory(DiContainer container, GraphViewSettings settings)
        {
            _container = container;
            _settings = settings;
        }

        public void CreateNodeView(GraphNodeModel node)
        {
            var prefab = node.NodeType == EGraphNodeType.Base
                ? _settings.BaseNodePrefab
                : _settings.MineStationNodePrefab;

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