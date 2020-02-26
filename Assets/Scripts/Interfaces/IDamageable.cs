namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damageAmount);
    }

    public interface IHealable
    {
        void Heal(int healAmount);
    }

    public interface IShieldable
    {
        int MaxShield { get; }
        int CurrentShield { get; }
        bool IsShieldEmpty();
    }

    public interface IAudioHandler
    {
        void PlayAudioSource();
    }
}