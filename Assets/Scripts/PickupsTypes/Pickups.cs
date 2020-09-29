using UnityEngine;

namespace PickupsTypes
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Pickups : MonoBehaviour
    {
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            OnPickedUp();
        }

        protected abstract void OnPickedUp();
    }
}