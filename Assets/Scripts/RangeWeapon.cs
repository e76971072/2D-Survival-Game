using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private int damage;
    [SerializeField] private float firingRate;
    [SerializeField] private float shootingRange;
    [SerializeField] private GameObject gunHitEffect;

    #endregion

    #region NonSerializeFields

    private float nextTimeToFire;
    private PlayerMovement playerMovement;

    #endregion

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (!Input.GetButton("Fire1") || !(Time.time >= nextTimeToFire)) return;

        nextTimeToFire = Time.time + 1f / firingRate;
        Shoot();
    }

    private void Shoot()
    {
        var gunTransform = transform;
        var playerPosition = gunTransform.parent.position;
        var hitInfo =
            Physics2D.Raycast(playerPosition, gunTransform.right, shootingRange);
        if (!hitInfo) return;
        
        var hitParticle = Instantiate(gunHitEffect, hitInfo.point,
            Quaternion.FromToRotation(Vector2.up, hitInfo.normal));
        Destroy(hitParticle, 0.5f);
        if (hitInfo.transform.CompareTag("Enemy"))
        {
            hitInfo.transform.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}