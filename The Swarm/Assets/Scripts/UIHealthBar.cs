using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public GameObject healthBar;

    Slider healthSlider;
    BaseHealth baseHealth;

    void Awake()
    {
        healthSlider = healthBar.GetComponent<Slider>();
    }

    void OnEnable()
    {
        baseHealth = GetComponent<BaseHealth>();
        baseHealth.OnHealthChanged += OnHealthChanged;
        healthBar.SetActive(true);
        OnHealthChanged();
    }

    void OnDisable()
    {
        baseHealth.OnHealthChanged -= OnHealthChanged;

        if (!healthBar)
            return;
            
        healthBar.SetActive(false);
    }

    void OnHealthChanged()
    {
        healthSlider.value = baseHealth.health/baseHealth.maxHealth;
    }
}
