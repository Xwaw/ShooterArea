using UnityEngine;

namespace Weapons.Projectiles.BulletComponents.Base
{
    public class RaycastBase : MonoBehaviour
    {
        [SerializeField] private float bulletLifeTime = 0.05f;
        [SerializeField] private float lineWidth = 0.04f;

        [SerializeField] private LineRenderer lineRenderer;

        protected void SetLineRenderer(LineRenderer line, Vector3 startPos, Vector3 endPos)
        {
            line.positionCount = 2;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.SetPosition(0, startPos);
            line.SetPosition(1, endPos);
        
            Destroy(gameObject, bulletLifeTime);
        }

        protected LineRenderer LineRenderer => lineRenderer;
    }
}
