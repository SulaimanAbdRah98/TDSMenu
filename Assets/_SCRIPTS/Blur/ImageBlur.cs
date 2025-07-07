using UnityEngine;

namespace TDS.UI.Visual.Blur
{
    public class ImageBlur : MonoBehaviour
    {
        [SerializeField] private Canvas referenceCanvas;

        public void ToggleBlur(bool toggle)
        {
            foreach(Canvas canvas in CanvasManager.Instance.Canvases)
            {
                //No need to change our own canvas, skip it
                if(canvas == referenceCanvas) { continue; }

                //Any canvas under should be set to ScreenspaceCamera or World Space
                //This is necessary to make them be affected by the blurring
                if (toggle && canvas.sortingOrder < referenceCanvas.sortingOrder)
                {
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = CanvasManager.Instance.DefaultCamera;
                }
                else
                {
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                }
            }
        }
    }
}