using System;
using Entities.Interfaces;
using UnityEngine;

namespace Entities
{
    public class HealthEntityManager : MonoBehaviour, IHealth
    {
        public event Action OnEntityDeath;
        public event Action OnHealthChanged;

        [SerializeField] private int maxHealth = 100;

        public float CurrentHealth { get; private set; }

        public int MaxHealth => maxHealth;
        void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
            
            OnHealthChanged?.Invoke();
    
            if (CurrentHealth <= 0)
                Die();
        }

        public void Heal(float heal)
        {
            CurrentHealth += heal;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
            
            OnHealthChanged?.Invoke();
        }

        public void Die()
        {
            Debug.Log(gameObject.name + " is dead");
            
            OnEntityDeath?.Invoke();
            OnEntityDeath = null;
        }
    }
}