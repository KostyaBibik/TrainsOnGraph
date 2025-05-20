using Core;
using DataBase;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class TrainFactory : ITrainFactory
    {
        private readonly DiContainer _container;
        private readonly GameSettings _viewSettings;
        private readonly ITrainAgentService _agentService;
        private readonly IPathfindingService _pathfindingService;
        private readonly IResourceStorageService _resourceStorage;

        public TrainFactory(
            DiContainer container,
            GameSettings viewSettings,
            ITrainAgentService trainAgentService,
            IPathfindingService pathfindingService,
            IResourceStorageService resourceStorage
        )
        {
            _container = container;
            _viewSettings = viewSettings;
            _agentService = trainAgentService;
            _pathfindingService = pathfindingService;
            _resourceStorage = resourceStorage;
        }

        public TrainView Create(GraphNodeModel spawnNode, float speedMoving, float timeMining)
        {
            var view = _container.InstantiatePrefabForComponent<TrainView>(_viewSettings.TrainPrefab);
            view.transform.position = spawnNode.Position;

            var model = new TrainModel($"Train_{Random.Range(1000, 9999)}", spawnNode.Id, speedMoving, timeMining);
            view.Initialize(model);
            view.SetPosition(spawnNode.Position);

            var stateMachine = new TrainStateMachine(model, view, _pathfindingService, _resourceStorage);
            stateMachine.SetState(new MoveToNextNodeState());

            _agentService.Register(stateMachine);
            
            return view;
        }
    }
}