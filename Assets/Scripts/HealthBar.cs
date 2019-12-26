using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private Image foregroundImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float updateSpeedInSec = 0.5f;
    [SerializeField] private float positionOffset = 1f;
    [SerializeField] private float fadeSeconds = 0.1f;
    [SerializeField] private float shownSeconds = 2f;

    #endregion

    #region NonSerializeFields

    private Health health;

    #endregion

    private void Awake()
    {
        FadeHealthBar(0, 0);
    }

    public void SetHealth(Health healthToSet)
    {
        health = healthToSet;
        healthToSet.OnHealthPctChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        FadeHealthBar(1, fadeSeconds);
        
        StopAllCoroutines();
        StartCoroutine(ChangeToPct(pct));
        StartCoroutine(WaitToFade());
    }

    private IEnumerator WaitToFade()
    {
        yield return new WaitForSeconds(shownSeconds);
        FadeHealthBar(0, fadeSeconds);
    }

    private void FadeHealthBar(float alphaValue, float seconds)
    {
        foregroundImage.CrossFadeAlpha(alphaValue, seconds, false);
        backgroundImage.CrossFadeAlpha(alphaValue, seconds, false);
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
        var worldToScreenPoint = GameManager.Instance.mainCamera
            .WorldToScreenPoint(health.transform.position + positionOffset * Vector3.up);
        transform.position = worldToScreenPoint;
    }

    private void OnDestroy()
    {
        health.OnHealthPctChanged -= HandleHealthChanged;
    }
}