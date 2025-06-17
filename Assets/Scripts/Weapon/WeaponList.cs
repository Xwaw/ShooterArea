using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{ 
    [SerializeField] private List<WeaponData> weaponList = new();
    public List<WeaponData> Weapons => weaponList;
}
