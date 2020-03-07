using Data;
using NUnit.Framework;

namespace Tests
{
    public class ScoreTest
    {
        private Score score;

        [SetUp]
        public void SetUp()
        {
            score = Score.Instance;
        }
        
        [Test]
        public void Current_Score_Hit_Combo_Reset_To_0_When_Call_Score_Reset()
        {
            score.ResetScore();

            Assert.AreEqual(0, score.CurrentScore);
            Assert.AreEqual(0, HitCombo.Instance.CurrentHitCombo);
        }

        [Test]
        public void Current_Score_Increased_By_The_Score_Increment_Amount_When_Increase_One_Time()
        {
            score.ResetScore();
            score.IncreaseScore();
            
            Assert.AreEqual(score.ScoreIncrement, score.CurrentScore);
        }
        
        [Test]
        public void Current_Score_Increased_By_The_Score_Increment_Plus_Score_Increment_Multiply_By_Combo_When_Increase_Twice()
        {
            score.ResetScore();
            score.IncreaseScore();
            score.IncreaseScore();
            
            Assert.AreEqual(score.ScoreIncrement + score.ScoreIncrement * HitCombo.Instance.CurrentHitCombo, score.CurrentScore);
        }
    }
}