using UnityEngine;
using UnityEngine.UI;
public class Weapons : MonoBehaviour
{

    readonly int m_AnimeHashKeyState = Animator.StringToHash("State");
    readonly int m_AnimeHashKeySpeedRatio = Animator.StringToHash("SpeedRatio");
    readonly int m_AnimeHashKeyTrigger = Animator.StringToHash("New Trigger");

    //총 기본특성
    [SerializeField] string m_WeaponName;
    [SerializeField] int m_BulletAmmo;               //장전될 아모
    [SerializeField] int m_BulletAmmoPack = 90;      //가지고있는 총 아모
    [SerializeField] int m_CurrentAmmo = 30;         //장전된 아모의 현재 갯수
    [SerializeField] float m_Range;
    [SerializeField] float m_FireRate;
    [SerializeField] Transform m_BulletObjectPoint;     //위치지정
    [SerializeField] GameObject m_BulletObject;         //불릿객체 생성

    //총의 공격속도
    //private bool m_BulletAmmoPackActive;
    private float m_FireTimer = 0.8f;

    //줌기능
    [SerializeField] Vector3 m_AimPosition;
    private Vector3 m_DefaultPosition;
    private bool isAiming;
    private float m_AimSpeed = 5f;

    //반동
    [SerializeField] Transform m_Recoiltransform;
    [SerializeField] Vector3 m_RecoilReturn;
    [SerializeField] float m_RecoilAmount;
    [SerializeField] float m_Accuracy;

    [SerializeField] Transform m_ShotPoint;
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip m_ShotSound;
    [SerializeField] AudioClip m_ReLoadSound;
    [SerializeField] Animator m_Animator;
    [SerializeField] Text m_BulletText;
    [SerializeField] ParticleSystem m_Muzzle;
    [SerializeField] GameObject m_BulletHole;

    //[SerializeField] LayerMask m_Target;
    //[SerializeField] LivingEvent m_TargetEntity;
    [SerializeField] int m_LayerMask;
    [SerializeField] int m_EnemyLayerMask;

    [SerializeField] Transform m_AKM;

    //공격력
    [SerializeField] int m_AKMDamage = 37;

    bool m_isReload =false;                     //장전 중인 상태인지
    public bool isReLoading {get { return m_isReload; } set { m_isReload = value; } }

    public enum State
    {
        Idle = 0,
        Run,
        Fire,
        Reload,
        Draw
    }
    State m_State = State.Idle;

    public int Bullet { get { return m_BulletAmmo; } set { m_BulletAmmo = value; } }
    public State Status { get { return m_State; } set { m_State = value; } }

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentAmmo = m_BulletAmmo;       //처음부터 현재아모는 장전된채로 시작하게 만들어줌
        m_BulletText.text = m_CurrentAmmo + " / " + m_BulletAmmoPack;           //초기값 표시
        m_Animator = GameObject.Find("AKM_rig").GetComponent<Animator>();
        m_DefaultPosition = transform.localPosition;
        m_LayerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy"));
        m_EnemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        m_AKM = GameObject.Find("AKM_rig").GetComponent<Transform>();
        //m_RecoilReturn = GameObject.Find("AKM_rig").GetComponent<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        //AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(1);
        //isReLoading = info.IsName("Reload");

        //이거 문젠가 싶어서 다시 풀어서 밑에 다시 쓰고

        //R키를 누르거나, 현재탄창이 0발일때 마우스왼쪽버튼을 눌렀고, 내가 가지고 있는 탄창이 0발보다 많고 
        //if ((Input.GetKeyDown(KeyCode.R) || (Input.GetMouseButtonDown(0) && m_CurrentAmmo == 0)) && m_BulletAmmo >= m_CurrentAmmo && m_CurrentAmmo != m_BulletAmmo + 1 && m_isReload == false)
        //{
        //    ChangeState(State.Reload);
        //    m_isReload = true;
        //    ReloadSound();
        //    
        //    //Reload();                   //함수 실행
        //}

