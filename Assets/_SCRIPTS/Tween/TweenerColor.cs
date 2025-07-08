using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TDS.Tween
{
    public class TweenerColor : Tweener
    {
        [System.Serializable]
        protected class TweenTarget
        {
            [Header("TWEENING")]
            public GameObject TargetGO;
            public float Delay;
            public Color Target;

            public UnityEvent OnComplete;
        }
        [SerializeField] private TweenTarget[] tweenTargets;

        public override void Execute()
        {
            foreach (TweenTarget tweenTarget in tweenTargets)
            {
                Tweening(tweenTarget);
            }
        }

        //Actual tweening of the color
        private void Tweening(TweenTarget tweenTarget)
        {
            RectTransform rect = (RectTransform)tweenTarget.TargetGO.transform;
            LTDescr tweenDesc = LeanTween.color(rect, tweenTarget.Target, tweenTime);

            tweenDesc.setDelay(tweenTarget.Delay);
            tweenDesc.setOnComplete(tweenTarget.OnComplete.Invoke);
        }

        //Instantly assign target values
        public override void ApplyTarget()
        {
            base.ApplyTarget();

            foreach (TweenTarget tweenTarget in tweenTargets)
            {
                if(tweenTarget.TargetGO == null) { continue; }
                if(tweenTarget.TargetGO.TryGetComponent(out Image image))
                {
                    image.color = tweenTarget.Target;
                }
            }
        }
    }
}