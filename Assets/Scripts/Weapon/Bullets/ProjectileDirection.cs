using UnityEngine;

public class ProjectileDirection : BaseProjectile
{
    private float bulletLifeTime;
    private float velocityOfBullet = 100f;

    private float damage;

    public ProjectileDirection(GameObject projectilePrefab, Vector3 startPos, Vector3 direction, float range, float recoil, float damage)
        : base(projectilePrefab, startPos, direction, range, recoil)
    {
        bulletLifeTime = range / velocityOfBullet;

        this.damage = damage;
    }

    public override void Shoot()
    {
        Vector3 finalDir = ApplyRecoil(shootDirection, GetRandomRecoil()).normalized;
        float angle = Mathf.Atan2(finalDir.y, finalDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        instancePrefab = Object.Instantiate(projectilePrefab, startPos, rotation);
        var logic = instancePrefab.GetComponent<ProjectileDirectionLogic>();
        logic.Init(velocityOfBullet, finalDir, damage);

        Object.Destroy(instancePrefab, bulletLifeTime);
    }
}