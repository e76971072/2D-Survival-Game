using System;
using UnityEngine;

namespace Data
{
    public class HitCombo
    {
        public static HitCombo Instance => _instance ?? (_instance = new HitCombo());
        public static event Action<int> OnHitComboChanged;

        private static HitCombo _instance;
        private int hitCombo;

        public int CurrentHitCombo
        {
            get => hitCombo;
            private set
            {
                hitCombo = Mathf.Clamp(value, 0, int.MaxValue);
                OnHitComboChanged?.Invoke(CurrentHitCombo);
            }
        }

        public void ResetStreak()
        {
            CurrentHitCombo = 0;
        }

        public void IncreaseStreak()
        {
            CurrentHitCombo++;
        }
    }
}