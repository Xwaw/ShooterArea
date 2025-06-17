using UnityEngine;

public class ProjectilePhysics : BaseProjectile
{
    private float damage;

    public ProjectilePhysics(GameObject projectilePrefab, Vector3 startPos, Vector3 direction, float range,
        float recoil, float damage)
        : base(projectilePrefab, startPos, direction, range, recoil)
    {
        this.damage = damage;
    }

    public override void Shoot()
    {
        Vector3 finalDir = ApplyRecoil(shootDirection * range, GetRandomRecoil()).normalized;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        instancePrefab = Object.Instantiate(projectilePrefab, startPos, rotation);
        ProjectilePhysicsLogic logic = instancePrefab.GetComponent<ProjectilePhysicsLogic>();
        logic.Init(finalDir, range, damage);
    }
}