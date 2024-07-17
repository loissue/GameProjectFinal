using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionDelay = 2f;
    public GameObject explosionEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        // Show explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Add explosion logic here (damage, effects, etc.)

        // Destroy the bomb
        Destroy(gameObject);
    }
}
