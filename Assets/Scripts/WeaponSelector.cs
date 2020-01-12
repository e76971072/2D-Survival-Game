using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    private List<GameObject> weaponsList = new List<GameObject>();
    private Dictionary<int, GameObject> weaponDictionary = new Dictionary<int, GameObject>();
    private int targetWeapon;
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
        if (!int.TryParse(Input.inputString, out targetWeapon)) return;

        if (!weaponDictionary.ContainsKey(targetWeapon)) return;
        currentWeapon.SetActive(false);
        currentWeapon = weaponDictionary[targetWeapon];
        currentWeapon.SetActive(true);
    }
}