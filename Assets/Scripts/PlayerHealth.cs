using UnityEngine;

public class PlayerHealth : Health
{
    private void Awake()
    {
        GameManager.OnGameLost += DisableOnDead;
    }

    protected override void Die()
    {
        base.Die();
        GameManager.Instance.GameLost();
    }

    private void DisableOnDead()
    {
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Collider Disabled");
    }
}