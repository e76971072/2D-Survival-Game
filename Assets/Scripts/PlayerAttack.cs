using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

    public float timeBetweenAttack;
    public int damage;

    Animator attackAnim;
    bool enemyInRange;
    bool hasAttacked;
    float timer = 0.7f;

    void Awake()
    {
        attackAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= timeBetweenAttack)
            Attack();
    }

    void Attack()
    {
        timer = 0f;
        attackAnim.SetTrigger("CanAttack");
    }
}

