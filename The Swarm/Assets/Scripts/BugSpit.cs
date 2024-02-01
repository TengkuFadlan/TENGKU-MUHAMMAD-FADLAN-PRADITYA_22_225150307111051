using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpit : MonoBehaviour
{
    public float spitRateOfFire = 60f;
    public float spitDamage = 10f;
    public float spitRange = 15f;
    public float spitSpeed = 5f;
    public float spitLifetime = 2f;
    public GameObject spitProjectile;

    float lastSpitTime = 0f;

    GameObject player;
    BaseHealth baseHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void OnEnable()
    {
        baseHealth = gameObject.GetComponent<BaseHealth>();
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

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < spitRange)
        {
            if (Time.time - lastSpitTime > 60f / spitRateOfFire)
            {
                lastSpitTime = Time.time;

                GameObject spit = Instantiate(spitProjectile, transform.position, Quaternion.identity);

                BugSpitProjectile bugSpitProjectile = spit.GetComponent<BugSpitProjectile>();
                bugSpitProjectile.damage = spitDamage;

                Vector3 direction3 = player.transform.position - transform.position;
                Vector2 direction = new Vector2(direction3.x, direction3.y);

                Rigidbody2D spitRB = spit.GetComponent<Rigidbody2D>();
                spitRB.velocity = direction.normalized * spitSpeed;

                Destroy(spit, spitLifetime);
            }
        }
    }
}
