using UnityEngine;
using UnityEditor;

namespace TDS.UI.Visual.Settings
{
    [CustomEditor(typeof(SettingsDesigner))]
    public class SettingsDesignerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                SettingsDesigner settingsDesigner = (SettingsDesigner)target;
                settingsDesigner.Refresh();
            }
        }
    }
}