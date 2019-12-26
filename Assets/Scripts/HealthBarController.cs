using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        healthBars.Clear();
        Health.OnHealthAdded += AddHealthBar;
        Health.OnHealthRemoved += RemoveHealthBar;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

        if (healthBars[health] != null) Destroy(healthBars[health].gameObject);
        healthBars.Remove(health);
        if (healthBars.Count != 0) return;

        Health.OnHealthAdded -= AddHealthBar;
        Health.OnHealthRemoved -= RemoveHealthBar;
    }
}