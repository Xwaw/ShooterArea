using UnityEngine;

public abstract class BaseProjectile
{
    protected GameObject projectilePrefab;
    protected GameObject instancePrefab;
    
    protected Vector3 startPos;
    protected Vector3 shootDirection;
    protected float range;
    protected float recoil;

    public BaseProjectile(GameObject projectilePrefab, Vector3 startPos, Vector3 shootDirection, float range, float recoil)
    {
        this.projectilePrefab = projectilePrefab;
        this.startPos = startPos;
        this.shootDirection = shootDirection.normalized;
        this.range = range;
        this.recoil = recoil;
    }

    protected float GetRandomRecoil()
    {
        return Random.Range(-recoil, recoil);
    }

    protected Vector3 ApplyRecoil(Vector3 direction, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * direction;
    }

    public GameObject GetInstance()
    {
        return instancePrefab;
    }

    public abstract void Shoot();
}