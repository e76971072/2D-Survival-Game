using UnityEngine;

namespace Helpers
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorHandler : MonoBehaviour
    {
        protected Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}