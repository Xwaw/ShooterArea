using System;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.Zombie.Components
{
    public class AttackComponent : MonoBehaviour
    {
        public float attackRange = 1f;
        public float damage = 10f;
        public float cooldown = 1f;

        private float _timer;

        public void TryAttack(AIController ai)
        {
            _timer += Time.deltaTime;
            if (_timer < cooldown) return;

            float dist = Vector2.Distance(transform.position, ai.Follow.target.position);
            if (dist > attackRange) return;

            if (ai.Follow.target.TryGetComponent<IHealth>(out var health))
                health.TakeDamage(damage);

            _timer = 0;
        }
    }
}