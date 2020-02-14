using System.Collections.Generic;
using Props;
using UnityEngine;

namespace Helpers
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;

        private readonly Dictionary<Health, HealthBar> healthBars = new Dictionary<Health, HealthBar>();

        private void Awake()
        {
            healthBars.Clear();
            Health.OnHealthAdded += AddHealthBar;
            Health.OnHealthRemoved += RemoveHealthBar;
        }

        private void AddHealthBar(Health health)
        {
            if (healthBars.ContainsKey(health)) return;

            var newHealthBar = Instantiate(healthBar, gameObject.transform);
            healthBars.Add(health, newHealthBar);
            newHealthBar.SetHealth(health);
        }

        private void RemoveHealthBar(Health health)
        {
            if (!healthBars.ContainsKey(health)) return;

            if (healthBars[health] != null)
            {
                Destroy(healthBars[health].gameObject);
            }
            healthBars.Remove(health);
            if (healthBars.Count != 0) return;

            Health.OnHealthAdded -= AddHealthBar;
            Health.OnHealthRemoved -= RemoveHealthBar;
        }
    }
}