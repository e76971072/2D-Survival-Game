namespace Interfaces
{
    public interface IShieldable
    {
        int MaxShield { get; }
        int CurrentShield { get; }
        bool IsShieldEmpty();
    }
}