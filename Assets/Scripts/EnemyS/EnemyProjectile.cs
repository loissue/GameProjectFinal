using UnityEngine;

namespace EnemyS
{
    public class EnemyProjectile : EnemyDamage
    {
        [SerializeField] private float speed;
        [SerializeField] private float resetTime;
        private float lifetime;
        private Animator anim;
        private BoxCollider2D coll;
        
        [Header("Burn Effect")]
        [SerializeField] private float burnDuration = 3f;
        [SerializeField] private float burnDamagePerSecond = 10f;

        private bool hit;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            coll = GetComponent<BoxCollider2D>();
        }

        public void ActivateProjectile()
        {
            hit = false;
            lifetime = 0;
            gameObject.SetActive(true);
            coll.enabled = true;
        }

        private void Update()
        {
            if (hit) return;
            float movementSpeed = speed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);

            lifetime += Time.deltaTime;
            if (lifetime > resetTime)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
            {
                hit = true;
                base.OnTriggerEnter2D(collision); //Execute logic from parent script first
                coll.enabled = false;
                
                if (collision.gameObject.tag == "Player")
                {
                    collision.GetComponent<Health>().ApplyBurnEffect(burnDuration, burnDamagePerSecond);
                }

                if (anim != null)
                    anim.SetTrigger("explode"); //When the object is a fireball explode it
                else
                    gameObject.SetActive(false); //When this hits any object deactivate arrow
            }
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}