using System;
using UnityEngine;

namespace Entities.Player
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private WeaponData[] weapons;
        private int currentWeaponIndex = 0;

        public event Action<WeaponData> OnWeaponChanged; 

        public WeaponData GetCurrentWeapon() => weapons[currentWeaponIndex];

        public void SwitchWeapon(int direction)
        {
            currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
            OnWeaponChanged?.Invoke(GetCurrentWeapon());
        }
    }
}