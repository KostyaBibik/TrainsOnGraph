﻿using UnityEngine;

namespace Core
{
    public class GraphNodeView : MonoBehaviour
    {
        private GraphNodeModel _model;

        public void Initialize(GraphNodeModel model)
        {
            _model = model;
            name = $"Node_{model.Type}[{model.Id}]";
            transform.position = model.Position;
        }
    }
}