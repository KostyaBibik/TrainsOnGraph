using Assets.Scripts.Core.Graph;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class NeighborConnection
    {
        [SerializeField] private GraphWaypoint _neighbor;
        [SerializeField] private float _edgeLength; 
        
        public GraphWaypoint Neighbor => _neighbor;
        public float EdgeLength => _edgeLength;
    }
}