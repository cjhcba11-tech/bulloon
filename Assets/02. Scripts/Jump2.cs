using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//�±׶� ���º����� ����� ó�� 
public class Jump2 : MonoBehaviour
{
    //����ĳ��Ʈ��...
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float groundRadius = 1f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer; //������ ���̾�

    Rigidbody2D rb;
    bool isGrounded; //������ Ȯ��

    float horizontalInput; //����Ű �Է� 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //������û
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jumping();
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move();

    }

    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);
    }
    void Jumping()
    {

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }
    void GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (hit != null)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }


    }
    void OnDrawGizmos()
    {
        // groundCheck ������ �� ����
        if (groundCheck == null) { return; }

        // (��ġ, �ݰ�) �� ���� �� / �ʷ�=����, ����=����
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        // (��ġ, �ݰ�, ���̾�)�� �浹 ����
        var hit = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // ����׿� ������ ����
        Gizmos.color = Color.red;

        // (�߽�, ũ��) ������ �ݶ��̴� ��� ǥ��
        if (hit != null) Gizmos.DrawWireCube(hit.bounds.center, hit.bounds.size);

    }
}