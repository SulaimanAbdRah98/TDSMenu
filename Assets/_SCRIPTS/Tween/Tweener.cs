using UnityEngine;

namespace TDS.Tween
{
    public abstract class Tweener : MonoBehaviour
    {
        [SerializeField] protected float tweenTime;

        public abstract void Execute();
        public virtual void ApplyTarget() { }
    }
}