using Interfaces;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileLogic : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private GameObject explosionPrefab;

        private int _owner;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == _owner) return;
            if (other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().TakeDamage(damage);
            }

            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 5f);
            Destroy(gameObject);
        }

        public void AddForce(Vector2 moveForce)
        {
            _rigidbody2D.AddForce(moveForce);
        }

        public void SetOwner(int ownerLayerMask)
        {
            _owner = ownerLayerMask;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector2.one);
        }
    }
}