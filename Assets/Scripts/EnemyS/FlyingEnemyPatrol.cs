using UnityEngine;

namespace EnemyS
{
    public class FlyingEnemyPatrol : MonoBehaviour
    {
        [Header("Enemy")] [SerializeField] private float patrolDistanceX = 5f; // Horizontal patrol distance
        [SerializeField] private float patrolDistanceY = 2f; // Vertical patrol distance

        [Header("Movement parameters")] [SerializeField]
        private float speed;

        private Vector3 initPosition;
        private Vector3 targetPosition;
        private bool movingLeft;
        private bool movingDown;

        [Header("Idle Behaviour")] [SerializeField]
        private float idleDuration = 1f; // Duration of idle state

        private float idleTimer;

        private Animator anim;

        private void Awake()
        {
            initPosition = gameObject.transform.position;
            targetPosition = initPosition;
            anim = gameObject.GetComponent<Animator>();
        }

        private void OnDisable()
        {
            anim.SetBool("moving", false);
        }

        private void Update()
        {
            if (idleTimer > 0)
            {
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    anim.SetBool("moving", true);
                    SetNewTargetPosition();
                }
            }
            else
            {
                MoveTowardsTarget();
            }
        }

        private void SetNewTargetPosition()
        {
            movingLeft = !movingLeft;
            movingDown = !movingDown;

            float directionX = movingLeft ? -1 : 1;
            float directionY = movingDown ? -1 : 1;

            targetPosition = new Vector3(
                initPosition.x + directionX * patrolDistanceX,
                initPosition.y + directionY * patrolDistanceY,
                initPosition.z
            );
            
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

            anim.SetBool("moving", true);
        }

        private void MoveTowardsTarget()
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // If reached target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                anim.SetBool("moving", false);
                idleTimer = idleDuration;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(initPosition, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(initPosition.x + patrolDistanceX, initPosition.y + patrolDistanceY, initPosition.z), new Vector3(0.2f, 0.2f, 0.2f));
            Gizmos.DrawWireCube(new Vector3(initPosition.x - patrolDistanceX, initPosition.y - patrolDistanceY, initPosition.z), new Vector3(0.2f, 0.2f, 0.2f));
        }
        
    }
}