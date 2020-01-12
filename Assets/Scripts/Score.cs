public static class Score
{
    #region NonSerializeFields

    public static int currentScore;

    #endregion

    public static void ResetScore()
    {
        currentScore = 0;
    }

    public static void IncreaseScore()
    {
        currentScore += ScoreSystemUI.instance.scoreIncrement * HitCombo.Instance.hitCombo;
    }
}