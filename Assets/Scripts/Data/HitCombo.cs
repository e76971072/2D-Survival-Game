namespace Data
{
    public class HitCombo
    {
        public static HitCombo Instance => _instance ?? (_instance = new HitCombo());

        public int hitCombo;

        private static HitCombo _instance;
        private float maxResetTime;

        public void ResetStreak()
        {
            hitCombo = 0;
        }

        public void IncreaseStreak()
        {
            hitCombo += 1;
        }
    }
}