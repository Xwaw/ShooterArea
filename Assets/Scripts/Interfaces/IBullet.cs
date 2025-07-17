using UnityEngine;

public interface IBullet
{
    void Init(Vector3 start, Vector3 direction, float recoil, float range, float damage);
}
