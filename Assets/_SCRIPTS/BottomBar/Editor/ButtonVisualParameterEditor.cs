using UnityEngine;
using UnityEditor;

namespace TDS.UI.Visual.General
{
    [CustomEditor(typeof(ButtonVisualParameter))]
    public class ButtonVisualParameterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(7);

            if(GUILayout.Button("Set Parent as Transform"))
            {
                ButtonVisualParameter bvp = (ButtonVisualParameter)target;
                bvp.SetParentAsTransform();
            }

            if(GUILayout.Button("Set Default Dimensions"))
            {
                ButtonVisualParameter bvp = (ButtonVisualParameter)target;
                bvp.SetDefaultDimensions();
            }
        }
    }
}