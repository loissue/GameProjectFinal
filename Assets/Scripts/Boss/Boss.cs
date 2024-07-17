using System.Collections;
using UnityEngine;

namespace Boss
{
    public class Boss : MonoBehaviour
    {
        [Header("Attack Settings")] [SerializeField]
        private float timeBetweenAttacks;

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;

        [Header("Spray Attack Settings")] [SerializeField]
        private int sprayProjectilesCount;

        [SerializeField] private float sprayAngle;

        [Header("180 Degree Attack Settings")] [SerializeField]
        private int arcProjectilesCount;

        [SerializeField] private float arcAngle;

        private bool isAttacking;

        private void Start()
        {
            StartCoroutine(AttackRoutine());
        }

        private IEnumerator AttackRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenAttacks);

                int attackType = Random.Range(0, 2);
                switch (attackType)
                {
                    case 0:
                        StartCoroutine(SprayAttack());
                        break;
                    case 1:
                        StartCoroutine(ArcAttack());
                        break;
                }
            }
        }

        private IEnumerator SprayAttack()
        {
            float angleStep = sprayAngle / (sprayProjectilesCount - 1);
            float startAngle = -sprayAngle / 2;

            for (int i = 0; i < sprayProjectilesCount; i++)
            {
                float angle = startAngle + i * angleStep;
                ShootProjectile(angle);
            }

            yield return null; // Adjust timing if needed
        }

        private IEnumerator ArcAttack()
        {
            float angleStep = arcAngle / (arcProjectilesCount - 1);
            float startAngle = -arcAngle / 2;

            for (int i = 0; i < arcProjectilesCount; i++)
            {
                float angle = startAngle + i * angleStep;
                ShootProjectile(angle);
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
    }
}