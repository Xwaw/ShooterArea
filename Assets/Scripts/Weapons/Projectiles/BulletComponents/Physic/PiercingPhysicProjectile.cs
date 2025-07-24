using Entities.Interfaces;
using Interfaces;
using UnityEngine;
using Weapons.Projectiles.BulletComponents.Base;

namespace Weapons.Projectiles.BulletComponents.Physic
{
    public class PiercingPhysicProjectile : PhysicProjectileBase, IBullet
    {

        private float _damage;
        public void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage)
        {
            Vector3 directionNormalized = SetProjectile(direction, recoil);
            _damage = damage;
        
            rb.velocity = new Vector2(directionNormalized.x * (range * speed), directionNormalized.y * (range * speed));
        
            float lifetime = range / (range * speed);
            Destroy(gameObject, lifetime);
        }
    
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) return;
            
            if (other.TryGetComponent<IHealth>(out var health) && other.TryGetComponent<IStats>(out var stats))
            {
                health.TakeDamage(stats.Defence > _damage ? 1 : _damage - stats.Defence);
            }
        }
    
        void FixedUpdate()
        {
            SetImageRotationByVelocity(rb);
        }
    }
}
