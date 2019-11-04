using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private HealthBar healthBar;

    #endregion

    #region NonSerializeFields

    private Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

    #endregion

    private void Awake()
    {
        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
    }

    private void AddHealthBar(Health health)
    {
        if (healthBars.ContainsKey(health)) return;

        var newHealthBar = Instantiate(healthBar, transform);
        healthBars.Add(health, newHealthBar);
        newHealthBar.SetHealth(health);
    }

    private void RemoveHealthBar(Health health)
    {
        if (!healthBars.ContainsKey(health)) return;

        Destroy(healthBars[health].gameObject);
        healthBars.Remove(health);
    }
}