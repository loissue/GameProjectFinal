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
    public bool isTeleport = false;
    private bool directionLeft = true;
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
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", movement.sqrMagnitude);
            if (movement.sqrMagnitude > 0)
            {
                animator.SetBool("IsWalking", true);
                if (Input.GetKeyDown(KeyCode.A))
                {
                 animator.SetBool("LeftWalk", true);
                directionLeft = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                 animator.SetBool("RightWalk", true);
                directionLeft = false;
            }
                
            }
            else
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("LeftWalk", false);
                animator.SetBool("RightWalk", false);
            }
        isGrounded = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Ground"));
        float moveInput = Input.GetAxisRaw("Horizontal");
        float moveVelo = moveInput*movespeed;
        rb.velocity = new Vector2(moveVelo, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            if (isTeleport)
            {
                StartCoroutine(TeleportWithDelay());
            }
            else if(isRevert)
            {
                rb.gravityScale *= -1;
            }
            else if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            }
        }
    }
    IEnumerator TeleportWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if (directionLeft)
        {
            transform.position += Vector3.left * 5f;
        }
        else
        {
            transform.position += Vector3.right * 5f;
        }
    }
}
