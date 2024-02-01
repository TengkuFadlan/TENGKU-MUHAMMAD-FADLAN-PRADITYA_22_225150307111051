using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public bool dead = false;

    public event Action OnDeath;
    public event Action OnHealthChanged;

    public void TakeDamage(float damage)
    {
        if (dead)
            return;

        health = Mathf.Max(health - damage, 0f);
        OnHealthChanged?.Invoke();

        if (health == 0f)
        {
            dead = true;
            OnDeath?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        if (dead)
            return;

        health = Mathf.Min(health + heal, maxHealth);
        OnHealthChanged?.Invoke();
    }
}