using UnityEngine;
namespace EnemyS
{
    public class EnemyPatrol : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private PolygonCollider2D polygonCollider;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance = 1f;

        [Header("Movement parameters")]
        [SerializeField] private float speed;
        private Vector3 initScale;
        private bool movingLeft;

        [Header("Idle Behaviour")]
        private float idleDuration;
        private float idleTimer;

        private Animator anim;

        private void Awake()
        {
            initScale = gameObject.transform.localScale;
            anim = gameObject.GetComponent<Animator>();
        }

        private void OnDisable()
        {
            anim.SetBool("moving", false);
        }

        private void Update()
        {
            if (IsGroundInFront())
            {
                MoveInDirection(movingLeft ? -1 : 1);
            }
            else
            {
                DirectionChange();
            }
        }

        private bool IsGroundInFront()
        {
            Vector2 position = gameObject.transform.position;
            Vector2 direction = movingLeft ? Vector2.left : Vector2.right;
            Vector2 groundCheckPosition = position + new Vector2(direction.x, -groundCheckDistance);


            RaycastHit2D hit = Physics2D.BoxCast(
                groundCheckPosition, 
                new Vector3(polygonCollider.bounds.size.x, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z),
                0,
                direction, 
                groundCheckDistance,
                groundLayer);
            return hit.collider != null;
        }
        
        private void OnDrawGizmos()
        {
            if (polygonCollider == null) return;
            Gizmos.color = Color.red;
            Vector2 position = gameObject.transform.position;
            Vector2 direction = movingLeft ? Vector2.left : Vector2.right;
            Vector2 groundCheckPosition = position + new Vector2(direction.x * groundCheckDistance, -groundCheckDistance);
            Gizmos.DrawWireCube(groundCheckPosition, new Vector3(polygonCollider.bounds.size.x, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z));
        }

        private void DirectionChange()
        {
            anim.SetBool("moving", false);
            idleTimer += Time.deltaTime;

            if (idleTimer > idleDuration)
            {
                movingLeft = !movingLeft;
                idleTimer = 0;
            }
        }

        private void MoveInDirection(int _direction)
        {
            idleTimer = 0;
            anim.SetBool("moving", true);

            // Make enemy face direction
            gameObject.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
                initScale.y, initScale.z);

            // Move in that direction
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime * _direction * speed,
                gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}