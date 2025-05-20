using System.Collections.Generic;
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
        private readonly TrainSettings _trainSettings;

        public TrainInitializer(
            IGraphService graphService,
            ITrainFactory trainFactory,
            TrainSettings trainSettings
        )
        {
            _graphService = graphService;
            _trainFactory = trainFactory;
            _trainSettings = trainSettings;
        }

        public void Initialize()
        {
            var graph = _graphService.GetGraph();
            var nodes = graph.Nodes;

            if (nodes == null || nodes.Count == 0)
                return;

            var availableNodes = new List<GraphNodeModel>(nodes);
            
            
            for (var iterator = 0; iterator < _trainSettings.Trains.Length; iterator++)
            {
                var trainData = _trainSettings.Trains[iterator];
                var randomIndex = Random.Range(0, availableNodes.Count);
                var spawnNode = availableNodes[randomIndex];

                _trainFactory.Create(spawnNode, trainData.SpeedMoving, trainData.TimeMining);

                availableNodes.RemoveAt(randomIndex);
            }
        }

    }
}