using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public ShootType shootType;
    public ProjectileProperty projectileProperty;
    public int bulletsPerShoot = 1;

    [Header("Stats")] 
    public float damageMultiplier;
    public float fireRateMultiplier;
    public int magazineCapacity;
    
    public bool canShootOnReload;
    
    public RangeStrength range;
    public RecoilStrength recoil;
    
    [Header("Visuals")]
    public Sprite weaponAimSprite;
    public Vector3 offsetBarrel;
    public ProjectileImage bulletImage;
}


