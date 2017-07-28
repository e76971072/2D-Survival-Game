using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float deadWaitTime;
    public int startingHealth;

    Collider2D enemyCollider;
    int currentHealth;
    //bool isDead;

    // Use this for initialization
    void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        enemyCollider.enabled = false;
        Destroy(gameObject, deadWaitTime);
    }
}
