using System;
using Core;

namespace Infrastructure
{
    public interface ITrainContext
    {
        public TrainModel Model { get; }
        public TrainView View { get; }
        public IPathfindingService PathfindingService { get; }
        public IResourceStorageService ResourceStorage { get; }
        
        public void RequestStateChange(ITrainState newState);
    }
}