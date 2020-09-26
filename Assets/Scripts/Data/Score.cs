using System;
using Enemy;
using Helpers;
using UnityEngine;

namespace Data
{
    public class Score
    {
        public static Score Instance => _instance ?? (_instance = new Score());
        public static event Action<int> OnScoreChanged;

        private static Score _instance;

        private const string Highscore = "highscore";
        private int _currentScore;
        public int ScoreIncrement = 10;

        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                _currentScore = Mathf.Clamp(value, 0, int.MaxValue);
                OnScoreChanged?.Invoke(CurrentScore);
            }
        }

        private Score()
        {
            ResetScore();

            GameManager.OnGameLost += SaveScore;
            EnemyHealth.OnEnemyHit += IncreaseScore;
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            HitCombo.Instance.ResetStreak();
        }

        public void IncreaseScore()
        {
            HitCombo.Instance.IncreaseStreak();
            CurrentScore += ScoreIncrement * HitCombo.Instance.CurrentHitCombo;
        }

        private void SaveScore()
        {
            var currentHighScore = PlayerPrefs.GetInt(Highscore);
            if (currentHighScore >= CurrentScore) return;
            PlayerPrefs.SetInt(Highscore, CurrentScore);
        }

        public static int LoadHighScore()
        {
            return PlayerPrefs.GetInt(Highscore);
        }
    }
}