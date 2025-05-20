using System;
using Core;
using UniRx;

namespace Infrastructure
{
    public class UnloadResourceState : ITrainState
    {
        private ITrainContext _context;
        private IDisposable _unloadSubscription;
        
        private readonly float _unloadTime = 1f;
        private readonly BaseNodeModel _baseStation;

        public UnloadResourceState(GraphNodeModel baseStationNode)
        {
            _baseStation = (BaseNodeModel) baseStationNode;
        }
        
        public void Enter(ITrainContext context)
        {
            _context = context;

            if (!_context.Model.HasResource)
            {
                SwapState();
                return;
            }
            
            _unloadSubscription = Observable.Timer(TimeSpan.FromSeconds(_unloadTime))
                .Subscribe(_ =>
                {
                    _context.Model.SetResourceStatus(false);
                    _context.ResourceStorage.AddResource(_baseStation.ResourceMultiplier);
                    SwapState();
                });
        }

        private void SwapState() =>
            _context.RequestStateChange(new MoveToNextNodeState());

        public void Tick()
        {
        }

        public void Exit()
        {
            _unloadSubscription?.Dispose();
            _unloadSubscription = null;
            _context = null;
        }
    }
}