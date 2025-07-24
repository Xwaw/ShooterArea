using UnityEngine;

namespace Weapons.Projectiles.BulletComponents.Base
{
    public class RaycastBase : MonoBehaviour
    {
        [SerializeField] private float bulletLifeTime = 0.05f;
        [SerializeField] private float lineWidth = 0.04f;

        [SerializeField] protected LineRenderer lineRenderer;

        protected void SetLineRenderer(Vector3 startPos, Vector3 endPos)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        
            Destroy(gameObject, bulletLifeTime);
        }
    }
}
