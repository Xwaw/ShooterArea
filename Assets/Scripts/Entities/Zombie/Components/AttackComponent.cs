using System;
using Entities.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Entities.Zombie.Components
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint;
        public float attackRange = 1;

        public void TryAttack(AIController ai)
        {
            if(attackPoint == null) return;
            
            var hittedTarget = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, LayerMask.GetMask("Player"));

            if (hittedTarget != null)
            {
                if (!ai.Follow.CurrentTarget.TryGetComponent<IHealth>(out var health)) return;
                health.TakeDamage(ai.Stats.Damage);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
        }

    }
}