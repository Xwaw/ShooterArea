using UnityEngine;
using Weapons.Projectiles.Base;

namespace Weapons.Projectiles.BulletComponents.Physic
{
    public class DeployPhysicProjectile : PhysicProjectileBase, IBullet
    {
        public float damage;
        public void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage)
        {
            Vector3 directionNormalized = SetProjectile(direction, recoil);
            this.damage = damage;
        
            rb.velocity = new Vector2(directionNormalized.x * (range * speed), directionNormalized.y * (range * speed));
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Ground")) return;        
        
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        private void FixedUpdate()
        {
            SetImageRotationByVelocity(rb);
        }
    }
}
