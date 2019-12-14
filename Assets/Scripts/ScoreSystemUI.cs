using TMPro;
using UnityEngine;

public class ScoreSystemUI : MonoBehaviour
{
    #region SerializeFields

    public static ScoreSystemUI instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private int resetTime = 3;

    public int scoreIncrement;

    #endregion

    #region NonSerializeFields

    private Animator comboTextAnimator;
    private static readonly int Hit = Animator.StringToHash("Hit");

    #endregion

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        comboTextAnimator = comboText.GetComponent<Animator>();
        
        EnemyHealth.EnemyHit += HitCombo.IncreaseStreak;
        EnemyHealth.EnemyHit += Score.IncreaseScore;
        EnemyHealth.EnemyHit += UpdateComboText;
        EnemyHealth.EnemyHit += UpdateScoreText;
    }

    public void UpdateComboText()
    {
        comboText.text = $"x{HitCombo.hitStreak}";
        comboTextAnimator.SetTrigger(Hit);
    }

    public void UpdateScoreText()
    {
        scoreText.text = Score.currentScore.ToString();
    }

    public void CheckResetStreak()
    {
        if (HitCombo.resetStreak != null)
            StopCoroutine(HitCombo.resetStreak);
        HitCombo.resetStreak = HitCombo.ResetStreak(resetTime);
        StartCoroutine(HitCombo.resetStreak);
    }
}