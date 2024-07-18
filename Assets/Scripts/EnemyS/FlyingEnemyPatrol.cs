using UnityEngine;

namespace EnemyS
{
    public class FlyingEnemyPatrol : MonoBehaviour
    {
        [Header("Enemy")] 
        [SerializeField] private float patrolDistanceX = 5f; // Horizontal patrol distance
        [SerializeField] private float patrolDistanceY = 2f; // Vertical patrol distance

        
        [Header("Movement parameters")]
        [SerializeField] private float speed;
        [SerializeField] private float moveDuration = 3f; // Duration to move in one direction

        [Header("Idle Behaviour")]
        [SerializeField] private float idleDuration = 1f; // Duration of idle state

        private Vector3 initPosition;
        private bool movingLeft;
        private bool movingDown;
        private float moveTimer;
        private float idleTimer;
        private bool isIdle;

        private Animator anim;

        private void Awake()
        {
            initPosition = gameObject.transform.position;
            anim = gameObject.GetComponent<Animator>();
            SetNewMoveTimer();
        }

        private void OnDisable()
        {
            anim.SetBool("moving", false);
        }

        private void Update()
        {
            if (isIdle)
            {
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    anim.SetBool("moving", true);
                    SetNewMoveTimer();
                    isIdle = false;
                }
            }
            else
            {
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    anim.SetBool("moving", false);
                    ChooseIdleOrReverse();
                }
                else
                {
                    MoveForward();
                }
            }
        }

        private void SetNewMoveTimer()
        {
            moveTimer = moveDuration;
        }

        private void ChooseIdleOrReverse()
        {
            if (Random.value > 0.5f)
            {
                idleTimer = idleDuration;
                isIdle = true;
            }
            else
            {
                ReverseDirection();
                SetNewMoveTimer();
            }
        }

        private void ReverseDirection()
        {
            movingLeft = !movingLeft;
            movingDown = !movingDown;
            // Reverse direction and flip sprite
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        private void MoveForward()
        {
            float directionX = movingLeft ? -1 : 1;
            Vector3 moveDirection = new Vector3(directionX, 0, 0).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
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