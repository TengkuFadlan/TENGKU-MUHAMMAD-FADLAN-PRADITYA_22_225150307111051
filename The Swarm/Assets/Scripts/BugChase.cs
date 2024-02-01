using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugChase : MonoBehaviour
{
    public float bugSpeed = 1f;
    public float bugSightRange = 10f;

    GameObject player;
    Rigidbody2D bugRB;
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

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= bugSightRange)
        {
            bugRB.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, bugSpeed * Time.fixedDeltaTime));
            bugRB.MoveRotation(Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg + 90f);
        }
    }
}
