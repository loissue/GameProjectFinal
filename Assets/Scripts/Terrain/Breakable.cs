using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private int hitCount = 0; // To keep track of the number of hits
    [SerializeField]
    int fallhit = 5;
    [SerializeField]    
    int delhit = 20;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object has the tag "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitCount++;
            Debug.Log("Hit by Bullet: " + hitCount);

            if (hitCount == fallhit)
            {
                // Add Rigidbody2D on first hit
                if (gameObject.GetComponent<Rigidbody2D>() == null)
                {
                    gameObject.AddComponent<Rigidbody2D>();
                    Debug.Log("Rigidbody2D added.");
                }
            }
            else if (hitCount == delhit)
            {
                // Destroy the game object on second hit
                Destroy(gameObject);
                Debug.Log("Object destroyed.");
            }
        }
    }
}
