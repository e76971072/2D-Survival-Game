using Attacks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentAmmoText;
        [SerializeField] private TextMeshProUGUI currentMaxAmmoText;

        private void OnEnable()
        {
            Ammo.OnAmmoChanged += UpdateCurrentAmmo;
            Ammo.OnAmmoChanged += UpdateCurrentMaxAmmo;
        }

        private void UpdateCurrentMaxAmmo(Ammo ammo)
        {
            currentMaxAmmoText.text = ammo.GetCurrentMaxAmmo.ToString();
        }

        private void UpdateCurrentAmmo(Ammo ammo)
        {
            currentAmmoText.text = ammo.GetCurrentAmmo.ToString();
        }

        private void OnDisable()
        {
            Ammo.OnAmmoChanged -= UpdateCurrentAmmo;
            Ammo.OnAmmoChanged -= UpdateCurrentMaxAmmo;
        }
    }
}