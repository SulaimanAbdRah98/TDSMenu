using UnityEngine;

namespace TDS.UI.Visual.General
{
    //This script we will be defining values for customisation
    //This is so that we can have flexibility in cases where some buttons are bigger
    //You can learn more about my thought process on BottomBarView.cs
    public class ButtonVisualParameter : MonoBehaviour
    {
        //All variables are public because BottomBarView is accessing it
        [Tooltip("The RectTransform we want to animate")]
        public RectTransform Transform;
        [Tooltip("The width that we will revert to")]
        public float DefaultWidth;
        [Tooltip("The height that we will revert to")]
        public float DefaultHeight;

        [Tooltip("Is this button locked?")]
        public bool Locked;

        [HideInInspector] public float ExpandedWidth;
        [HideInInspector] public Vector2 OriginalPosition;

#if UNITY_EDITOR
        //Here are some helper functions I wrote called from ButtonVisualParameters.cs
        //I like writing helper functions like these even if it's minor because it saves time
        
        //This function is to automatically assign the parent as our target transform
        //This is how I designed the UI for this test
        public void SetParentAsTransform()
        {
            Transform = (RectTransform)transform.parent.transform;
        }

        //A helper function to automatically assign the default value
        //to whatever the current transform value of Transform is
        public void SetDefaultDimensions()
        {
            if(Transform == null)
            {
                SetParentAsTransform();
            }

            DefaultWidth = Transform.rect.width;
            DefaultHeight = Transform.rect.height;
        }
#endif
    }
}