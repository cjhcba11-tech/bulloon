using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//태그랑 상태변수를 만들어 처리 
public class Jump : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float MoveSpeed = 5f;

    Rigidbody2D rb;

    bool isJumpPressed; // 점프키 눌렀나
    bool isGrounded; //땅인지 확인

    float horizontalInput; //방향키 입력
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //점프요청
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Jumping();
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);
    }
    void Jumping()
    {
        if (isJumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumpPressed = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
