using UnityEngine;

public class HealthPickups : Pickups
{
    [SerializeField] private int healAmount = 10;

    protected override void OnPickedUp(Collider2D player)
    {
        player.GetComponent<IHealth>().ModifyHealth(healAmount);
        Destroy(gameObject);
    }
}