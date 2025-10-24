using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//태그랑 상태변수를 만들어 처리 
public class Jump2 : MonoBehaviour
{
    //레이캐스트로...
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float groundRadius = 1f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer; //감지할 레이어

    Rigidbody2D rb;
    bool isGrounded; //땅인지 확인

    float horizontalInput; //방향키 입력 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //점프요청
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
        // groundCheck 미지정 시 종료
        if (groundCheck == null) { return; }

        // (위치, 반경) 땅 감지 원 / 초록=착지, 빨강=공중
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        // (위치, 반경, 레이어)로 충돌 감지
        var hit = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // 디버그용 빨강색 설정
        Gizmos.color = Color.red;

        // (중심, 크기) 감지된 콜라이더 경계 표시
        if (hit != null) Gizmos.DrawWireCube(hit.bounds.center, hit.bounds.size);

    }
}