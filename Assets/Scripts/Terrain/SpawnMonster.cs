using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    public GameObject monster;
    public float spawnHeightOffset = 1f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has the tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeightOffset;
            Instantiate(monster, spawnPosition, Quaternion.identity);
        }
    }
}
