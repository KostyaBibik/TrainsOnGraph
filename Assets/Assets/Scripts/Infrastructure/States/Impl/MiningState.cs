using System;
using Core;
using Infrastructure.Impl;
using UniRx;

namespace Infrastructure 
{
    public class MiningState : ITrainState
    {
        private ITrainContext _context;
        private IDisposable _miningSubscription;
        private bool _isMining;
        
        private readonly MineStationNodeModel _mineStation;

        public MiningState(GraphNodeModel miningNode)
        {
            _mineStation = (MineStationNodeModel) miningNode;
        }

        public void Enter(ITrainContext context)
        {
            _context = context;
            _isMining = true;

            var miningTime = _context.Model.TimeMining;
            var miningTimeMultiplier = _mineStation.MiningTimeMultiplier;
            var miningDuration = miningTime * miningTimeMultiplier;

            _miningSubscription = Observable.Timer(TimeSpan.FromSeconds(miningDuration))
                .Subscribe(_ =>
                {
                    if (_isMining)
                    {
                        _context.Model.SetResourceStatus(true);
                        _isMining = false;
                        
                        _context.RequestStateChange(new MoveToNextNodeState());
                    }
                });
        }
        
        public void Tick()
        {
        }

        public void Exit()
        {
            _isMining = false;
            _miningSubscription?.Dispose();
            _miningSubscription = null;
            _context = null;
        }
    }
}