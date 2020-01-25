using Interfaces;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileLogic : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private GameObject explosionPrefab;

        private int owner;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == owner) return;
            if (other.GetComponent<IHealth>() != null) other.GetComponent<IHealth>().ModifyHealth(-damage);

            Destroy(gameObject);
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 5f);
        }

        public void SetOwner(int ownerLayerMask)
        {
            owner = ownerLayerMask;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector2.one);
        }
    }
}