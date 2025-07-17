using System.Collections;
using System.Collections.Generic;
using Entities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthEntityManager healthManager;
    private Slider healthSlider;
    private TextMeshProUGUI healthText;
    [SerializeField] private Image healthFill;
    
    void Awake()
    {
        healthSlider = GetComponent<Slider>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        healthManager.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        float currentHP = healthManager.CurrentHealth;
        healthSlider.value = currentHP;
        healthText.text = (int)currentHP + "%";

        UpdateColorHealthBar(currentHP, healthManager.MaxHealth);
    }

    private void UpdateColorHealthBar(float health, int maxHealth)
    {
        float normalizedHealth = Mathf.Clamp01(health / maxHealth);
        Color targetColor;

        if (normalizedHealth > 0.5f)
        {
            float t = (normalizedHealth - 0.5f) * 2f; 
            targetColor = Color.Lerp(Color.yellow, Color.green, t);
        }
        else
        {
            float t = normalizedHealth * 2f;
            targetColor = Color.Lerp(Color.red, Color.yellow, t);
        }

        healthFill.color = targetColor;
    }
}
