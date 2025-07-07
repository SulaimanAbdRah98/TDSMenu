using UnityEngine;
using UnityEngine.Events;
using TDS.Tween;

namespace TDS.UI.Visual.Popup
{
    public class PopupMenu : MonoBehaviour
    {
        [Header("TWEENING")]
        [SerializeField] private Tweener[] openTweens;
        [SerializeField] private Tweener[] closeTweens;

        //Include events in case we want to extend this behaviour
        [Header("EVENTS")]
        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        public void Open()
        {
            onOpen.Invoke();

            foreach(Tweener t in openTweens)
            {
                if (t != null)
                {
                    t.Execute();
                }
            }
        }

        public void Close()
        {
            onClose.Invoke();

            foreach(Tweener t in closeTweens)
            {
                if (t != null)
                {
                    t.Execute();
                }
            }
        }
    }
}