using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Scriptable_Objects_Data.DataStores;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponObject
{
    public WeaponObject(WeaponData weaponData, int reserveAmmo)
    {
        GetWeaponData = weaponData;
        MagazineAmmo = reserveAmmo >= weaponData.maxBullets ?  weaponData.maxBullets : reserveAmmo;
        ReserveAmmo = reserveAmmo >= weaponData.maxBullets ? reserveAmmo : 0;
    }

    public WeaponData GetWeaponData { get; }
    public int MagazineAmmo { get; private set; }
    public int ReserveAmmo { get; private set; }

    public void TakeReserveAmmo(int value)
    {
        Math.Min(value, ReserveAmmo);
    }

    public void TakeCurrentAmmo(int value)
    {
        MagazineAmmo -= value;
    }

    public void SetAmmo(int value)
    {
        MagazineAmmo = value;

        ReloadAmmo();
    }

    public void ReloadAmmo()
    {
        int magazineCapacity = GetWeaponData.maxBullets;

        if (MagazineAmmo > magazineCapacity)
        {
            MagazineAmmo = magazineCapacity;
            return;
        }

        if (MagazineAmmo >= magazineCapacity || ReserveAmmo <= 0)
            return;

        int bulletsNeeded = magazineCapacity - MagazineAmmo;
        int bulletsToAdd = Mathf.Min(bulletsNeeded, ReserveAmmo);

        MagazineAmmo += bulletsToAdd;
        ReserveAmmo -= bulletsToAdd;
    }
}
