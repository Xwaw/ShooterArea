using System;
using System.Collections.Generic;
using Entities;
using Entities.Player;
using UnityEditor.UIElements;
using UnityEngine;
using Object = UnityEngine.Object;

public class WeaponController : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private StatManager statManager;
    private WeaponRender weaponRender;

    public event Action weaponFired; 
    public bool canShootThisFrame { get; private set; }

    private float fireCooldownTimer = 0f;

    void Awake()
    {
       inventoryManager = GetComponentInParent<InventoryManager>();
       statManager = GetComponentInParent<StatManager>();
       weaponRender = GetComponentInParent<WeaponRender>();
    }

    void Update()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        
        UpdateSwitchingWeaponEvent(scrollValue);

        canShootThisFrame = IsCooldownEnabledForShoot();
        WeaponShoot();
    }
    
    private void WeaponShoot()
    {
        if (canShootThisFrame)
        {
            Vector3 startPos = weaponRender.GetBarrelTransform.position;
            Vector3 endPos = weaponRender.GetBarrelTransform.right;

            float range = (float) inventoryManager.GetCurrentWeapon().range;
            float recoil = (float)inventoryManager.GetCurrentWeapon().recoil;
            float damage = inventoryManager.GetCurrentWeapon().damageMultiplier * statManager.GetDamage();
            
            weaponFired?.Invoke();

            for (int i = 0; i < inventoryManager.GetCurrentWeapon().bulletsPerShoot; i++)
            {
                ProjectileFactory.ShootProjectile(inventoryManager.GetCurrentWeapon().shootType, startPos, endPos, range, recoil, damage);
            }
        }
    }

    private bool IsCooldownEnabledForShoot()
    {
        if (inventoryManager == null || statManager == null) return false;
        
        fireCooldownTimer += Time.deltaTime;

        bool isLPMPressed = Input.GetMouseButton(0);

        float fireRateWeapon = inventoryManager.GetCurrentWeapon().fireRateMultiplier;
        float attackSpeedPlayer = statManager.GetAttackSpeed();

        float timeBeetweenShots = 1f / (fireRateWeapon * attackSpeedPlayer);
        
        if (isLPMPressed && fireCooldownTimer >= timeBeetweenShots)
        {
            fireCooldownTimer = 0f;
            return true;
        }
        
        return false;
    }
    
    private void UpdateSwitchingWeaponEvent(float scrollValue)
    {
        if (scrollValue > 0)
        {
            inventoryManager.SwitchWeapon(1);
        }
        else if (scrollValue < 0)
        {
            inventoryManager.SwitchWeapon(-1);
        }
    }
}