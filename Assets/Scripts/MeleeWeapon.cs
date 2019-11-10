public class MeleeWeapon : MeleeAttack
{
    protected override void Update()
    {
        base.Update();
        
        if (!CanDamage()) return;
        
        PlayAttackAnimation();
        Attack();
    }
}