using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f; // tốc độ di chuyển

    public float jumpForce = 10f;// toc do nhay
    public float speed = 5f;
    bool isGrounded; // Biến kiểm tra xem player có đang đứng trên mặt đất không
   public bool isRevert = false;
    /// </summary>
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Lấy đầu vào từ bàn phím
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        // Thiết lập các parameters của Animator
        animator.SetFloat("Speed", movement.sqrMagnitude);
        


            if (movement.sqrMagnitude > 0)
            {
                animator.SetBool("IsWalking", true);
                if (Input.GetKeyDown(KeyCode.A))
                {
                 animator.SetBool("LeftWalk", true);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                 animator.SetBool("RightWalk", true);
                 }
                
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("LeftWalk", false);
                animator.SetBool("RightWalk", false);

            }
        
        
        // Kiểm tra xem player có đang đứng trên mặt đất không
        isGrounded = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Ground"));
        
        float moveInput = Input.GetAxisRaw("Horizontal");

        float moveVelo = moveInput*movespeed;

        rb.velocity = new Vector2(moveVelo, rb.velocity.y);

        // Xử lý nhảy khi người chơi nhấn phím Space và player đang đứng trên mặt đất
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (!isRevert)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else
            {
                rb.gravityScale *= -1;
            }
        }
    }
}
