using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float deadWaitTime;
    public int startingHealth;

    Collider2D enemyCollider;
    int currentHealth;
    bool isDead;

    // Use this for initialization
    void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        enemyCollider.enabled = false;
        Destroy(gameObject, deadWaitTime);
    }
}
