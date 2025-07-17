using System;
using System.Collections.Generic;
using Scriptable_Objects_Data.DataStores;
using UnityEngine;

namespace Player
{
    public class InventoryManager : MonoBehaviour
    {   
        public WeaponDataBase weapons;
        
        private List<WeaponObject> _inventoryWeapons = new();
        private int _currentWeaponIndex = 0;

        public event Action<WeaponData> OnWeaponChanged;
        
        public WeaponObject GetCurrentWeapon()
        {
            if (_inventoryWeapons.Count == 0)
            {
                return null;
            }
            return _inventoryWeapons[_currentWeaponIndex];
        }

        public void SwitchWeapon(int direction)
        {
            _currentWeaponIndex = (_currentWeaponIndex + direction + _inventoryWeapons.Count) % _inventoryWeapons.Count;
            OnWeaponChanged?.Invoke(GetCurrentWeapon().GetWeaponData);
        }

        public void AddWeapon(WeaponData weaponData, int reserveAmmo)
        {
            _inventoryWeapons.Add(new WeaponObject(weaponData, reserveAmmo));
        }

        void Start()
        {
            for (int i = 0; i < weapons.allWeapons.Count; i++)
            {
                AddWeapon(weapons.allWeapons[i] , 350);
            }
        }
    }
}