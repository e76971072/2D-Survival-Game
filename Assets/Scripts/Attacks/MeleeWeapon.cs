using Helpers;
using Player;
using Signals;
using UnityEngine;
using Zenject;

namespace Attacks
{
    [RequireComponent(typeof(MeleeWeaponAnimatorHandler))]
    public class MeleeWeapon : MeleeAttack
    {
        [SerializeField] private PlayerInput playerInput;
        
        private MeleeWeaponAnimatorHandler _animatorHandler;
        [Inject] private readonly SignalBus _signalBus;
        
        protected override void Awake()
        {
            _animatorHandler = GetComponent<MeleeWeaponAnimatorHandler>();
            _signalBus.Subscribe<GameLostSignal>(DisableOnDead);
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (CantDamage()) return;

            _animatorHandler.PlayAttackAnimation();
            Attack();
        }

        protected virtual bool CantDamage()
        {
            return !playerInput.canShoot || !(Timer >= timeBetweenAttack);
        }

        private void DisableOnDead()
        {
            GetComponent<MeleeWeapon>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }

        private void OnDisable()
        {
            Timer = timeBetweenAttack;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<GameLostSignal>(DisableOnDead);
        }
    }
}