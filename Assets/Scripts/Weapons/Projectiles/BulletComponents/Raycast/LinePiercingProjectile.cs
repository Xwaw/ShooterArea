using Entities.Interfaces;
using Interfaces;
using UnityEngine;
using Weapons.Projectiles.BulletComponents.Base;

namespace Weapons.Projectiles.BulletComponents.Raycast
{
    public class LinePiercingProjectile : RaycastBase, IBullet
    {
        public void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage)
        {
            float angle = Random.Range(-recoil, recoil);
            Vector3 finalDir = (Quaternion.Euler(0, 0, angle) * direction).normalized;

            RaycastHit2D[] hits = Physics2D.RaycastAll(start, finalDir, range);
            Vector3 endPos = hits.Length > 0 ? hits[^1].point : start + finalDir * range;

            SetLineRenderer(start, endPos);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Player") &&
                    hit.collider.gameObject.CompareTag("Bullet")) return;
                
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

