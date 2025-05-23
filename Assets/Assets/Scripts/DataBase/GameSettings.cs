﻿using Core;
using Enums;
using UnityEngine;

namespace DataBase
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Settings/" + nameof(GameSettings))]
    public class GameSettings : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField] private GraphNodeView _baseNodePrefab;
        [SerializeField] private GraphNodeView _mineStationNodePrefab;
        [SerializeField] private GraphNodeView _emptyNodePrefab;
        [SerializeField] private GameObject _edgePrefab;
        [SerializeField] private TrainView _trainPrefab;

        public GameObject EdgePrefab => _edgePrefab;
        public TrainView TrainPrefab => _trainPrefab;

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