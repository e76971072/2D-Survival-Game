using UnityEngine;
public class MeleeWeapon : MeleeAttack
{
    protected override void Awake()
    {
        GameManager.OnGameLost += DisableOnDead;
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        
        if (!CanDamage()) return;
        
        PlayAttackAnimation();
        Attack();
    }
    
    private void DisableOnDead()
    {
        GetComponent<MeleeWeapon>().enabled = false;
        GetComponent<Animator>().enabled = false;
        Debug.Log("Weapon Disabled");
    }
}