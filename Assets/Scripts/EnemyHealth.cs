using UnityEngine;

public class EnemyHealth : Health
{
    #region SerializeFields

    [SerializeField] private float deadWaitTime;

    #endregion

    #region NonSerializeFields

    private Collider2D enemyCollider;

    #endregion

    private void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
    }

    protected override void Die()
    {
        RemoveHealthBar();
        enemyCollider.enabled = false;
        Destroy(transform.parent.gameObject, deadWaitTime);
    }
}