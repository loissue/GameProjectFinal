using System.Collections;
using System.Collections.Generic;
using EnemyS;
using UnityEngine;
public class SkeletonEnemy : Enemy 
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private PolygonCollider2D polygonCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
    private Health health;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(polygonCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(polygonCollider.bounds.size.x * range, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(polygonCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(polygonCollider.bounds.size.x * range, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
