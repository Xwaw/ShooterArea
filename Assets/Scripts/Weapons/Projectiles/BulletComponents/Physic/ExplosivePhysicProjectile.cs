using Effects;
using UnityEngine;
using Weapons.Projectiles.Base;

namespace Weapons.Projectiles.Physic
{
    public class ExplosivePhysicProjectile : PhysicProjectileBase, IBullet
    {
        [SerializeField] private GameObject explosivePrefab;

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

            GameObject explosionObject = Instantiate(explosivePrefab, transform.position, Quaternion.identity);
            var script = explosionObject.GetComponent<ExplosionEffect>();
            script.Init(_damage);
        
            Destroy(gameObject);
        }
        void FixedUpdate()
        {
            SetImageRotationByVelocity(rb);
        }
    }
}
