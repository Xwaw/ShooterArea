using UnityEngine;

public static class ProjectileFactory
{
    public static void ShootProjectile(ShootType type, Vector3 startPos, Vector3 direction, float range, float recoil, float damage)
    {
        var prefab = ProjectileDataBase.Instance?.GetProjectilePrefab(type);

        if (prefab == null)
        {
            Debug.LogWarning($"Missing projectile prefab for type: {type}");
            return;
        }
        
        switch (type)
        {
            case ShootType.Line:
                var line = new ProjectileLine(prefab, startPos, direction, range, recoil, damage);
                line.Shoot();
                break;
            
            case ShootType.ProjectileDirection:
                var dir = new ProjectileDirection(prefab, startPos, direction, range, recoil, damage);
                dir.Shoot();
                break;

            case ShootType.ProjectilePhysics:
                var phy = new ProjectilePhysics(prefab, startPos, direction, range, recoil, damage);
                phy.Shoot();
                break;

            case ShootType.Gas:
                var gas = new ProjectileGas(prefab, startPos, direction, range, recoil, damage);
                gas.Shoot();
                break;

            default:
                Debug.LogWarning("Unknown projectile type");
                break;
        }
    }
}