using Attacks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentAmmoText;
        [SerializeField] private TextMeshProUGUI currentMaxAmmoText;

        private void Awake()
        {
            Ammo.OnAmmoChanged += UpdateCurrentAmmo;
            Ammo.OnAmmoChanged += UpdateCurrentMaxAmmo;
        }

        private void UpdateCurrentMaxAmmo(Ammo ammo)
        {
            currentMaxAmmoText.text = ammo.CurrentMaxAmmo.ToString();
        }

        private void UpdateCurrentAmmo(Ammo ammo)
        {
            currentAmmoText.text = ammo.CurrentAmmo.ToString();
        }

        private void OnDisable()
        {
            Ammo.OnAmmoChanged -= UpdateCurrentAmmo;
            Ammo.OnAmmoChanged -= UpdateCurrentMaxAmmo;
        }
    }
}