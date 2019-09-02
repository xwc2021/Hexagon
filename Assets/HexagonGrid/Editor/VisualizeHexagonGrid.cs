using UnityEngine;
using UnityEditor;

namespace Mytool
{
    [CustomEditor(typeof(VisualizeHexagonGrid))]
    public class VisualizeHexagonGridEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var cs = target as VisualizeHexagonGrid;
            if (GUILayout.Button("Generate Cube Set"))
            {
                cs.generateCubeSet();
                SceneView.RepaintAll();
            }

            if (GUILayout.Button("set camera transform"))
            {
                cs.setCameraTransform();
                SceneView.RepaintAll();
            }
        }
        private void OnSceneViewGUI(SceneView sv)
        {
           
        }

        void OnEnable()
        {
            //Debug.Log("OnEnable");
            SceneView.duringSceneGui += OnSceneViewGUI;
        }

        void OnDisable()
        {
            //Debug.Log("OnDisable");
            SceneView.duringSceneGui -= OnSceneViewGUI;
        }
    }
}