using System;
using Player;
using Scriptable_Objects_Data.DataStores;
using UnityEngine;
using Weapons.Appearance;

namespace Weapons.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private InventoryManager _inventoryManager;
        private StatManager _statManager;
        private WeaponRender _weaponRender;

        public event Action WeaponFired; 
        public bool CanShootThisFrame { get; private set; }

        private float _fireCooldownTimer = 0f;
        
        [SerializeField] private LayerMask[] layersToCollide;

        void Awake()
        {
            _inventoryManager = GetComponentInParent<InventoryManager>();
            _statManager = GetComponentInParent<StatManager>();
            _weaponRender = GetComponentInParent<WeaponRender>();
        }

        void Update()
        {
            float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        
            UpdateSwitchingWeaponEvent(scrollValue);

            CanShootThisFrame = IsCooldownEnabledForShoot();

            ReloadWeapon();
        
            WeaponShoot();
        }
    
        private void WeaponShoot()
        {
            if (CanShootThisFrame && _inventoryManager.GetCurrentWeapon().MagazineAmmo > 0) 
            {
                Vector3 startPos = _weaponRender.GetBarrelTransform.position;
                Vector3 endPos = _weaponRender.GetBarrelTransform.right;

                WeaponData currentWeapon = _inventoryManager.GetCurrentWeapon().GetWeaponData;
                float range = (float) currentWeapon.range;
                float recoil = (float) currentWeapon.recoil;
                float damage = currentWeapon.damageMultiplier * _statManager.Damage;
            
                for (int i = 0; i < currentWeapon.bulletsPerShoot; i++)
                {
                    GameObject bullet = Instantiate(currentWeapon.bulletPrefab, startPos, Quaternion.identity);
                
                    if (bullet.TryGetComponent<IBullet>(out var bulletLogic))
                    {
                        bulletLogic.Init(startPos, endPos, recoil, range, damage);
                    }
                }
            
                _inventoryManager.GetCurrentWeapon().TakeCurrentAmmo(1);
            
                WeaponFired?.Invoke();
            }
        }

        private bool IsCooldownEnabledForShoot()
        {
            if (_inventoryManager == null || _statManager == null) return false;
        
            _fireCooldownTimer += Time.deltaTime;

            bool isLpmPressed = Input.GetMouseButton(0);

            float fireRateWeapon = _inventoryManager.GetCurrentWeapon().GetWeaponData.fireRateMultiplier;
            float attackSpeedPlayer = _statManager.AttackSpeed;

            float timeBeetweenShots = 1f / (fireRateWeapon * attackSpeedPlayer);
        
            if (isLpmPressed && _fireCooldownTimer >= timeBeetweenShots)
            {
                _fireCooldownTimer = 0f;
                return true;
            }
        
            return false;
        }
    
        private void UpdateSwitchingWeaponEvent(float scrollValue)
        {
            switch (scrollValue)
            {
                case > 0:
                    _inventoryManager.SwitchWeapon(1);
                    break;
                case < 0:
                    _inventoryManager.SwitchWeapon(-1);
                    break;
            }
        }

        private void ReloadWeapon()
        {
            if (Input.GetKeyDown(KeyCode.R) || _inventoryManager.GetCurrentWeapon().MagazineAmmo <= 0)
            {
                if (_inventoryManager == null || _statManager == null) return;
            
                _inventoryManager.GetCurrentWeapon().ReloadAmmo();
            }
        }
    }
}