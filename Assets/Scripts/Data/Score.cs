using UI;
using UnityEngine;

namespace Data
{
    public static class Score
    {
        private const string Highscore = "highscore";
        public static int currentScore;

        public static void ResetScore()
        {
            currentScore = 0;
        }

        public static void IncreaseScore()
        {
            currentScore += ScoreSystemUI.Instance.scoreIncrement * HitCombo.Instance.hitCombo;
        }

        public static void SaveScore()
        {
            var currentHighScore = PlayerPrefs.GetInt(Highscore);
            if (currentHighScore >= currentScore) return;
            PlayerPrefs.SetInt(Highscore, currentScore);
        }

        public static int LoadHighScore()
        {
            return PlayerPrefs.GetInt(Highscore);
        }
    }
}