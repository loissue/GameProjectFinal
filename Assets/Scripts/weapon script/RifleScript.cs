using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScript : MonoBehaviour
{
    public Shoot shoot;  // Reference to the Shoot script

    void Start()
    {
        if (shoot != null)
        {
            float randomInterval = Random.Range(0.001f, 0.9f);  // Generate a random float from 0.001 to 1
            shoot.shootInterval = randomInterval;
            Debug.Log("Random shoot interval set to: " + randomInterval);
        }
        else
        {
            Debug.LogError("Shoot component is not assigned to RifleScript.");
        }
    }

    void Update()
    {
        // You can add additional logic here if needed
    }
}
