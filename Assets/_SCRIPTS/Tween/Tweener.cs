using UnityEngine;

namespace TDS.Tween
{
    public abstract class Tweener : MonoBehaviour
    {
        [SerializeField] protected float tweenTime;

        //The function called when executing the tweening
        public abstract void Execute();

        //The function called from editor when we press the Apply Target
        //This can be called in runtime too if we want the resulting values to be applied instantly
        public virtual void ApplyTarget() { }
    }
}