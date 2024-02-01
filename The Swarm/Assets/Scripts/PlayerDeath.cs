using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public ParticleSystem deathParticle;
    public GameObject gameOverScreen;
    public AudioClip deathAudioClip;
    

    BaseHealth baseHealth;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        Destroy(GetComponent<PlayerCamera>());
        Destroy(GetComponent<PlayerGun>());
        Destroy(GetComponent<PlayerMovement>());
        Destroy(GetComponent<PlayerHeal>());

        deathParticle.Play();
        audioSource.PlayOneShot(deathAudioClip);

        gameOverScreen.SetActive(true);
    }
}
