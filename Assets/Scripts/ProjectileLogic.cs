using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileLogic : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int damage;

    private int owner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == owner) return;
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 5f);
    }

    public void SetOwner(int ownerLayerMask)
    {
        owner = ownerLayerMask;
    }
}