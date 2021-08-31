using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed = 8f;
    [SerializeField] float m_Gravity = -10f;
    [SerializeField] float m_JumpPower = 3f;
    float m_VelocityY = 0;

    bool m_isJumping = false;       //���� ������

    CharacterController m_CC;

	private void Start()
	{
        m_CC = GetComponent<CharacterController>();
	}

	private void Update()
	{
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if(m_CC.collisionFlags == CollisionFlags.Below)             //���� ĳ������Ʈ�ѷ��� ���� ��Ҵٸ�
        {
            if(m_isJumping)                                         //���� ���̴ٸ�
            {
                m_isJumping = false;                                //���������� �ʰ� ����
                m_VelocityY = 0;
			}
		}

        if(Input.GetButtonDown("Jump") && !m_isJumping)
        {
            Jump();
		}

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);    //����ī�޶� �������� ������ ��ȯ�Ѵ�. (���콺�� ������ ������ ����ȴ�)

        m_VelocityY += m_Gravity * Time.deltaTime;              //���ν�Ƽ�� �߷°��� �ְ�
        dir.y = m_VelocityY;                                    //���ν�Ƽ�� �÷��̾�.y���� �־ �߷� ����.

        //transform.position += dir * m_MoveSpeed * Time.deltaTime;

        m_CC.Move(dir * m_MoveSpeed * Time.deltaTime);
	}

    void Jump()
    {
        if (m_isJumping)
            return;

        m_isJumping = true;
        m_VelocityY = m_JumpPower;
    }


	/*
    [SerializeField] float m_PlayerSpeed = 5f;
    [SerializeField] float m_Gravity = -20f;
    [SerializeField] float m_JumpPower = 5f;
    float m_VelocityY = 0;
    
    bool m_Jump = true;
    bool isJump = false;
    
    Weapons m_State;
    
    //CharacterController m_CC;
    //Rigidbody m_Rigidbody;
    CharacterController m_CC;
    GameObject m_Grounds;
    private void Start()
    {
        m_CC = gameObject.GetComponent<CharacterController>();
        //m_Rigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        m_Grounds = GameObject.FindGameObjectWithTag("Grounds");
    }
    
    
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //m_Jump = Input.GetButtonDown("Jump");
    
    
        //�̵� ����
        Vector3 dir = new Vector3(h, 0f, v).normalized;     
        dir = dir.normalized;
        dir = Camera.main.transform.TransformDirection(dir);        //�ٶ󺸴� �������� ��ȯ
        
        m_VelocityY += m_Gravity * Time.deltaTime;              // -20�� ���� ������Ʈ ������ �־��ش�
        dir.y = m_VelocityY;                                    // �÷��̾�.y��ǥ���� �־��ش�. ������Ʈ���� �Ʒ��� �÷��̾�� ��������.
        
        m_CC.Move(dir * m_PlayerSpeed * Time.deltaTime);        //������� �̵��ӵ��� �°� �̵��Ѵ�.
    
    
    }
    
    
    void Jump()
    {
        //if(m_Rigidbody.collisionFlags == CollisionFlags.Below)
        //{
        //    m_PlayerJumpCount = 0;
        //    m_VelocityY = 0;
        //}
    
        //���� �ڵ�
        if (m_Jump && !isJump)      //������ Ȱ��ȭ �Ǿ��ְ� ���������� ������
        {
            
            isJump = true;
            //m_Rigidbody.AddForce(Vector3.up * m_JumpPower * Time.deltaTime, ForceMode.Impulse);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag != "Grounds")
        //{
        //    isJump = false;
        //}
    }
    */



	//[SerializeField] float m_Speed = 5f;
	//[SerializeField] float m_RunSpeed = 10f;
	//[SerializeField] float m_Jump = 5;
	//[SerializeField] float m_Gravity = 20f;
	//[SerializeField] float m_LookSpeed = 2f;
	//[SerializeField] float m_LookXLimit = 89f;
	//
	//[SerializeField] Camera m_PlayerCamera;
	//CharacterController m_CharacterController;
	//Vector3 m_MoveDirection = Vector3.zero;
	//float m_RotationX = 0;
	//
	//[SerializeField] bool m_isMove = true;
	//
	//private void Start()
	//{
	//    m_CharacterController = GetComponent<CharacterController>();
	//}
	//
	//private void Update()
	//{
	//    Vector3 forward = transform.TransformDirection(Vector3.forward);
	//    Vector3 right = transform.TransformDirection(Vector3.right);
	//
	//    float curSpeedX = m_isMove ? (Input.GetKey(KeyCode.LeftShift) ? m_RunSpeed : m_Speed) * Input.GetAxis("Vertical") : 0;
	//    float curSpeedY = m_isMove ? (Input.GetKey(KeyCode.LeftShift) ? m_RunSpeed : m_Speed) * Input.GetAxis("Horizontal") : 0;
	//    float movementDirectionY = m_MoveDirection.y;
	//    m_MoveDirection = (forward * curSpeedX) + (right * curSpeedY);
	//
	//    //����Ű(�����̽�)�� ������, ������ �� �ִ� ���°�, ĳ������Ʈ�ѷ��� ���� ����ִٸ�
	//    if(Input.GetButton("Jump") && m_isMove && m_CharacterController.isGrounded)
	//    {
	//        m_MoveDirection.y = m_Jump;         //Y�� ���
	//    }
	//    else                //�ƴ϶��
	//    {
	//        m_MoveDirection.y = movementDirectionY; //�������д�.
	//    }
	//
	//    //���� ���� ĳ������Ʈ�ѷ��� ���ٸ�
	//    if(!m_CharacterController.isGrounded)
	//    {
	//        m_MoveDirection.y -= m_Gravity * Time.deltaTime; //�߷� ����
	//    }
	//
	//    m_CharacterController.Move(m_MoveDirection * Time.deltaTime);
	//
	//    if(m_isMove)
	//    {
	//        m_RotationX += -Input.GetAxis("Mouse Y") * m_LookSpeed;
	//        m_RotationX = Mathf.Clamp(m_RotationX, -m_LookXLimit, m_LookXLimit);
	//        m_PlayerCamera.transform.localRotation = Quaternion.Euler(m_RotationX, 0, 0);
	//        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_LookSpeed , 0);
	//    }
	//
	//}
}
