using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Props
{
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

        private Health _health;

        private void Awake()
        {
            FadeHealthBar(0, 0);
        }

        public void SetHealth(Health healthToSet)
        {
            _health = healthToSet;
            healthToSet.OnHealthPctChanged += HandleHealthChanged;
        }

        private void HandleHealthChanged(float pct)
        {
            FadeHealthBar(1, fadeSeconds);

            StopAllCoroutines();
            StartCoroutine(ChangeToPct(pct));
        }

        private void FadeHealthBar(float alphaValue, float seconds)
        {
            foregroundImage.CrossFadeAlpha(alphaValue, seconds, false);
            backgroundImage.CrossFadeAlpha(alphaValue, seconds, false);
        }

        private IEnumerator ChangeToPct(float pct)
        {
            var preChangedPct = foregroundImage.fillAmount;
            var elapsedTime = 0f;

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
                .WorldToScreenPoint(_health.transform.position + positionOffset * Vector3.up);
            transform.position = worldToScreenPoint;
        }

        private void OnDestroy()
        {
            _health.OnHealthPctChanged -= HandleHealthChanged;
        }
    }
}