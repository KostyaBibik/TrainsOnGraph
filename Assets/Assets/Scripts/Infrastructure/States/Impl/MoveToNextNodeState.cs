using System.Collections.Generic;
using Core;
using Enums;
using UnityEngine;

namespace Infrastructure.Impl
{
    public class MoveToNextNodeState : ITrainState
    {
        private ITrainContext _context;
        private float _progress;
        private float _speed;

        private Vector3 _start;
        private Vector3 _target;
        private GraphNodeModel _endNode;

        // Новое поле — длина текущего ребра
        private float _currentEdgeLength;

        public void Enter(ITrainContext context)
        {
            _context = context;
            _speed = _context.Model.SpeedMoving;

            var targetNodeType = _context.Model.HasResource
                ? EGraphNodeType.Base
                : EGraphNodeType.MineStation;

            var route = new List<GraphNodeModel>(_context.PathfindingService.CalculateRoute(_context.Model.CurrentNodeId, targetNodeType));
            _endNode = route[^1];
            _context.Model.SetRoute(route);

            _progress = 0f;
            _start = _context.View.transform.position;

            SetNextTargetAndEdge();
        }

        private void SetNextTargetAndEdge()
        {
            var currentTarget = _context.Model.GetCurrentTarget();
            if (currentTarget == null)
            {
                _context = null;
                return;
            }

            _target = currentTarget.Position;

            var currentNodeId = _context.Model.CurrentNodeId;
            var nextNodeId = currentTarget.Id;

            // Получаем длину ребра между текущей нодой и целью из модели графа
            _currentEdgeLength = GetEdgeLength(currentNodeId, nextNodeId);
        }

        private float GetEdgeLength(int fromNodeId, int toNodeId)
        {
            // Предположим, у PathfindingService есть метод для получения ребра:
            var edge = _context.PathfindingService.GetEdge(fromNodeId, toNodeId);
            if (edge != null)
            {
                return edge.Length;
            }

            // Если ребро не найдено, fallback на расстояние в мире:
            var fromPos = _context.Model.CurrentNodeId == fromNodeId 
                ? _context.View.transform.position
                : _context.PathfindingService.GetNodePosition(fromNodeId);
            var toPos = _context.PathfindingService.GetNodePosition(toNodeId);

            return Vector3.Distance(fromPos, toPos);
        }

        public void Tick()
        {
            if (_context == null)
                return;

            _progress += Time.deltaTime * _speed / _currentEdgeLength;

            _context.View.transform.position = Vector3.Lerp(_start, _target, _progress);

            if (_progress < 1f) 
                return;

            // Завершили движение по ребру
            var hasNext = _context.Model.AdvanceToNextNode();

            if (hasNext)
            {
                _start = _target;
                _progress = 0f;
                SetNextTargetAndEdge();
            }
            else
            {
                if(!_context.Model.HasResource)
                {
                    _context.RequestStateChange(new MiningState(_endNode));
                }
                else
                {
                    _context.RequestStateChange(new UnloadResourceState(_endNode));
                }
            }
        }

        public void Exit()
        {
            _context = null;
        }
    }
}
