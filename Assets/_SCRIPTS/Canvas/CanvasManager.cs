using UnityEngine;

namespace TDS.UI
{
    //A script made to keep track of all canvas
    //Originally this was meant specifically to help with how I'm setting up the UI blur system
    //But I realise that having a list of canvas somewhere can be generally applied elsewhere
    //So I made a manager script for it in case we need this functionality elsewhere

    //You can check out ImageBlur.cs for more info on how I'm using it!
    public class CanvasManager : MonoBehaviour
    {
        [Tooltip("All UI canvas that we have")]
        public Canvas[] Canvases;

        [Tooltip("Camera to use if we are switching to Screenspace - Camera Render Mode")]
        public Camera DefaultCamera;

        //We could use this to revert render mode if needed
        //but we would need to account for cameras if the original render mode is Screenspace-Camera
        //Commenting out because it's not really applicable here
        //[Tooltip("More specific to our blur system, there must be a better way of doing this")]
        //public RenderMode[] OriginalRenderModes;

        //Make it accessible anywhere
        public static CanvasManager Instance;

        private void Awake()
        {
            if(Instance != null) { Destroy(Instance); }
            Instance = this;
        }
    }
}