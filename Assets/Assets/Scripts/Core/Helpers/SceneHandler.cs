using System.Collections.Generic;
using Assets.Scripts.Core.Graph;
using UnityEngine;

namespace Assets.Scripts.Core.Helpers
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private List<GraphWaypoint> _waypoints;

        public List<GraphWaypoint> Waypoints => _waypoints;
    }
}