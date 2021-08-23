using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEvent
{
    /*[SerializeField] float m_PlayerSpeed = 8f;
    [SerializeField] float m_Gravity = -20f;
    [SerializeField] float m_JumpPower = 5f;
    float m_H;
    float m_V;
    float m_VelocityY;

    bool m_Jump = true;
    bool isJump = false;


    //CharacterController m_CC;
    Vector3 dir;
    Rigidbody m_Rigidbody;
    GameObject m_Grounds;
    private void Start()
    {
        //m_CC = gameObject.GetComponent<CharacterController>();
        m_Rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        m_Grounds = GameObject.FindGameObjectWithTag("Grounds");
    }
    
    private void Update()
    {
        GetInput();
        Move();
        Jump();
    }

    void GetInput()
    {
        m_H = Input.GetAxis("Horizontal");
        m_V = Input.GetAxis("Vertical");
        m_Jump = Input.GetButtonDown("Jump"); 
    }

    void Move()
    {
        dir = new Vector3(m_H, 0f, m_V).normalized;

        dir = Camera.main.transform.TransformDirection(dir);
        transform.position += dir * m_PlayerSpeed * Time.deltaTime;

    }

    void Jump()
    {
        //if(m_Rigidbody.collisionFlags == CollisionFlags.Below)
        //{
        //    m_PlayerJumpCount = 0;
        //    m_VelocityY = 0;
        //}

        //점프 코드
        if (m_Jump && !isJump)      //점프가 활성화 되어있고 점프중이지 않으면
        {
            
            isJump = true;
            m_Rigidbody.AddForce(Vector3.up * m_JumpPower * Time.deltaTime, ForceMode.Impulse);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Grounds")
        {
            isJump = false;
        }
    }
    */
    protected override void OnEnable()
    {
        
    }
}
