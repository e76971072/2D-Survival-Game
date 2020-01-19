using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickups : MonoBehaviour
{
    protected abstract void OnTriggerEnter2D(Collider2D other);

    protected abstract void OnPickedUp(Collider2D player);
}