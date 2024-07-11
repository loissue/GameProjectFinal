using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health health; // Sức khỏe của đối tượng Enemy

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Health health = GetComponent<Health>();
    }

    // Update is called once per frame
    public float getHealth()
    {
        return health.currentHealth;
    }
    // public float moveSpeed = 2f; // Tốc độ di chuyển của enemy
    //public Transform leftBoundary; // Điểm biên trái
    //public Transform rightBoundary; // Điểm biên phải
    // public float minTimeToChangeDirection = 3f; // Thời gian tối thiểu để đổi hướng
    // public float maxTimeToChangeDirection = 15f;
    // private bool movingRight = true;
    // private float timeToChangeDirection; // Thời gian để đổi hướng tiếp theo
    // private float timer;
    // void Update()
    // {
    //     Move();
    //     HandleDirectionChange();
    // }

    //void Move()
    //{
    //    if (movingRight)
    //    {
    //        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

    //        if (transform.position.x >= rightBoundary.position.x)
    //        {
    //            movingRight = false;
    //            Flip();
    //        }
    //    }
    //    else
    //    {
    //        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    //        if (transform.position.x <= leftBoundary.position.x)
    //        {
    //            movingRight = true;
    //            Flip();
    //        }
    //    }
    //}
    // void Move()
    // {
    //     if (movingRight)
    //     {
    //         transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    //     }
    //     else
    //     {
    //         transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    //     }
    // }
    //
    // void HandleDirectionChange()
    // {
    //     // Tăng giá trị bộ đếm thời gian theo thời gian trôi qua
    //     timer += Time.deltaTime;
    //
    //     // Nếu bộ đếm thời gian lớn hơn hoặc bằng thời gian cần để đổi hướng
    //     if (timer >= timeToChangeDirection)
    //     {
    //         // Đổi hướng di chuyển
    //         movingRight = !movingRight;
    //         Flip();
    //
    //         // Thiết lập lại bộ đếm thời gian và thời gian để đổi hướng tiếp theo
    //         timer = 0f;
    //         SetRandomTimeToChangeDirection();
    //     }
    // }
    //
    // void SetRandomTimeToChangeDirection()
    // {
    //     // Thiết lập thời gian ngẫu nhiên để đổi hướng trong khoảng từ minTimeToChangeDirection đến maxTimeToChangeDirection
    //     timeToChangeDirection = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);
    // }
    // void Flip()
    // {
    //     Vector3 localScale = transform.localScale;
    //     localScale.x *= -1;
    //     transform.localScale = localScale;
    // }

}
    
