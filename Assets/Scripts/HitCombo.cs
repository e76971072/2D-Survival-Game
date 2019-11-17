using System.Collections;
using UnityEngine;

public class HitCombo
{
    #region SerializeFields

    private static HitCombo instance;

    public static HitCombo Instance => instance ?? (instance = new HitCombo());

    public IEnumerator resetStreak;
    public int hitStreak;

    #endregion

    public void IncreaseStreak()
    {
        hitStreak += 1;
        ScoreSystemUI.instance.CheckResetStreak();
    }

    public IEnumerator ResetStreak(float resetTime)
    {
        yield return new WaitForSeconds(resetTime);
        hitStreak = 0;
        ScoreSystemUI.instance.UpdateComboText(hitStreak);
    }
}