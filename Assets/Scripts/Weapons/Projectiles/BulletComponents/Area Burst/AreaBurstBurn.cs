using UnityEngine;
using Weapons.Projectiles.BulletComponents.Base;

namespace Weapons.Projectiles.BulletComponents.Area_Burst
{
    public class AreaBurstBurn : AreaBurstBase, IBullet
    {
        public void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage)
        {
            float finalRange = range/growingSpeed;
            rb.velocity = SetProjectile(direction, recoil) * finalRange;
            
            Destroy(gameObject, finalRange/speed);
        }
    }
}