using System.Collections;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour
{
    public float zigzagDuration = 3f;
    public float zigzagFrequency = 5f; // Adjust this value to change the zigzag frequency
    public float zigzagAmplitude = 0.5f; // Adjust this value to change the zigzag amplitude
    public float BulletSpeed;

    private Rigidbody2D rb;
    private Vector2 initialDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        initialDirection = transform.right;
        // Add initial force to bullet
        rb.velocity = initialDirection * BulletSpeed;
        StartCoroutine(ZigZag());
    }

    private IEnumerator ZigZag()
    {
        float elapsedTime = 0f;

        while (elapsedTime < zigzagDuration)
        {
            float yOffset = Mathf.Sin(elapsedTime * zigzagFrequency) * zigzagAmplitude; // Adjust amplitude for zigzag
            Vector2 perpendicularDirection = new Vector2(-initialDirection.y, initialDirection.x); // Perpendicular to the initial direction
            Vector2 zigzagVelocity = initialDirection * BulletSpeed + perpendicularDirection * yOffset;
            rb.velocity = zigzagVelocity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = initialDirection * BulletSpeed; // Maintain straight line after zigzag duration
    }
}
