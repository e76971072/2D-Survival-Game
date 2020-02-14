using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class WeaponSelector : MonoBehaviour
    {
        private readonly Dictionary<int, GameObject> weaponDictionary = new Dictionary<int, GameObject>();
        private int currentWeaponIndex;

        private void Awake()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var weapon = transform.GetChild(i).gameObject;
                weaponDictionary.Add(i + 1, weapon);

                if (!weapon.activeSelf) continue;
                currentWeaponIndex = i + 1;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchWeapon();
            }
        }

        public void SwitchWeapon()
        {
            var targetWeaponIndex = GetTargetWeaponIndex();
            weaponDictionary[currentWeaponIndex].SetActive(false);
            weaponDictionary[targetWeaponIndex].SetActive(true);
            currentWeaponIndex = targetWeaponIndex;
        }

        private int GetTargetWeaponIndex()
        {
            return currentWeaponIndex == 1 ? 2 : 1;
        }
    }
}