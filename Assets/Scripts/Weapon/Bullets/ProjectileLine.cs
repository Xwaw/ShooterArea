using Entities;
using UnityEngine;

public class ProjectileLine : BaseProjectile
{
    private float bulletLifeTime = 0.05f;
    private float lineWidth = 0.04f;
    private LineRenderer lineRenderer;
    private float damage;

    public ProjectileLine(GameObject projectilePrefab, Vector3 startPos, Vector3 direction, float range, float recoil, float damage)
        : base(projectilePrefab, startPos, direction, range, recoil)
    {
        this.damage = damage;
    }

    public override void Shoot()
    {
        Vector3 finalDir = ApplyRecoil(shootDirection, GetRandomRecoil()).normalized;
        RaycastHit2D hit = Physics2D.Raycast(startPos, finalDir, range);
        Vector3 endPos = hit.collider != null ? hit.point : startPos + finalDir * range;

        instancePrefab = Object.Instantiate(projectilePrefab, startPos, Quaternion.identity);
        lineRenderer = instancePrefab.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);

        takeDamageByRaycastHit(hit, damage);

        Object.Destroy(instancePrefab, bulletLifeTime);
        
        Debug.DrawLine(startPos, endPos, Color.red, 0.5f);
    }

    public RaycastHit2D GetHit()
    {
        Vector3 finalDir = ApplyRecoil(shootDirection, GetRandomRecoil()).normalized;
        return Physics2D.Raycast(startPos, finalDir, range);
    }

    private void takeDamageByRaycastHit(RaycastHit2D hit, float damage)
    {
        if (hit.collider != null)
        {
            GameObject hittedObj = hit.collider.gameObject;
            HealthEntityManager health = hittedObj.GetComponent<HealthEntityManager>();
            health?.TakeDamage(damage);
        }
    }
}