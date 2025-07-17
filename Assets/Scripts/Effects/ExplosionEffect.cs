using Entities;
using Entities.Interfaces;
using Interfaces;
using UnityEngine;

namespace Effects
{
    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float radiusDamage;

        private float _damage;

        public void Init(float damage)
        {
            _damage = damage;
        }
        void Start()
        {
            SetDamageByExplosion(transform.position, radiusDamage, _damage);
        
            Destroy(gameObject, 0.5f);
        }
    
        private void SetDamageByExplosion(Vector3 pos, float radius, float damage)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(pos, radius);
        
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<IHealth>(out var health) && hit.TryGetComponent<IStats>(out var stats))
                {
                    if (health == null) continue;
                    var positionTarget = hit.transform.position;
                    var distanceDamageTake = Vector2.Distance(pos, positionTarget);
                    
                    health.TakeDamage(stats.Defence > _damage ? 1 : (_damage - stats.Defence) / distanceDamageTake);
                    
                    Debug.Log(stats.Defence > _damage ? 1 : (_damage - stats.Defence) / distanceDamageTake);
                }
            }
        }
    }
}
