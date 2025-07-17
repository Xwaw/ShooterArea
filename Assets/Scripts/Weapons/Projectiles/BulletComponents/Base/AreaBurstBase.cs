using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.Projectiles.BulletComponents.Base
{
    public class AreaBurstBase : MonoBehaviour
    {
        [SerializeField] protected float speed = 5;
        [SerializeField] protected float growingSpeed = 5;
        [SerializeField] protected EffectType type;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected float durationEffectPerObject = 1;

        private EffectEntityManager _effectEntityManager;
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;
                
            _effectEntityManager = other.gameObject.GetComponent<EffectEntityManager>();
            if (_effectEntityManager == null) return;
            
            _effectEntityManager.AddEffect(type, durationEffectPerObject, 1);
            
            Destroy(gameObject);
        }

        private void Update()
        {
            gameObject.transform.localScale += new Vector3(growingSpeed, growingSpeed, 0) * Time.deltaTime; //"animation" of burst projectile
        }

        protected Vector3 SetProjectile(Vector3 direction, float recoil)
        {
            float recoilAngle = Random.Range(-recoil, recoil);
            Vector3 directionNormalized = Quaternion.Euler(0, 0, recoilAngle) * direction.normalized;
            float angle = Mathf.Atan2(directionNormalized.y, directionNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        
            return directionNormalized;
        }
    }
}
