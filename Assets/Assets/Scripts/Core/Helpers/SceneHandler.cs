using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] private List<GraphWaypoint> _waypoints;

        public List<GraphWaypoint> Waypoints => _waypoints;
    }
}