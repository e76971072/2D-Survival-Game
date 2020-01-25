using System;
using PickupsTypes;
using UnityEngine;

namespace Attacks
{
    [Serializable]
    public class Ammo
    {
        public static event Action<Ammo> OnAmmoChanged = delegate { };
        public int GetCurrentAmmo { get; private set; }
        public bool IsReloading { get; private set; }
        public int GetCurrentMaxAmmo { get; private set; }

        [SerializeField] private int maxAmmo = 30;
        [SerializeField] private int maxAmmoPerClip = 10;

        public Ammo()
        {
            GetCurrentAmmo = maxAmmoPerClip;
            GetCurrentMaxAmmo = maxAmmo;

            AmmoPickups.OnAmmoPickedUp += RefillAmmo;
        }


        public void UpdateAmmoUi()
        {
            OnAmmoChanged(this);
        }

        public void Reload()
        {
            ResetCurrentAmmo();
            ReduceCurrentMaxAmmo();
            StopReloading();
            OnAmmoChanged(this);
        }

        public void ReduceCurrentAmmo()
        {
            GetCurrentAmmo--;
            OnAmmoChanged(this);
        }

        private void ReduceCurrentMaxAmmo()
        {
            GetCurrentMaxAmmo -= GetCurrentMaxAmmo < maxAmmoPerClip ? GetCurrentMaxAmmo : maxAmmoPerClip;
            OnAmmoChanged(this);
        }

        private void ResetCurrentAmmo()
        {
            GetCurrentAmmo = GetCurrentMaxAmmo < maxAmmoPerClip ? GetCurrentMaxAmmo : maxAmmoPerClip;
            OnAmmoChanged(this);
        }

        public bool IsAmmoEmpty()
        {
            return GetCurrentMaxAmmo <= 0 && GetCurrentAmmo <= 0;
        }

        public void StartReloading()
        {
            IsReloading = true;
        }

        public void StopReloading()
        {
            IsReloading = false;
        }

        public void RefillAmmo(int refillAmount)
        {
            GetCurrentMaxAmmo += refillAmount;
            OnAmmoChanged(this);
        }
    }
}