        //버튼을 눌렀고
        if(Input.GetKeyDown(KeyCode.R) || (Input.GetMouseButtonDown(0) && m_CurrentAmmo == 0))
        {
            //가지고있는 총아모가 0보다 많을 경우
            if (m_BulletAmmoPack > 0)   
            {
                //내 모든총탄이 현재총알보다 많고
                if (m_BulletAmmo >= m_CurrentAmmo)
                {
                    //내 현재총알이 31발이 아니라면
                    if (m_CurrentAmmo != m_BulletAmmo + 1)
                    {
                        //내가 지금 장전상태가 아니라면
                        if (m_isReload == false)
                        {
                            ChangeState(State.Reload);
                            ReloadSound();
                        }
                    }

                }
            }
			
		}
        //else if (Input.GetKeyUp(KeyCode.R))
        //{
        //    ChangeState(State.Idle);
        //}

        if (m_FireTimer < m_FireRate)        //총의 공격속도
        {
            m_FireTimer += Time.deltaTime;
        }
        
        //발사버튼누르고 있고, 장전중이지 않다면
        if (Input.GetButton("Fire1") && m_isReload == false)
        {
            if(m_CurrentAmmo > 0)
            {
                ChangeState(State.Fire);
                //함수실행
                Fire();
            }
        }
        RecoilReturn();
        //우클릭 눌르고 있으면 줌 때면 줌 취소
        AimZoom();
    }

    void Fire()
    {
        if(m_FireTimer < m_FireRate)    //내 총의 발사속도 제한된 것보다 입력이 더 빨리 되었고, 장전중인 상태라면
        {    
            return;                     //안쏴짐
        }
        
        Debug.Log("총 쏴짐");             
        RaycastHit hit;                 //레이저가 표시되는데 출력은 안됨

        //히트스캔방식과 비슷한 레이캐스트 총알 구멍을 생성한다.
        if (Physics.Raycast(m_ShotPoint.position, m_ShotPoint.transform.forward + Random.onUnitSphere * m_Accuracy, out hit, m_Range, m_LayerMask))
        {
            Debug.Log("맞았다!");
            //(레이캐스트가 충돌한 지점에 m_GunFire게임오브젝트를 생성)
            GameObject BulletHole = Instantiate(m_BulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(BulletHole, 10f);              //총알구멍을 10초 후 삭제
        }
        if (Physics.Raycast(m_ShotPoint.position, m_ShotPoint.transform.forward, out hit, m_Range, m_EnemyLayerMask))
        {
            Debug.Log("좀비가 맞았다!");
            Enemy enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
            enemy.Hit = m_AKMDamage;
        }
        m_CurrentAmmo--;    //쏠 때마다 하나씩 제거
        m_FireTimer = 0.0f;   //
        m_AudioSource.PlayOneShot(m_ShotSound);       //총 사운드출력
        
        //Play() : 소리 한번씩 재생
        //PlayOneShot() : 소리 재생된 것을 끊지않고 다음 소리 재생
        
        m_BulletText.text = m_CurrentAmmo + " / " + m_BulletAmmoPack;       //탄창 얼마나 남았는지 Ui표시
        ChangeState(State.Idle);
        m_Animator.CrossFadeInFixedTime("Fire", 0.01f);    //특정 동작에 지정한 시간만큼 애니메이션 동작
        m_Muzzle.Play();            //이펙트 재생


        BulletSpawn();
        ReCoil();
    }

    //반동 함수
    void ReCoil()
    {       
        Vector3 recoilVector = new Vector3(Random.Range(-m_RecoilReturn.x, m_RecoilReturn.x), m_RecoilReturn.y, m_RecoilReturn.z);//상하좌우에 대한 반동 값 지정
        Vector3 recoilCamVector = new Vector3(-recoilVector.y * 300f, recoilVector.x * 200f, 0);                                //상하좌우에 대한 반동 작동
    
        m_AKM.localPosition = Vector3.Lerp(m_AKM.localPosition, m_AKM.localPosition + recoilVector, m_RecoilAmount / 2f);       //총의 포지션을 변경
        m_Recoiltransform.localRotation = Quaternion.Slerp(m_Recoiltransform.localRotation, Quaternion.Euler(m_Recoiltransform.localEulerAngles + recoilCamVector), m_RecoilAmount);
    
    }

    void RecoilReturn()
    {
        m_Recoiltransform.localRotation = Quaternion.Slerp(m_Recoiltransform.localRotation, Quaternion.identity, Time.deltaTime * 2f);
    }

    //줌기능
    void AimZoom()
    {
        if (Input.GetButton("Fire2") && !isReLoading)
        {
            m_AKM.localPosition = Vector3.Lerp(m_AKM.localPosition, m_AimPosition, Time.deltaTime * 8f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40f, Time.deltaTime * 8f);
            isAiming = true;
            //m_Accuracy = origin
        }
        else
        {
            m_AKM.localPosition = Vector3.Lerp(m_AKM.localPosition, m_RecoilReturn, Time.deltaTime * 5f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 8f);
            isAiming = false;
        }
    }

    //탄피 생성
    void BulletSpawn()
    {
        Quaternion randomQuaternion = new Quaternion(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f), 1);                       //탄이 튀는 값 
        GameObject casing = Instantiate(m_BulletObject, m_BulletObjectPoint);                                                                       //탄생성
        casing.transform.localRotation = randomQuaternion;                                                                                          //탄이 튈때
        casing.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(50f, 100f), Random.Range(50f, 100f), Random.Range(-30f, 30f)));  //오른쪽 상단으로 튄다.
        Destroy(casing, 1f);                                                                                                                            //1초뒤 게임오브젝트 삭제
	}
    void ReloadSound()
    {
        //만약 장전중이지 않고, 현재 탄창이, 장전될 탄약이 더크며, 가진 총탄약이 0발보다 많다면
        //if (!m_isReload && m_CurrentAmmo <= m_BulletAmmo && m_BulletAmmoPack > 0)    //장전중이고, 현재탄창이 채워질 탄창보다 적으며, 가지고있는 탄창이 0보다 많다면 
        //{
            m_Animator.CrossFadeInFixedTime("Reload", 0.01f);
            m_AudioSource.PlayOneShot(m_ReLoadSound);
            //m_isReload = true;
        //}
    }

    public void Reload()
    {
        if (m_BulletAmmoPack <= 0)
        {
            Debug.Log("탄창 부족");
            return;
        }
        
        Debug.Log("리로도");

        int supplementBullet = m_BulletAmmo - m_CurrentAmmo;//장전해줄 총알을 계산
        int addedBulletCount = 0;                           //최종적으로 탄창에 들어갈 수 있는 총알 수

        if (supplementBullet >= m_BulletAmmoPack)             //장전될 총알이 총보유한 탄창보다 많을 경우
        {
            addedBulletCount = m_BulletAmmoPack;            //총보유한 탄창을 장전할 탄창에 넣어준다.
        }
        else if (m_CurrentAmmo > 0 && addedBulletCount <= m_BulletAmmoPack)                          //그런데 현재 탄창이 0발보다 많이 가지고 있다면
                                                                                                     //여분이 충분할 경우
        {
            addedBulletCount = supplementBullet + 1;            //1발더 추가해서 장전한다.        

        }
        else
        {
            addedBulletCount = supplementBullet;            //장전해줄 총알을 장전할 탄창에 넣어준다.
        }
        m_BulletAmmoPack -= addedBulletCount;               //총 보유한 탄창을 들어간 총알만큼 빼고
        m_CurrentAmmo += addedBulletCount;                  //들어간 총알만큼 현재 총알을 더한다.

        m_BulletText.text = m_CurrentAmmo + " / " + m_BulletAmmoPack;
        ChangeState(State.Idle);
        //m_isReload = false;                                 //장전중인 상태를 끝낸다.
    }

    public void ChangeState(State state)
    {
        if (m_State == state)        //현재 상태와 같은 상태가 들어온다면
            return;                 //함수종료

        m_State = state;
        m_Animator.SetInteger(m_AnimeHashKeyState, (int)m_State);

        switch(m_State)
        {
            case State.Idle:
                Debug.Log("상태: 아이들");
                break;
            case State.Run:
                Debug.Log("상태: 뛰어가기");
                break;
            case State.Fire:
                Debug.Log("상태: 발사");
                break;
            case State.Reload:
                Debug.Log("상태: 리로도");

                break;
            case State.Draw:
                break;
        }
	}

    bool IsState(State state)               //현재상태함수
    {
        return m_State == state;            //현재상태로 만들어줌
	}
}
