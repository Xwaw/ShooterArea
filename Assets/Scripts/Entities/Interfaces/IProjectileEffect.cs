using UnityEngine;

namespace Entities.Interfaces
{
    public interface IProjectileEffect
    {
        void OnHit(Collider2D target, float damage);
    }
}