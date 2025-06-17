using UnityEngine;

public class ProjectileGas : BaseProjectile
{
    private float spread = 5f;
    private float damage;

    public ProjectileGas(GameObject projectilePrefab, Vector3 startPos, Vector3 direction, float range, float recoil, float damage)
        : base(projectilePrefab, startPos, direction, range / 4f, recoil)
    { this.damage = damage; }

    public override void Shoot()
    {
        Vector3 finalDir = ApplyRecoil(shootDirection, GetRandomRecoil());

        instancePrefab = Object.Instantiate(projectilePrefab, startPos, Quaternion.identity);
        var logic = instancePrefab.GetComponent<ProjectileGasLogic>();
        logic.Init(finalDir, damage);

        Object.Destroy(instancePrefab, range);
    }
}