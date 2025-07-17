using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Player;
using TMPro;
using UnityEngine;

public class WeaponShowerUI : MonoBehaviour
{
    private TextMeshProUGUI ammoValueText;
    
    [SerializeField] private InventoryManager inventory;
    
    void Awake()
    {
        ammoValueText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        ammoValueText.text = "Ammo: " + inventory.GetCurrentWeapon().ReserveAmmo + " / " + inventory.GetCurrentWeapon().MagazineAmmo;
    }
}
