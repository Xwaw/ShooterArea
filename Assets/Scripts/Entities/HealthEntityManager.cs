using System;
using Entities.Interfaces;
using UnityEngine;

namespace Entities
{
    public class HealthEntityManager : MonoBehaviour, IHealth
    {
        public event Action OnEntityDeath;

        [SerializeField] private int maxHealth = 100;
        private float currentHealth;
        
        public float CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;
        void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    
            if (currentHealth <= 0)
                Die();
        }

        public void Heal(float heal)
        {
            currentHealth += heal;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        public void Die()
        {
            Debug.Log(gameObject.name + " is dead");
            
            OnEntityDeath?.Invoke();
            OnEntityDeath = null;
        }
    }
}