﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytool
{
    public class HexagonGridCamera : MonoBehaviour
    {
        public VisualizeHexagonGrid visualizeHexagonGrid;
        Camera using_camera;
        private void Awake()
        {
            using_camera = GetComponent<Camera>();
        }
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = using_camera.ScreenPointToRay(Input.mousePosition);
                visualizeHexagonGrid.hitBox(ray);
            }
        }
    }
}

