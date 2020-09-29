using System.Collections;
using Data;
using Enemy;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private float _currentTimer;
        private Animator _comboTextAnimator;
        private Color _timerImageInitColor;
        private Score _score;
        private GameManager _gameManager;
        private UIManager _uiManager;

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int ShouldFlick = Animator.StringToHash("ShouldFlick");
        private static readonly int FlickSpeed = Animator.StringToHash("FlickSpeed");
        private static readonly int EndCombo = Animator.StringToHash("EndCombo");

        #endregion

        [Inject]
        public void Construct(Score score, GameManager gameManager, UIManager uiManager)
        {
            _score = score;
            _gameManager = gameManager;
            _uiManager = uiManager;
        }

        private void OnValidate()
        {
            scoreIncrement = Mathf.Clamp(scoreIncrement, 0, 100);
        }

        private void Awake()
        {
            _comboTextAnimator = comboText.GetComponent<Animator>();
            _timerImageInitColor = timerImage.color;

            _score.ResetScore();
            _score.ScoreIncrement = scoreIncrement;
            
            Score.OnScoreChanged += UpdateScoreText;
            HitCombo.OnHitComboChanged += UpdateComboText;

            EnemyHealth.OnEnemyHit += TimerCoroutineController;
        }

        private void UpdateComboText(int currentCombo)
        {
            comboText.text = $"x{currentCombo}";
            _comboTextAnimator.SetTrigger(Hit);
        }

        private void UpdateScoreText(int currentScore)
        {
            scoreText.text = currentScore.ToString();
        }

        private void UpdateComboTimerText()
        {
            timerImage.fillAmount = _currentTimer / maxResetTime;
        }

        private void TimerCoroutineController()
        {
            StopAllCoroutines();
            StartCoroutine(ComboTimer());
        }

        private IEnumerator ComboTimer()
        {
            _currentTimer = maxResetTime;
            _comboTextAnimator.SetBool(ShouldFlick, false);
            _comboTextAnimator.SetFloat(FlickSpeed, 0);
            while (_currentTimer > 0)
            {
                UpdateComboTimerText();
                timerImage.color = Color.Lerp(Color.red, _timerImageInitColor, _currentTimer / maxResetTime);
                _currentTimer -= Time.deltaTime;
                if (_currentTimer <= maxResetTime / 2)
                {
                    _comboTextAnimator.SetBool(ShouldFlick, true);
                    _comboTextAnimator.SetFloat(FlickSpeed, maxResetTime / _currentTimer);
                }

                yield return null;
            }

            OnComboEnd();
        }

        private void OnComboEnd()
        {
            _comboTextAnimator.SetBool(EndCombo, true);
            _uiManager.SetLosingReasonText("Combo End!");
            _gameManager.GameLost();
        }

        private void OnDestroy()
        {
            Score.OnScoreChanged -= UpdateScoreText;
            HitCombo.OnHitComboChanged -= UpdateComboText;

            EnemyHealth.OnEnemyHit -= TimerCoroutineController;
        }
    }
}