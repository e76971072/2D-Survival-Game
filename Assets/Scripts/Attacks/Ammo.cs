using System;
using Data;
using PickupsTypes;
using UnityEngine;

namespace Attacks
{
    public class Ammo
    {
        public static event Action<Ammo> OnAmmoChanged;

        public bool IsReloading;

        private readonly int maxAmmo;
        private readonly int maxAmmoPerClip;

        private int currentAmmo;
        private int currentMaxAmmo;

        public int CurrentMaxAmmo
        {
            get => currentMaxAmmo;
            private set => currentMaxAmmo = Mathf.Clamp(value, 0, maxAmmo);
        }

        public int CurrentAmmo
        {
            get => currentAmmo;
            private set => currentAmmo = Mathf.Clamp(value, 0, maxAmmoPerClip);
        }


        public Ammo(int maxAmmo, int maxAmmoPerClip)
        {
            AmmoPickups.OnAmmoPickedUp += RefillAmmo;

            this.maxAmmo = maxAmmo;
            this.maxAmmoPerClip = maxAmmoPerClip;
            CurrentMaxAmmo = maxAmmo;
            CurrentAmmo = maxAmmoPerClip;
            OnAmmoChanged?.Invoke(this);
        }

        public void Reload()
        {
            var reloadAmount = GetReloadAmount();
            ResetCurrentAmmo(reloadAmount);
            ReduceCurrentMaxAmmo(reloadAmount);
            IsReloading = false;
            OnAmmoChanged?.Invoke(this);
        }

        public void ReduceCurrentAmmo()
        {
            CurrentAmmo--;
            OnAmmoChanged?.Invoke(this);
        }
        
        public void ReduceCurrentAmmo(int reduceAmount)
        {
            CurrentAmmo -= reduceAmount;
            OnAmmoChanged?.Invoke(this);
        }

        private void ReduceCurrentMaxAmmo(int reduceAmount)
        {
            CurrentMaxAmmo -= reduceAmount;
        }

        private void ResetCurrentAmmo(int reloadAmount)
        {
            CurrentAmmo += reloadAmount;
        }

        public bool IsAmmoEmpty()
        {
            return CurrentMaxAmmo <= 0 && CurrentAmmo <= 0;
        }

        private void RefillAmmo(int refillAmount)
        {
            CurrentMaxAmmo += refillAmount;
            OnAmmoChanged?.Invoke(this);
        }

        private int GetReloadAmount()
        {
            return CurrentMaxAmmo <= (maxAmmoPerClip - CurrentAmmo) ? CurrentMaxAmmo : (maxAmmoPerClip - CurrentAmmo);
        }
    }
}