using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = nameof(GraphViewSettings), menuName = "Settings/" + nameof(GraphViewSettings))]
    public class GraphViewSettings : ScriptableObject
    {
        [SerializeField] private GameObject _baseNodePrefab;
        [SerializeField] private GameObject _mineStationNodePrefab;
        [SerializeField] private GameObject _edgePrefab;
        
        public GameObject BaseNodePrefab => _baseNodePrefab;
        public GameObject MineStationNodePrefab => _mineStationNodePrefab;
        public GameObject EdgePrefab => _edgePrefab;
    }
}