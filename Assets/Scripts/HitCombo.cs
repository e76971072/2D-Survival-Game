public class HitCombo
{
    #region SerializeFields

    public static HitCombo Instance => _instance ?? (_instance = new HitCombo());

    private static HitCombo _instance;

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