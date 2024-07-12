using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManBehavior : MonoBehaviour
{
    public float chaseSpeed = 2f; // Speed of the snowman
    public float stopDistance = 30f; // Distance at which the snowman stops chasing
    public GameObject snowballPrefab; // Snowball prefab
    public float fireRate = 1f; // Rate at which snowballs are fired
    public float snowballSpeed = 10f; // Speed of the snowball

    private Transform player; // Reference to the player
    private Transform firePoint; // Point from where the snowball is fired
    private float nextFireTime = 0f;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Find the firePoint as a child of the snowman
        firePoint = transform.Find("FirePoint");

        // Check if firePoint is assigned
        if (firePoint == null)
        {
            Debug.LogError("FirePoint not found! Make sure the firePoint is a child of the snowman prefab and named 'FirePoint'.");
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found! Make sure the player has the 'Player' tag.");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            // Chase the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

            // Shoot at the player
            if (Time.time >= nextFireTime && firePoint != null)
            {
                ShootSnowball(direction);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void ShootSnowball(Vector2 direction)
    {
        GameObject snowball = Instantiate(snowballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = snowball.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on the snowball prefab!");
        }
        else
        {
            rb.velocity = direction * snowballSpeed;
        }

        Debug.Log("Snowball shot at direction: " + direction);
    }
}
