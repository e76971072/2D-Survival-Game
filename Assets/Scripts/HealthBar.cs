using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeedInSec = 0.5f;
    [SerializeField] private float positionOffset = 1f;

    #endregion

    #region NonSerializeFields

    private Health health;

    #endregion

    public void SetHealth(Health healthToSet)
    {
        health = healthToSet;
        healthToSet.OnHealthPctChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangedPct = foregroundImage.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < updateSpeedInSec)
        {
            elapsedTime += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsedTime / updateSpeedInSec);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate()
    {
        var worldToScreenPoint = GameManager.mainCamera
            .WorldToScreenPoint(health.transform.position + positionOffset * Vector3.up);
        transform.position = worldToScreenPoint;
    }

    private void OnDestroy()
    {
        health.OnHealthPctChanged -= HandleHealthChanged;
    }
}