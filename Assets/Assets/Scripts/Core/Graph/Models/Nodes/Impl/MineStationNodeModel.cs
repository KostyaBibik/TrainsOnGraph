﻿using Enums;
using UnityEngine;

namespace Core
{
    public class MineStationNodeModel : GraphNodeModel
    {
        public float MiningTimeMultiplier { get; set; }

        public MineStationNodeModel(int id, Vector3 position, float miningTimeMultiplier)
            : base(id, EGraphNodeType.MineStation, position)
        {
            MiningTimeMultiplier = miningTimeMultiplier;
        }
    }
}