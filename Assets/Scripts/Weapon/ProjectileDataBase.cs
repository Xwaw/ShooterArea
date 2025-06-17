using System.Collections.Generic;
using UnityEngine;

public class ProjectileDataBase : MonoBehaviour
{
    public List<GameObject> projectilePrefabs; // 0: Line, 1: Direction, 2: Physics, 3: Gas

    public static ProjectileDataBase Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetProjectilePrefab(ShootType type)
    {
        return type switch
        {
            ShootType.Line => projectilePrefabs[0],
            ShootType.ProjectileDirection => projectilePrefabs[1],
            ShootType.ProjectilePhysics => projectilePrefabs[2],
            ShootType.Gas => projectilePrefabs[3],
            _ => null,
        };
    }
}