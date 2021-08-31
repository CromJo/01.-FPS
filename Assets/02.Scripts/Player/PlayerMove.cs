using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float m_MoveSpeed = 8f;
    [SerializeField] float m_Gravity = -10f;
    [SerializeField] float m_JumpPower = 3f;
    float m_VelocityY = 0;

    bool m_isJumping = false;       //점프 중인지

    CharacterController m_CC;

	private void Start()
	{
        m_CC = GetComponent<CharacterController>();
	}

	private void Update()
	{
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if(m_CC.collisionFlags == CollisionFlags.Below)             //나의 캐릭터컨트롤러가 땅을 밟았다면
        {
            if(m_isJumping)                                         //점프 중이다면
            {
                m_isJumping = false;                                //점프중이지 않게 변경
                m_VelocityY = 0;
			}
		}

        if(Input.GetButtonDown("Jump") && !m_isJumping)
        {
            Jump();
		}

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);    //메인카메라를 기준으로 방향을 변환한다. (마우스를 움직일 때마다 변경된다)

        m_VelocityY += m_Gravity * Time.deltaTime;              //벨로시티에 중력값을 넣고
        dir.y = m_VelocityY;                                    //벨로시티를 플레이어.y값에 넣어서 중력 적용.

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
    
    
        //이동 방향
        Vector3 dir = new Vector3(h, 0f, v).normalized;     
        dir = dir.normalized;
        dir = Camera.main.transform.TransformDirection(dir);        //바라보는 방향으로 변환
        
        m_VelocityY += m_Gravity * Time.deltaTime;              // -20의 값을 업데이트 때마다 넣어준다
        dir.y = m_VelocityY;                                    // 플레이어.y좌표값에 넣어준다. 업데이트마다 아래로 플레이어는 내려간다.
        
        m_CC.Move(dir * m_PlayerSpeed * Time.deltaTime);        //사용자의 이동속도에 맞게 이동한다.
    
    
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
	//    //점프키(스페이스)를 눌렀고, 움직일 수 있는 상태고, 캐릭터컨트롤러가 땅을 밟고있다면
	//    if(Input.GetButton("Jump") && m_isMove && m_CharacterController.isGrounded)
	//    {
	//        m_MoveDirection.y = m_Jump;         //Y축 상승
	//    }
	//    else                //아니라면
	//    {
	//        m_MoveDirection.y = movementDirectionY; //내버려둔다.
	//    }
	//
	//    //만약 땅에 캐릭터컨트롤러가 없다면
	//    if(!m_CharacterController.isGrounded)
	//    {
	//        m_MoveDirection.y -= m_Gravity * Time.deltaTime; //중력 적용
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
