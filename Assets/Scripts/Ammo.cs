using System;
using UnityEngine;

[Serializable]
public class Ammo
{
    public static event Action<Ammo> OnAmmoChanged = delegate { };
    
    [SerializeField] private int maxAmmoPerClip = 10;
    [SerializeField] private int maxAmmo = 30;

    public Ammo()
    {
        GetCurrentAmmo = maxAmmoPerClip;
        GetCurrentMaxAmmo = maxAmmo;
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

    public int GetCurrentAmmo { get; private set; }

    public int GetCurrentMaxAmmo { get; private set; }

    public bool IsReloading { get; private set; }
}