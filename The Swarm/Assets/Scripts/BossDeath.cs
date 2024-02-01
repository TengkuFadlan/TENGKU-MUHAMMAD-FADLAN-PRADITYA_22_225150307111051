using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public ParticleSystem particleEmitter;
    public GameObject winScreen;
    public AudioClip deathAudioClip;

    BaseHealth baseHealth;
    GameObject player;
    Camera mainCamera;

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
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (player.GetComponent<BaseHealth>().dead)
            return;

        particleEmitter.Play();
        GetComponent<AudioSource>().PlayOneShot(deathAudioClip);

        Destroy(player.GetComponent<PlayerMovement>());
        Destroy(player.GetComponent<PlayerCamera>());
        Destroy(player.GetComponent<PlayerGun>());
        Destroy(player.GetComponent<PlayerHeal>());
        Destroy(player.GetComponent<PlayerDeath>());

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

        winScreen.SetActive(true);
    }
}
