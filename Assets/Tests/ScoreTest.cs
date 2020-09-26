using Data;
using NUnit.Framework;

namespace Tests
{
    public class ScoreTest
    {
        private Score _score;

        [SetUp]
        public void SetUp()
        {
            _score = Score.Instance;
        }
        
        [Test]
        public void Current_Score_Hit_Combo_Reset_To_0_When_Call_Score_Reset()
        {
            _score.ResetScore();

            Assert.AreEqual(0, _score.CurrentScore);
            Assert.AreEqual(0, HitCombo.Instance.CurrentHitCombo);
        }

        [Test]
        public void Current_Score_Increased_By_The_Score_Increment_Amount_When_Increase_One_Time()
        {
            _score.ResetScore();
            _score.IncreaseScore();
            
            Assert.AreEqual(_score.ScoreIncrement, _score.CurrentScore);
        }
        
        [Test]
        public void Current_Score_Increased_By_The_Score_Increment_Plus_Score_Increment_Multiply_By_Combo_When_Increase_Twice()
        {
            _score.ResetScore();
            _score.IncreaseScore();
            _score.IncreaseScore();
            
            Assert.AreEqual(_score.ScoreIncrement + _score.ScoreIncrement * HitCombo.Instance.CurrentHitCombo, _score.CurrentScore);
        }
    }
}