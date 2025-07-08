using UnityEngine;
using UnityEngine.Events;

namespace TDS.Tween
{
    public class TweenerTransform : Tweener
    {
        protected enum TweenType
        {
            MOVE, SCALE, SIZE
        }

        [System.Serializable]
        protected class TweenTarget
        {
            [Header("TWEEN")]
            public GameObject TargetGO;
            public float Delay;
            public TweenType Type;
            public Vector2 Target;

            [Header("BOUNCE")]
            public bool Bounce;
            public LeanTweenType BounceType  = LeanTweenType.easeInOutElastic;

            public UnityEvent OnComplete;
        }
        [SerializeField] private TweenTarget[] tweenTargets;

        public override void Execute()
        {
            foreach(TweenTarget tweenTarget in tweenTargets)
            {
                Tweening(tweenTarget);
            }
        }

        //Tweening function
        private void Tweening(TweenTarget tweenTarget)
        {
            LTDescr tweenDesc = null;

            //Depending on the type of transform tween this is, we will be doing different things
            switch (tweenTarget.Type)
            {
                case TweenType.MOVE:
                    RectTransform rect = (RectTransform)tweenTarget.TargetGO.transform;
                    tweenDesc = LeanTween.moveLocal(tweenTarget.TargetGO, tweenTarget.Target, tweenTime);
                    break;
                case TweenType.SCALE:
                    tweenDesc = LeanTween.scale(tweenTarget.TargetGO, tweenTarget.Target, tweenTime);
                    break;
                case TweenType.SIZE:
                    tweenDesc = LeanTween.size((RectTransform)tweenTarget.TargetGO.transform, tweenTarget.Target, tweenTime);
                    break;
            }

            //An option to add a bit of a bounce to add a bit of ease and satisfying feeling
            if (tweenTarget.Bounce)
            {
                tweenDesc.setDelay(0.5f).setEase(tweenTarget.BounceType);
            }

            tweenDesc.setDelay(tweenTarget.Delay);
            tweenDesc.setOnComplete(tweenTarget.OnComplete.Invoke);
        }

        //Instantly assign target values
        public override void ApplyTarget()
        {
            base.ApplyTarget();

            foreach (TweenTarget tweenTarget in tweenTargets)
            {
                if (tweenTarget.TargetGO == null) { continue; }

                RectTransform rect = (RectTransform)tweenTarget.TargetGO.transform;

                switch (tweenTarget.Type)
                {
                    case TweenType.MOVE:
                        rect.localPosition = tweenTarget.Target;
                        break;
                    case TweenType.SCALE:
                        rect.localScale = tweenTarget.Target;
                        break;
                    case TweenType.SIZE:
                        rect.sizeDelta = tweenTarget.Target;
                        break;
                }
            }
        }
    }
}