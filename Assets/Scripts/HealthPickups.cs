using UnityEngine;

public class HealthPickups : Pickups
{
    [SerializeField] private int healAmount = 10;
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        OnPickedUp(other);
    }

    protected override void OnPickedUp(Collider2D player)
    {
        player.GetComponent<IHealth>().ModifyHealth(healAmount);
        Destroy(gameObject);
    }
}