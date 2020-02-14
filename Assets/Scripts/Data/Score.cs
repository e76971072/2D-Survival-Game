using UI;

namespace Data
{
    public static class Score
    {
        public static int currentScore;

        public static void ResetScore()
        {
            currentScore = 0;
        }

        public static void IncreaseScore()
        {
            currentScore += ScoreSystemUI.Instance.scoreIncrement * HitCombo.Instance.hitCombo;
        }
    }
}