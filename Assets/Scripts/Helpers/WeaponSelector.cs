using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class WeaponSelector : MonoBehaviour
    {
        private readonly Dictionary<int, GameObject> _weaponDictionary = new Dictionary<int, GameObject>();
        private int _currentWeaponIndex;

        private void Awake()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var weapon = transform.GetChild(i).gameObject;
                _weaponDictionary.Add(i + 1, weapon);

                if (!weapon.activeSelf) continue;
                _currentWeaponIndex = i + 1;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchWeapon();
            }
        }

        private void SwitchWeapon()
        {
            var targetWeaponIndex = GetTargetWeaponIndex();
            _weaponDictionary[_currentWeaponIndex].SetActive(false);
            _weaponDictionary[targetWeaponIndex].SetActive(true);
            _currentWeaponIndex = targetWeaponIndex;
        }

        private int GetTargetWeaponIndex()
        {
            return _currentWeaponIndex == 1 ? 2 : 1;
        }
    }
}