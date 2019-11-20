public class Score
{
    #region SerializeFields

    private static Score instance;
    
    public static Score Instance => instance ?? (instance = new Score());
    
    #endregion

    #region NonSerializeFields

    public int currentScore;

    #endregion

    public Score()
    {
        EnemyHealth.EnemyHit += IncreaseScore;
    }

    public void EnemyHit()
    {
        HitCombo.Instance.IncreaseStreak();
        IncreaseScore();

        ScoreSystemUI.instance.UpdateComboText();
        ScoreSystemUI.instance.UpdateScoreText();
    }

    public void IncreaseScore()
    {
        currentScore += ScoreSystemUI.instance.scoreIncrement * HitCombo.Instance.hitStreak;
    }
}