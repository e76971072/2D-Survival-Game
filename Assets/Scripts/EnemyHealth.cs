using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    #region SerializeFields

    [SerializeField] private float deadWaitTime;
    [SerializeField] private int startingHealth;

    #endregion

    #region NonSerializeFields

    private Collider2D enemyCollider;
    private int currentHealth;

    #endregion

    private void Awake()
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

    private void Death()
    {
        enemyCollider.enabled = false;
        Destroy(transform.parent.gameObject, deadWaitTime);
    }
}