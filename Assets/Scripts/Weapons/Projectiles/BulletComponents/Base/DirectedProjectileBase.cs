using UnityEngine;

namespace Weapons.Projectiles.BulletComponents.Base
{
    public class DirectedProjectileBase : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rb;

        protected void SetProjectileReady(Vector3 direction, float recoil, float range)
        {
            float recoilAngle = Random.Range(-recoil, recoil);
            Vector3 directionNormalized = Quaternion.Euler(0, 0, recoilAngle) * direction.normalized;
            float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            LaunchProjectile(directionNormalized, range);
        }
        private void LaunchProjectile(Vector3 direction ,float range)
        {
            rb.velocity = direction * speed;
            Destroy(gameObject, range/speed);
        }
    }
}