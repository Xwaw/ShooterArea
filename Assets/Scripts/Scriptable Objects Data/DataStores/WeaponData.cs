using UnityEngine;

namespace Scriptable_Objects_Data.DataStores
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class WeaponData : ScriptableObject
    {
        [Header("Main")] 
        public string weaponName;
        public WeaponCategory category;
        public GameObject bulletPrefab;

        [Header("Stats")] 
        public float damageMultiplier;
        public float fireRateMultiplier;
        public int maxBullets;
        public int bulletsPerShoot = 1;
    
        public bool hasMagazine;
        public bool ignoreRecoilAnimation;
    
        public RangeStrength range;
        public RecoilStrength recoil;
    
        [Header("Visuals")]
        public Sprite weaponAimSprite;
        public Vector3 offsetBarrel;
    }
}


