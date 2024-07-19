using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f; // tốc độ di chuyển

    public float jumpForce = 10f;// toc do nhay
    bool isGrounded; // Biến kiểm tra xem player có đang đứng trên mặt đất không
    public bool isRevert = false;
    public bool isTeleport = false;
    public bool isEye=false;
    public bool isFreeze=false;
    public float orthographic;
    private bool directionRight = true;
    [SerializeField] private Transform groundCheck;
    public AudioManager audio;
    /// </summary>
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float direction;
    public CinemachineVirtualCamera virtualCamera;
    private float originalSize;
    bool hasPlayed = false;
    bool hasPlayed1 = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
          originalSize = virtualCamera.m_Lens.OrthographicSize;

}

// Update is called once per frame
void Update()
    {
        Time.timeScale = 1.0f;
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 2f, LayerMask.GetMask("Ground"));
        float moveInput = Input.GetAxisRaw("Horizontal");

        float moveVelo = moveInput * movespeed;
        direction = Input.GetAxis("Horizontal");
        animator.SetFloat("move", Mathf.Abs(direction));

        if (isGrounded)
        {
            rb.velocity = new Vector2(moveVelo, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.W)&&isGrounded)
        {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }if(Input.GetKeyUp(KeyCode.W)&&rb.velocity.y > 0)
        {
            rb.velocity=new Vector2(rb.velocity.x,rb.velocity.y*0.5f);
        }   
        if(isTeleport)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("teleport");
                StartCoroutine(TeleportWithDelay());
            }
            
        }
        if (isRevert)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                rb.gravityScale *= -1;
            }
        }
      
        if (isEye)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!hasPlayed)
                {
                    audio.PlaySfx(audio.eyeClip);
                    hasPlayed = true;
                }
                virtualCamera.m_Lens.OrthographicSize = orthographic;
            }
            else 
            {
                virtualCamera.m_Lens.OrthographicSize = originalSize;
                hasPlayed = false;
            }
        }
        if (isFreeze)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
               audio.SetBackgroundSpeed(0.5f);
                Time.timeScale = 0.5f;
            }
            else
            {
                audio.SetBackgroundSpeed(1f);
                Time.timeScale = 1f;
            }

        }
        flip();
    }
    void flip()
    {
        if (directionRight && direction < 0 || !directionRight && direction > 0)
        {
            directionRight = !directionRight;
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
    IEnumerator TeleportWithDelay()
    {
        audio.PlaySfx(audio.teleClip);
        yield return new WaitForSeconds(0.3f);
        if (!directionRight)
        {
            transform.position += Vector3.left * 5f;
        }
        else
        {
            transform.position += Vector3.right * 5f;
        }
        animator.ResetTrigger("teleport");
    }
   
}