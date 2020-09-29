using Data;
using NUnit.Framework;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class ScoreTest : ZenjectUnitTestFixture
    {
        private Score _score;
        
        public override void Setup()
        {
            base.Setup();
            
            SignalBusInstaller.Install(Container);

            Container.Bind<Score>().AsSingle();
            Container.Bind<HitCombo>().FromNew().AsSingle();
            _score = Container.Resolve<Score>();
        }

        [Test]
        public void Current_Score_Reset_To_0_When_Call_Score_Reset()
        {
            _score.ResetScore();
            
            Assert.AreEqual(0, _score.CurrentScore);
            Assert.AreEqual(0, _score.HitCombo.CurrentHitCombo);
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
            
            Assert.AreEqual(_score.ScoreIncrement + _score.ScoreIncrement * _score.HitCombo.CurrentHitCombo, _score.CurrentScore);
        }
    }
}