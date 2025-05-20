using System.Collections.Generic;
using Core;
using Enums;
using UnityEngine;

namespace Assets.Scripts.Core.Graph
{
    public class GraphWaypoint : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private EGraphNodeType _type;
        [SerializeField] private float _multiplier;
        [SerializeField] private List<NeighborConnection> _neighbors;
        
        public string Id => _id;
        public EGraphNodeType Type => _type;
        public float Multiplier => _multiplier;
        public List<NeighborConnection> Neighbors => _neighbors; 
        public Vector3 Position => transform.position;
    }
}