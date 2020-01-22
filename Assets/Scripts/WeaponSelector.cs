using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    private Dictionary<int, GameObject> weaponDictionary = new Dictionary<int, GameObject>();
    private GameObject currentWeapon;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject weapon = transform.GetChild(i).gameObject;
            weaponDictionary.Add(i + 1, weapon);

            if (!weapon.activeSelf) continue;
            currentWeapon = weapon;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
        }
    }

    private void SwitchWeapon(int weaponIndex)
    {
        currentWeapon.SetActive(false);
        weaponDictionary[weaponIndex].SetActive(true);
        currentWeapon = weaponDictionary[weaponIndex];
    }
}