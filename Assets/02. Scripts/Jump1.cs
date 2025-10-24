using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//�±׶� ���º����� ����� ó�� 
public class Jump1 : MonoBehaviour
{
    //����ĳ��Ʈ��...
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float MoveSpeed = 5f;

    Rigidbody2D rb;

    [SerializeField] Transform groundCheck;
    [SerializeField] float rayLength = 20f;
    [SerializeField] LayerMask groundLayer; //������ ���̾�

    bool isJumpPressed; // ����Ű ������
    bool isGrounded; //������ Ȯ��

    float horizontalInput; //����Ű �Է� 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //������û
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
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
    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }

        Color raycolor = isGrounded ? Color.green : Color.red;

        Debug.DrawRay(groundCheck.position, Vector2.down * rayLength, raycolor);
    }

}
