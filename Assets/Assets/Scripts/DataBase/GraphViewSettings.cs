using Assets.Scripts.Core.Graph.Views;
using Enums;
using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = nameof(GraphViewSettings), menuName = "Settings/" + nameof(GraphViewSettings))]
    public class GraphViewSettings : ScriptableObject
    {
        [SerializeField] private GraphNodeView _baseNodePrefab;
        [SerializeField] private GraphNodeView _mineStationNodePrefab;
        [SerializeField] private GraphNodeView _emptyNodePrefab;
        [SerializeField] private GameObject _edgePrefab;

        public GameObject EdgePrefab => _edgePrefab;

        public GraphNodeView GetNodePrefabByType(EGraphNodeType nodeType)
        {
            return nodeType switch
            {
                EGraphNodeType.Base => _baseNodePrefab,
                EGraphNodeType.MineStation => _mineStationNodePrefab,
                EGraphNodeType.Empty => _emptyNodePrefab,
                _ => throw new System.ArgumentOutOfRangeException(nameof(nodeType), nodeType, null)
            };
        }
    }
}