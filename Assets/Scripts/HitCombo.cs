using System;
using System.Collections;
using System.Timers;

public class HitCombo
{
    #region SerializeFields

    public static HitCombo Instance => _instance ?? (_instance = new HitCombo());

    private static HitCombo _instance;
 
    public static event Action OnTimerPassed = delegate { };
    public static event Action OnTimerEnded = delegate { };
    
    public int hitCombo;
    private float maxResetTime;

    #endregion

    public void ResetStreak()
    {
        hitCombo = 0;
    }

    public void IncreaseStreak()
    {
        hitCombo += 1;
    }
}