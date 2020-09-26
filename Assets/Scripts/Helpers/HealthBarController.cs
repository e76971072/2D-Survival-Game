using System.Collections.Generic;
using Props;
using UnityEngine;

namespace Helpers
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;

        private readonly Dictionary<Health, HealthBar> _healthBars = new Dictionary<Health, HealthBar>();

        private void Awake()
        {
            _healthBars.Clear();
            Health.OnHealthAdded += AddHealthBar;
            Health.OnHealthRemoved += RemoveHealthBar;
        }

        private void AddHealthBar(Health health)
        {
            if (_healthBars.ContainsKey(health)) return;

            var newHealthBar = Instantiate(healthBar, gameObject.transform);
            _healthBars.Add(health, newHealthBar);
            newHealthBar.SetHealth(health);
        }

        private void RemoveHealthBar(Health health)
        {
            if (!_healthBars.ContainsKey(health)) return;

            if (_healthBars[health] != null)
            {
                Destroy(_healthBars[health].gameObject);
            }
            _healthBars.Remove(health);
            if (_healthBars.Count != 0) return;

            Health.OnHealthAdded -= AddHealthBar;
            Health.OnHealthRemoved -= RemoveHealthBar;
        }
    }
}