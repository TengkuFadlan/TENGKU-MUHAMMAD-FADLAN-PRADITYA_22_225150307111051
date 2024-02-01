using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugDash : MonoBehaviour
{
    public float dashCooldown = 1f;
    public float dashForce = 100f;

    float lastDashTime = 0;


    Rigidbody2D bugRB;
    GameObject player;
    BaseHealth baseHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bugRB = GetComponent<Rigidbody2D>();
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

    void Update()
    {
        if (lastDashTime + dashCooldown < Time.time)
        {
            lastDashTime = Time.time;

            Vector3 direction3 = player.transform.position - transform.position;
            Vector2 direction = new Vector2(direction3.x, direction3.y);


            bugRB.AddForce(direction.normalized * dashForce * bugRB.mass);
            bugRB.MoveRotation(Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90f);
        }
    }
}
