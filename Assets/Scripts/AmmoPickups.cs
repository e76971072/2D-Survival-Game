using UnityEngine;

public class AmmoPickups : Pickups
{
    [SerializeField] private int ammoAmount = 10;

    protected override void OnPickedUp(Collider2D player)
    {
        for (int i = 0; i < player.transform.GetChild(0).childCount; i++)
        {
            Transform weapon = player.transform.GetChild(0).GetChild(i);
            if (!weapon.GetComponent<WeaponRangeHitScan>()) continue;
            
            weapon.GetComponent<WeaponRangeHitScan>().ammo.RefillAmmo(ammoAmount);
        }
        Destroy(gameObject);
    }
}