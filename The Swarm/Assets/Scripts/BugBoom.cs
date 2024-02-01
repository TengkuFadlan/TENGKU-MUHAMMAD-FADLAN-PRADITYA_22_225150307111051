using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBoom : MonoBehaviour
{
    public float boomDamage = 75;
    public GameObject boomGameObject;

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
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.gameObject.TryGetComponent(out BaseHealth playerBaseHealth))
        {
            playerBaseHealth.TakeDamage(boomDamage);

            GameObject boom = Instantiate(boomGameObject, transform.position, Quaternion.identity);
            boom.GetComponent<ParticleSystem>().Play();
            boom.GetComponent<AudioSource>().Play();
            Destroy(boom, 1f);
            
            Destroy(gameObject);
        }
    }
}
