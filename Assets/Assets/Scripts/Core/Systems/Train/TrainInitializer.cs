using DataBase;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TrainInitializer : IInitializable
    {
        private readonly IGraphService _graphService;
        private readonly ITrainFactory _trainFactory;
        private readonly GameSettings _gameSettings;

        public TrainInitializer(
            IGraphService graphService,
            ITrainFactory trainFactory,
            GameSettings gameSettings
        )
        {
            _graphService = graphService;
            _trainFactory = trainFactory;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            var graph = _graphService.GetGraph();
            var nodes = graph.Nodes;

            if (nodes == null || nodes.Count == 0)
            {
                Debug.LogWarning("[TrainInitializer] Graph has no nodes.");
                return;
            }

            var trainsCount = _gameSettings.InitialCountTrains;

            for (var iterator = 0; iterator < trainsCount; iterator++)
            {
                var spawnNode = nodes[Random.Range(0, nodes.Count - 1)];

                _trainFactory.Create(spawnNode);
            }
        }
    }
}