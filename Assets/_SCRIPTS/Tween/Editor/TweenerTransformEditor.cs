using UnityEngine;
using UnityEditor;

namespace TDS.Tween
{
    [CustomEditor(typeof(TweenerTransform))]
    public class TweenerTransformEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Apply Target Values"))
            {
                TweenerTransform tweener = (TweenerTransform)target;
                tweener.ApplyTarget();
            }
        }
    }
}