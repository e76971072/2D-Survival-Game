using System.Collections;
using UnityEngine;

public static class HitCombo
{
    #region SerializeFields

    public static IEnumerator resetStreak;
    public static int hitStreak;

    #endregion

    public static void ResetStreak()
    {
        hitStreak = 0;
    }

    public static void IncreaseStreak()
    {
        hitStreak += 1;
        ScoreSystemUI.instance.CheckResetStreak();
    }

    public static IEnumerator ResetStreak(float resetTime)
    {
        yield return new WaitForSeconds(resetTime);
        hitStreak = 0;
        ScoreSystemUI.instance.UpdateComboText();
        // GameManager.Instance.LoseGame();
    }
}