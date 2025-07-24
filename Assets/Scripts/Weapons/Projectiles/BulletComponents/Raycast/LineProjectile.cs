using Entities.Interfaces;
using Interfaces;
using UnityEngine;
using Weapons.Projectiles.BulletComponents.Base;

namespace Weapons.Projectiles.BulletComponents.Raycast
{
    public class LineProjectile : RaycastBase, IBullet
    {
        public void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage)
        {
            float angle = Random.Range(-recoil, recoil);
            Vector3 finalDir = (Quaternion.Euler(0, 0, angle) * direction).normalized;

            RaycastHit2D hit = Physics2D.Raycast(start, finalDir, range);
            Vector3 endPos = hit.collider ? hit.point : start + finalDir * range;

            SetLineRenderer(start, endPos);

            if (hit.collider != null)
            {
                float defence = 0;
                if (hit.collider.TryGetComponent<IStats>(out var stats))
                {
                    defence = stats.Defence;
                }
                if (hit.collider.TryGetComponent<IHealth>(out var health))
                {
                    health.TakeDamage(defence > damage ? 1 : damage - defence);
                }
            }
        }
    }
}


