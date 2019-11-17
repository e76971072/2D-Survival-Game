public class ScoreSystem
{
    #region SerializeFields

    private static ScoreSystem instance;
    
    public static ScoreSystem Instance => instance ?? (instance = new ScoreSystem());
    
    #endregion

    #region NonSerializeFields

    private int currentScore;

    #endregion

    public void EnemyHit()
    {
        HitCombo.Instance.IncreaseStreak();
        currentScore += ScoreSystemUI.instance.scoreIncrement * HitCombo.Instance.hitStreak;

        ScoreSystemUI.instance.UpdateComboText(HitCombo.Instance.hitStreak);
        ScoreSystemUI.instance.UpdateScoreText(currentScore);
    }
}