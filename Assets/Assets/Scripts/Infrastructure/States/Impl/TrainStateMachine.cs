using Core;
using UniRx;

namespace Infrastructure
{
    public class TrainStateMachine: ITrainAgent, ITrainContext
    {
        private ITrainState _currentState;
        
        private readonly TrainModel _model;
        private readonly TrainView _view;
        private readonly Subject<ITrainState> _stateChangeSubject = new();

        public TrainModel Model => _model;
        public TrainView View => _view;

        public IPathfindingService PathfindingService { get; }
        public IResourceStorageService ResourceStorage { get; }

        public TrainStateMachine(
            TrainModel model,
            TrainView view,
            IPathfindingService pathfindingService,
            IResourceStorageService resourceStorage
        )
        {
            _model = model;
            _view = view;
            PathfindingService = pathfindingService;
            ResourceStorage = resourceStorage;

            _stateChangeSubject.Subscribe(SetState).AddTo(_view);
        }
        
        public void RequestStateChange(ITrainState newState) =>
            _stateChangeSubject.OnNext(newState);
        
        public void SetState(ITrainState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter(this);
        }

        public void Tick() =>
            _currentState?.Tick();
    }
}