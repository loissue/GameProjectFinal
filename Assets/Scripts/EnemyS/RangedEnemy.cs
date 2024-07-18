using System.Collections;
using System.Collections.Generic;
using EnemyS;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Attack Parameters")] [SerializeField]
    private float attackCooldown;

    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;

    [SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private float detectionHeightMultiplier = 1f; // New field for y-axis scaling

    [SerializeField] private PolygonCollider2D polygonCollider2D;

    [Header("Player Layer")] [SerializeField]
    private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    private FlyingEnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<FlyingEnemyPatrol>();
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
                anim.SetTrigger("rangedAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile(FindObjectOfType<PlayerMovement>().transform.position);
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }

        return 0;
    }

    private bool PlayerInSight()
    {
        Vector3 boxCenter = polygonCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(polygonCollider2D.bounds.size.x * range, polygonCollider2D.bounds.size.y * detectionHeightMultiplier, polygonCollider2D.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = polygonCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(polygonCollider2D.bounds.size.x * range, polygonCollider2D.bounds.size.y * detectionHeightMultiplier, polygonCollider2D.bounds.size.z);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}