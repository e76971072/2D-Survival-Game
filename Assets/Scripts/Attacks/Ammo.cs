using System;
using PickupsTypes;
using UnityEngine;

namespace Attacks
{
    public class Ammo : MonoBehaviour
    {
        public static event Action<Ammo> OnAmmoChanged;

        [SerializeField] private int maxAmmo = 30;
        [SerializeField] private int maxAmmoPerClip = 10;

        [HideInInspector] public bool IsReloading;
        public int CurrentMaxAmmo { get; private set; }

        public int CurrentAmmo
        {
            get => currentAmmo;
            private set => currentAmmo = Mathf.Clamp(value, 0, maxAmmoPerClip);
        }

        private int currentAmmo;

        private void Awake()
        {
            AmmoPickups.OnAmmoPickedUp += RefillAmmo;
            currentAmmo = maxAmmoPerClip;
            CurrentMaxAmmo = maxAmmo;
        }

        private void Start()
        {
            UpdateAmmoUi();
        }

        private void UpdateAmmoUi()
        {
            OnAmmoChanged?.Invoke(this);
        }

        public void Reload()
        {
            ResetCurrentAmmo();
            ReduceCurrentMaxAmmo();
            IsReloading = false;
            OnAmmoChanged?.Invoke(this);
        }

        public void ReduceCurrentAmmo()
        {
            CurrentAmmo--;
            OnAmmoChanged?.Invoke(this);
        }

        private void ReduceCurrentMaxAmmo()
        {
            CurrentMaxAmmo -= CurrentMaxAmmo < maxAmmoPerClip ? CurrentMaxAmmo : maxAmmoPerClip;
            OnAmmoChanged?.Invoke(this);
        }

        private void ResetCurrentAmmo()
        {
            CurrentAmmo = CurrentMaxAmmo < maxAmmoPerClip ? CurrentMaxAmmo : maxAmmoPerClip;
            OnAmmoChanged?.Invoke(this);
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

        private void OnDestroy()
        {
            AmmoPickups.OnAmmoPickedUp -= RefillAmmo;
        }
    }
}