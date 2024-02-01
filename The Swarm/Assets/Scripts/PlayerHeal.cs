using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    public float healCooldown = 10f;
    public float healAmount = 50f;
    public GameObject healBar;
    public Slider healSlider;
    public AudioClip healAudioClip;

    float lastHealTime = 0f;

    BaseHealth baseHealth;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        lastHealTime = -healCooldown; // instant start
    }

    void OnEnable()
    {
        healBar.SetActive(true);
    }

    void Start()
    {
        baseHealth = GetComponent<BaseHealth>();
    }

    void OnDisable()
    {
        if (!healBar)
            return;

        healBar.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;
            
        if (Input.GetKeyDown(KeyCode.H) && Time.time >= lastHealTime + healCooldown)
        {
            lastHealTime = Time.time;
            baseHealth.Heal(healAmount);
            audioSource.PlayOneShot(healAudioClip);
        }

        float healBarSize = Math.Clamp((Time.time - lastHealTime) / healCooldown, 0, 1);

        healSlider.value = healBarSize;
    }
}
