using UnityEngine;

namespace Weapons.Projectiles.BulletComponents.Base
{
    public class PhysicProjectileBase : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected float speed;

        protected Vector3 SetProjectile(Vector3 direction, float recoil)
        {
            float recoilAngle = Random.Range(-recoil, recoil);
            Vector3 directionNormalized = Quaternion.Euler(0, 0, recoilAngle) * direction.normalized;
            float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        
            return directionNormalized;
        }
    
        protected void SetImageRotationByVelocity(Rigidbody2D rb)
        {
            Vector2 velocity = rb.velocity;
            if (velocity.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) return;
        }
    }
}
