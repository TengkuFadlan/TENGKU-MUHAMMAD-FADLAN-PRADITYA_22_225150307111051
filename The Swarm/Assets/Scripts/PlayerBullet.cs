using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage = 1f;
    public GameObject quickAudioPrefab;
    public AudioClip hitAudioClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;
        if (collision.gameObject.CompareTag("PlayerBullet"))
            return;
        if (collision.gameObject.CompareTag("BugSpit"))
            return;

        if (collision.gameObject.TryGetComponent(out BaseHealth baseHealth))
        {
            baseHealth.TakeDamage(damage);

            GameObject quickAudio = Instantiate(quickAudioPrefab, transform.position, Quaternion.identity);
            AudioSource quickAudioSource = quickAudio.GetComponent<AudioSource>();
            quickAudioSource.volume = 0.5f;
            quickAudioSource.PlayOneShot(hitAudioClip);
            Destroy(quickAudio, 1f);
        }
        Destroy(gameObject);
    }
}
