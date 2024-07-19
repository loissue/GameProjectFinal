﻿using System.Collections;
using UnityEngine;

namespace Boss
{
    public class BossScript : MonoBehaviour
    {
        [Header("Attack Settings")] [SerializeField]
        private float timeBetweenAttacks;

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        [Header("Downward Fan Attack Settings")] [SerializeField]
        private int downwardProjectilesCount;

        [SerializeField] private float downwardAngle;

        [Header("Spring  Spray Attack Settings")] [SerializeField]
        private int springSprayProjectilesCount;

        [SerializeField] private float springSprayAngle;

        [Header("Animation Explosion")] [SerializeField]
        private GameObject[] explosionPrefabs;

        private bool isAttacking;


        //References
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            isAttacking = true;
            StartCoroutine(AttackRoutine());
        }

        private IEnumerator AttackRoutine()
        {
            while (isAttacking)
            {
                yield return new WaitForSeconds(timeBetweenAttacks);

                var bossHealth = gameObject.GetComponent<Health>();
                if (bossHealth.currentHealth <= bossHealth.startingHealth / 2)
                {
                    downwardProjectilesCount *= 2;
                    springSprayProjectilesCount *= 2;
                }

                int attackType = Random.Range(0, 2);
                switch (attackType)
                {
                    case 0:
                        StartCoroutine(DownwardSprayAttack());
                        Debug.Log("Downward Spray Attack");
                        break;
                    case 1:
                        StartCoroutine(SpringDownwardSprayAttack());
                        Debug.Log("Spring Downward Spray Attack");
                        break;
                }
            }
        }


        private IEnumerator SpringDownwardSprayAttack()
        {
            float angleStep = springSprayAngle / (springSprayProjectilesCount - 1);
            float startAngle = -springSprayAngle / 2;

            for (int i = 0; i < springSprayProjectilesCount; i++)
            {
                yield return new WaitForSeconds(0.2f);
                float angle = startAngle + i * angleStep;
                anim.SetTrigger("attack");
                ShootProjectile(angle - 180); // Adjust angle to shoot downward
            }

            yield return null; // Adjust timing if needed
        }


        private IEnumerator DownwardSprayAttack()
        {
            float angleStep = downwardAngle / (downwardProjectilesCount - 1);
            float startAngle = -downwardAngle / 2;

            for (int i = 0; i < downwardProjectilesCount; i++)
            {
                float angle = startAngle + i * angleStep;
                ShootProjectile(angle - 180); // Adjust angle to shoot downward
            }

            yield return null; // Adjust timing if needed
        }

        private void ShootProjectile(float angle)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            projectile.GetComponent<Rigidbody2D>()
                .AddForce(projectile.transform.up * 10, ForceMode2D.Impulse); // Adjust speed
        }

        public IEnumerator Explode()
        {
            foreach (var explosionPrefab in explosionPrefabs)
            {
                yield return new WaitForSeconds(0.2f);
                explosionPrefab.SetActive(true);
            }

            yield return null; // Adjust timing if needed
        }

        public void OnDeath()
        {
            isAttacking = false;
            StartCoroutine(Explode());
        }
    }
}