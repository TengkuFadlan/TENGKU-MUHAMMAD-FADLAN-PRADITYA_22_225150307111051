using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBite : MonoBehaviour
{
    public float biteCooldown = 1;
    public float biteDamage = 25;
    public AudioClip biteAudioClip;

    float lastBiteTime = 0;

    BaseHealth baseHealth;
    AudioSource bugAudioSource;

    void Awake()
    {
        bugAudioSource = GetComponent<AudioSource>();
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
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        if (Time.time <= lastBiteTime + biteCooldown)
            return;

        if (collision.gameObject.TryGetComponent(out BaseHealth playerBaseHealth))
        {
            playerBaseHealth.TakeDamage(biteDamage);
            bugAudioSource.PlayOneShot(biteAudioClip);
            lastBiteTime = Time.time;
        }
    }
}
