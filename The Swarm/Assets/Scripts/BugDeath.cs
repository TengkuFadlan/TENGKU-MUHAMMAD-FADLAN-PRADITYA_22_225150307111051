using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugDeath : MonoBehaviour
{
    public ParticleSystem particleEmitter;

    BaseHealth baseHealth;

    void OnEnable()
    {
        baseHealth = GetComponent<BaseHealth>();
        baseHealth.OnDeath += HandleDeath;
    }

    void OnDisable()
    {
        baseHealth.OnDeath -= HandleDeath;
    }

    void HandleDeath()
    {
        particleEmitter.Play();

        Destroy(gameObject, particleEmitter.main.duration);
    }
}
