﻿public static class Score
{
    #region NonSerializeFields

    public static int currentScore;

    #endregion

    public static void IncreaseScore()
    {
        currentScore += ScoreSystemUI.instance.scoreIncrement * HitCombo.hitStreak;
    }
}