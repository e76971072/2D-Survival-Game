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
    }

    public void UpdateComboText(int hitStreak)
    {
        comboText.text = $"x{hitStreak}";
        comboTextAnimator.SetTrigger(Hit);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void CheckResetStreak()
    {
        if (HitCombo.Instance.resetStreak != null)
            StopCoroutine(HitCombo.Instance.resetStreak);
        HitCombo.Instance.resetStreak = HitCombo.Instance.ResetStreak(resetTime);
        StartCoroutine(HitCombo.Instance.resetStreak);
    }
}