using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

    public float timeBetweenAttack;
    public int damage;

    Animator attackAnimator;
    bool canDamage = true;
    bool enemyInRange;
    bool hasAttacked;
    float timer = 0f;

    void Awake()
    {
        attackAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= timeBetweenAttack)
        {
            if (attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !canDamage)
            {
                canDamage = true;
            }
            Attack();
        }
    }

    void Attack()
    {
        timer = 0f;
        attackAnimator.SetTrigger("Attack");
        
    }
    void OnTriggerStay2D(Collider2D collision2D)
    {
        if (attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack") && collision2D.tag == "Enemy" && canDamage == true)
        {
            collision2D.GetComponent<EnemyHealth>().TakeDamage(damage);
            canDamage = false;
        }
    }
}

