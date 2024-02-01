using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpitProjectile : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bug"))
            return;
        if (collision.gameObject.CompareTag("BugSpit"))
            return;
        if (collision.gameObject.CompareTag("PlayerBullet"))
            return;

        if (collision.gameObject.TryGetComponent(out BaseHealth baseHealth))
        {
            baseHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
