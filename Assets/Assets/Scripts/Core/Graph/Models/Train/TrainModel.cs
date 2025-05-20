using System.Collections.Generic;

namespace Core
{
    public class TrainModel
    {
        private List<GraphNodeModel> _route;
        private int _currentNodeId;
        private bool _hasResource;
        
        private readonly string _id;
        private readonly float _speedMoving;
        private readonly float _timeMining;

        public string Id => _id;
        public bool HasResource => _hasResource;
        public int CurrentNodeId => _currentNodeId;
        public float SpeedMoving => _speedMoving;
        public float TimeMining => _timeMining;

        private int CurrentTargetIndex { get; set; } = -1;
        
        public TrainModel(string id, int currentNodeId, float speedMoving, float timeMining)
        {
            _id = id;
            _currentNodeId = currentNodeId;
            _speedMoving = speedMoving;
            _timeMining = timeMining;
        }

        public void SetRoute(List<GraphNodeModel> route)
        {
            _route = route;
            CurrentTargetIndex = 0;
        }
        
        public GraphNodeModel GetCurrentTarget()
        {
            if (_route == null || _route.Count == 0 || CurrentTargetIndex >= _route.Count)
            {
                return null;
            }
            
            return _route[CurrentTargetIndex];
        }

        public bool AdvanceToNextNode()
        {
            _currentNodeId = _route[CurrentTargetIndex].Id;
            
            if (CurrentTargetIndex < _route.Count - 1)
            {
                CurrentTargetIndex++;
                return true;
            }
            return false;
        }

        public void SetResourceStatus(bool flag)
            => _hasResource = flag;
    }
}