using System;
using System.Collections;
using Data;
using Enemy;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreSystemUI : MonoBehaviour
    {
        #region SerializeFields
        
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI comboText;
        [SerializeField] private Image timerImage;
        [SerializeField] private float maxResetTime = 3;

        public int scoreIncrement;

        #endregion

        #region NonSerializeFields

        private float currentTimer;
        private Animator comboTextAnimator;
        private Color timerImageInitColor;

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int ShouldFlick = Animator.StringToHash("ShouldFlick");
        private static readonly int FlickSpeed = Animator.StringToHash("FlickSpeed");
        private static readonly int EndCombo = Animator.StringToHash("EndCombo");

        #endregion

        private void OnValidate()
        {
            scoreIncrement = Mathf.Clamp(scoreIncrement, 0, 100);
        }

        private void Awake()
        {
            comboTextAnimator = comboText.GetComponent<Animator>();
            timerImageInitColor = timerImage.color;

            Score.Instance.ResetScore();
            Score.Instance.ScoreIncrement = scoreIncrement;
            
            Score.OnScoreChanged += UpdateScoreText;
            HitCombo.OnHitComboChanged += UpdateComboText;

            EnemyHealth.OnEnemyHit += TimerCoroutineController;
        }

        private void UpdateComboText(int currentCombo)
        {
            comboText.text = $"x{currentCombo}";
            comboTextAnimator.SetTrigger(Hit);
        }

        private void UpdateScoreText(int currentScore)
        {
            scoreText.text = currentScore.ToString();
        }

        private void UpdateComboTimerText()
        {
            timerImage.fillAmount = currentTimer / maxResetTime;
        }

        private void TimerCoroutineController()
        {
            StopAllCoroutines();
            StartCoroutine(ComboTimer());
        }

        private IEnumerator ComboTimer()
        {
            currentTimer = maxResetTime;
            comboTextAnimator.SetBool(ShouldFlick, false);
            comboTextAnimator.SetFloat(FlickSpeed, 0);
            while (currentTimer > 0)
            {
                UpdateComboTimerText();
                timerImage.color = Color.Lerp(Color.red, timerImageInitColor, currentTimer / maxResetTime);
                currentTimer -= Time.deltaTime;
                if (currentTimer <= maxResetTime / 2)
                {
                    comboTextAnimator.SetBool(ShouldFlick, true);
                    comboTextAnimator.SetFloat(FlickSpeed, maxResetTime / currentTimer);
                }

                yield return null;
            }

            OnComboEnd();
        }

        private void OnComboEnd()
        {
            comboTextAnimator.SetBool(EndCombo, true);
            UIManager.Instance.SetLosingReasonText("Combo End!");
            GameManager.Instance.GameLost();
        }

        private void OnDestroy()
        {
            Score.OnScoreChanged -= UpdateScoreText;
            HitCombo.OnHitComboChanged -= UpdateComboText;

            EnemyHealth.OnEnemyHit -= TimerCoroutineController;
        }
    }
}