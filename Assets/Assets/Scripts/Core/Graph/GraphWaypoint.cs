using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Core
{
    public class GraphWaypoint : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private EGraphNodeType _type;
        [SerializeField] private float _multiplier;
        [SerializeField] private List<NeighborConnection> _neighbors;
        
        public int Id => _id;
        public EGraphNodeType Type => _type;
        public float Multiplier => _multiplier;
        public List<NeighborConnection> Neighbors => _neighbors; 
        public Vector3 Position => transform.position;
        
        private void OnDrawGizmos()
        {
            if (_neighbors == null)
                return;

            Gizmos.color = Color.green;

            var from = transform.position;
            foreach (var neighborConnection in _neighbors)
            {
                if (neighborConnection?.Neighbor == null) 
                    continue;
                
                var to = neighborConnection.Neighbor.transform.position;
                Gizmos.DrawLine(from, to);
            }
        }
    }
}