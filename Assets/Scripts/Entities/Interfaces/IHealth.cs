namespace Entities.Interfaces
{
    public interface IHealth
    {
        void TakeDamage(float damage);
        void Heal(float heal);
        void Die();
        float CurrentHealth { get; }
        int MaxHealth { get; }
    }
}