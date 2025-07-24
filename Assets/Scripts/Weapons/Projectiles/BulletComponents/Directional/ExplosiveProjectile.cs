using Effects;
using UnityEngine;
using Weapons.Projectiles.BulletComponents.Base;

namespace Weapons.Projectiles.BulletComponents.Directional
{
    public class ExplosiveProjectile : DirectedProjectileBase, IBullet
    {
        [SerializeField] private GameObject explosionPrefab;

        private float _damage; 

        public void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage)
        {
            SetProjectileReady(direction, recoil, range);

            _damage = damage;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var explosionObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            var script = explosionObject.GetComponent<ExplosionEffect>();
            script.Init(_damage);
        
            Destroy(gameObject);
        }
    }
}
