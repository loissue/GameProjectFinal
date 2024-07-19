using EnemyS;
using UnityEngine;

namespace Boss
{
    public class BossProjectile : EnemyDamage
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifetime = 5f;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rb.velocity = transform.up * speed;
            Destroy(gameObject, lifetime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                base.OnTriggerEnter2D(collision);
                Destroy(gameObject);
            }
        }
    }
}