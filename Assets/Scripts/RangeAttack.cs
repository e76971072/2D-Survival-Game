using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour {

    public int damage;
    public float firingRate;
    public float shootingRange;
    public GameObject gunHitEffect;

    float nextTimeToFire = 0f;
    PlayerMovement playerMovement;
    //Transform gunMuzzle;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        //gunMuzzle = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firingRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(gameObject.transform.position, playerMovement.direction, shootingRange);
        GameObject hitParticle = Instantiate(gunHitEffect, hitInfo.point, Quaternion.FromToRotation(Vector2.up, -playerMovement.direction));
        Destroy(hitParticle, 0.5f);
        if (hitInfo.collider != null && hitInfo.transform.tag == "Enemy")
        {
            hitInfo.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.DrawLine(gameObject.transform.position, hitInfo.point, Color.red, 1f);
        }
    }
}
