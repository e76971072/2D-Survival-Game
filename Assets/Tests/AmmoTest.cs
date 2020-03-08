using Attacks;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    public class AmmoTest
    {
        private Ammo ammo;
        private const int DefaultMaxAmmo = 30;
        private const int DefaultMaxAmmoPerClip = 10;

        [SetUp]
        public void SetUp()
        {
            ammo = new Ammo(DefaultMaxAmmo, DefaultMaxAmmoPerClip);
        }
        
        [Test]
        public void Ammo_Is_Empty_When_Both_Current_Ammo_And_Current_Max_Ammo_Is_0()
        {
            ammo = new Ammo(0, 0);

            Assert.IsTrue(ammo.IsAmmoEmpty());
        }

        [Test]
        public void Ammo_Is_Not_Empty_When_Only_Current_Ammo_Is_0()
        {
            ammo = new Ammo(0, DefaultMaxAmmoPerClip);

            Assert.IsFalse(ammo.IsAmmoEmpty());
        }

        [Test]
        public void Ammo_Is_Not_Empty_When_Only_Current_Max_Ammo_Is_0()
        {
            ammo = new Ammo(DefaultMaxAmmo, 0);

            Assert.IsFalse(ammo.IsAmmoEmpty());
        }

        [Test]
        public void Current_Ammo_Only_Reduced_By_1_When_Calling_Reduce_Ammo()
        {
            ammo.ReduceCurrentAmmo();
            
            Assert.AreEqual(DefaultMaxAmmoPerClip - 1, ammo.CurrentAmmo);
        }
        
        [Test]
        public void Current_Ammo_Dont_Go_Below_0_When_Calling_Reduce_Ammo()
        {
            for (var i = 0; i < DefaultMaxAmmoPerClip + 10; i++)
            {
                ammo.ReduceCurrentAmmo();
            }
            
            Assert.AreEqual(0, ammo.CurrentAmmo);
        }
        
        [Test]
        public void Current_Ammo_Only_Reduced_By_The_Specified_Amount_When_Calling_Reduce_Ammo_With_Parameter()
        {
            const int reduceAmount = 5;
            
            ammo.ReduceCurrentAmmo(reduceAmount);
            
            Assert.AreEqual(DefaultMaxAmmoPerClip - reduceAmount, ammo.CurrentAmmo);
        }
        
        [Test]
        public void Current_Ammo_Dont_Go_Below_0_When_Calling_Reduce_Ammo_With_Parameter()
        {
            const int reduceAmount = DefaultMaxAmmoPerClip + 10;
            
            ammo.ReduceCurrentAmmo(reduceAmount);
            
            Assert.AreEqual(0, ammo.CurrentAmmo);
        }
        
        [Test]
        public void Emptying_Current_Ammo_Then_Reload_Reset_Current_Ammo_Back_To_Full()
        {
            ammo.ReduceCurrentAmmo(DefaultMaxAmmoPerClip);
            ammo.Reload();
            
            Assert.AreEqual(DefaultMaxAmmoPerClip, ammo.CurrentAmmo);
        }

        [Test]
        public void Emptying_Current_Ammo_Then_Reload_Subtract_The_Reload_Amount_From_Current_Max_Ammo()
        {
            ammo.ReduceCurrentAmmo(DefaultMaxAmmoPerClip);
            ammo.Reload();
            
            Assert.AreEqual(DefaultMaxAmmo - DefaultMaxAmmoPerClip, ammo.CurrentMaxAmmo);
        }
        
        [Test]
        public void Reduce_Current_Ammo_By_5_Then_Reload_Reset_Current_Ammo_Back_To_Full()
        {
            ammo.ReduceCurrentAmmo(5);
            ammo.Reload();
            
            Assert.AreEqual(DefaultMaxAmmoPerClip, ammo.CurrentAmmo);
        }
        
        [Test]
        public void Reduce_Current_Ammo_By_5_Then_Reload_Subtract_The_Reload_Amount_From_Current_Max_Ammo()
        {
            const int reduceAmount = 5;
            
            ammo.ReduceCurrentAmmo(reduceAmount);
            ammo.Reload();
            
            Assert.AreEqual(DefaultMaxAmmo - reduceAmount, ammo.CurrentMaxAmmo);
        }
        
        [Test]
        public void If_Max_Ammo_Is_Not_Enough_To_Reload_To_Full_Then_Reload_To_Max_Possible()
        {
            const int maxAmmo = 5;
            const int maxAmmoPerClip = 10;
            ammo = new Ammo(maxAmmo, maxAmmoPerClip);
            const int reduceAmount = 8;
            
            ammo.ReduceCurrentAmmo(reduceAmount);
            ammo.Reload();

            Assert.AreEqual(maxAmmoPerClip - reduceAmount + maxAmmo, ammo.CurrentAmmo);
        }
    }
}