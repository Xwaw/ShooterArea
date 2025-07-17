using System.Collections.Generic;
using Scriptable_Objects_Data.DataStores;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Weapon Database")]
public class WeaponDataBase : ScriptableObject
{
    public List<WeaponData> allWeapons;
}
