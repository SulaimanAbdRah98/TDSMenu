using UnityEngine;
using UnityEditor;

namespace TDS.Tween
{
    [CustomEditor(typeof(TweenerColor))]
    public class TweenerColorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Apply Target Values"))
            {
                TweenerColor tweener = (TweenerColor)target;
                tweener.ApplyTarget();
            }
        }
    }
}