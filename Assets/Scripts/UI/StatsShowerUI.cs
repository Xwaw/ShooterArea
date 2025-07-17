using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

public class StatsShowerUI : MonoBehaviour
{
    [SerializeField] private StatManager statsManager;

    [SerializeField] private GameObject strValueObject;
    [SerializeField] private GameObject atValueObject;
    [SerializeField] private GameObject defValueObject;
    
    private TextMeshProUGUI strengthTextValue;
    private TextMeshProUGUI attackSpeedTextValue;
    private TextMeshProUGUI defenceTextValue;

    void Start()
    {
        strengthTextValue = strValueObject.GetComponentInChildren<TextMeshProUGUI>();
        attackSpeedTextValue = atValueObject.GetComponentInChildren<TextMeshProUGUI>();
        defenceTextValue = defValueObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        strengthTextValue.text = statsManager.Damage.ToString();
        attackSpeedTextValue.text = statsManager.AttackSpeed.ToString();
        defenceTextValue.text = statsManager.Defence.ToString();
    }
}
