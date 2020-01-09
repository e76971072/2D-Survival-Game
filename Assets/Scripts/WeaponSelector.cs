using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    private List<GameObject> weaponsList = new List<GameObject>();
    private int targetWeapon;
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            weaponsList.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if (!int.TryParse(Input.inputString, out targetWeapon)) return;
        
        if (!(targetWeapon <= weaponsList.Count)) return;

        for (int i = 0; i < weaponsList.Count; i++)
        {
            if (i == targetWeapon - 1)
            {
                weaponsList[i].SetActive(true);
                continue;
            }
            weaponsList[i].SetActive(false);
        }
    }
}