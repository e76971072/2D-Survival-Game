using UnityEngine;

namespace Helpers
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorHandler : MonoBehaviour
    {
        protected Animator Animator;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }
    }
}