using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEvent
{
    readonly int m_AnimeHashKeyState = Animator.StringToHash("State");
    readonly int m_AnimeHashKeySpeedRatio = Animator.StringToHash("SpeedRatio");
    readonly int m_AnimeHashKeyTrigger = Animator.StringToHash("New Trigger");

    //타겟 추적
    [SerializeField] LayerMask m_Target;            //받은 값의 물리, 그래픽 처리 적용
    [SerializeField] LivingEvent m_TargetEntity;    //받아온 값만을 추적하기 위해 사용 
    NavMeshAgent m_PathFinder;     //경로 추적하기 위해 사용
    [SerializeField] float m_TargetDistance = 0f;
    Transform m_TargetTransform;

    [SerializeField] ParticleSystem m_HitEffect;    //맞을때 나올 효과
    [SerializeField] AudioClip m_DeathSound;        //죽는 소리
    [SerializeField] AudioClip m_HitSound;          //맞을때 나는 소리

    Animator m_EnemyAnimator;  //나 자신의 애니메이션
    AudioSource m_EnemyAudioSource;
    [SerializeField] Renderer m_EnemyRenderer;  //렌더러 컴포넌트
    [SerializeField] Animator m_Animator;

    //공격
    [SerializeField] int m_Atk = 10;
    bool isAttack;
    [SerializeField] float m_AttackDistance = 3f;           //공격 사거리
    BoxCollider m_MeleeArea;
    //피격
    int m_Hit;
    public int Atk { get { return m_Atk; } set { m_Atk = value; } }
    public int Hit { get { return m_Hit; } set { m_Hit = value; } }

    //[SerializeField] int m_StartHP;
    //int m_LiveHP;

    //

    //bool isDead = false;
    private bool hasTarget      //프로퍼티화
    {
        get
        {

            float distance = Vector3.Distance(m_TargetTransform.position, this.transform.position);

            
            //타겟이 있고, 타겟이 죽은 상태가 아니라면
            if (m_TargetEntity != null && !m_TargetEntity.isDead && !isDead)
            {   //공격사거리안에 들어오면
                if (distance <= m_AttackDistance)
                {
                    //공격 실행
                    ChangeState(State.Fire);

                }
                //타겟이 일정 범위안에 들어오면
                else if (distance <= m_TargetDistance)
                {
                    //달리기 실행
                    ChangeState(State.Run);
                    m_PathFinder.SetDestination(m_TargetEntity.transform.position);     //새로운 경로(추적대상(플레이어))로 설정
                }
                //다른 특수한 상황은
                else
                {
                    //대기 실행
                    ChangeState(State.Idle);
                }
                return true;
            }
            return false;
        }
    }

    public enum State
    {
        Idle = 0,
        Run,
        Fire,
        HeadShotDead,
        BodyShotDead
    }

    State m_State = State.Idle;

    // Start is called before the first frame update
    void Awake()
    {
        m_PathFinder = GetComponent<NavMeshAgent>();
        m_EnemyAnimator = GetComponent<Animator>();
        m_EnemyAudioSource = GetComponent<AudioSource>();
        m_TargetTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        OnEnable();
        m_MeleeArea = GameObject.FindWithTag("Enemy").GetComponent<BoxCollider>();
        //m_EnemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    protected override void OnEnable()
    {
        m_StartHP = 50;
        m_LiveHP = m_StartHP;
        isDead = false;
    }
    //public void Setup(int newhp, int newdamage, float newspeed)
    //{
    //    m_StartHP = newhp;          //시작 HP
    //    LiveHP = newhp;             //현재 HP
    //    m_Atk = newdamage;       //현재 공격력
    //    m_PathFinder.speed = newspeed;  //현재 이동속도
    //
    //}

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        
        //ChangeState(State.Idle);
    }

    private IEnumerator UpdatePath()
    {
        //죽을때까지 무한루프
        while(!this.isDead)
        {
            if (hasTarget)       //타겟이 죽지않고 살아있는 상태라면 실행
            {
                //ChangeState(State.Run);
                //m_PathFinder.isStopped = false;     //추적 진행
            
                //m_PathFinder.SetDestination(m_TargetEntity.transform.position);     //새로운 경로(추적대상(플레이어))로 설정
                //단 이방법은 대규모로 할시 과부하가 매우 심하다. NavMeshPath에 있는 SetPath()를 이용하면 이러한 과부하를 줄일 수 있다.
                
               
            
            }
            else                //타겟이 죽거나 없는 상태라면
            {
                //ChangeState(State.Idle);
                m_PathFinder.isStopped = true;      //추적 중단
                Collider[] PathFinderColliders = Physics.OverlapSphere(transform.position, 200f, m_Target); //내 반경(스크립트를 받는 객체)에서 200범위 안에있는 m_Target을 알아냄
                Debug.Log("PathFinderColliders.Length : " + PathFinderColliders.Length);
                for(int i = 0; i<PathFinderColliders.Length; i++)       //패스파인더 배열길이(생성된 수만큼) 반복문이 실행
                {
                    LivingEvent livingEntity = PathFinderColliders[i].GetComponent<LivingEvent>(); //컴포넌트 가지고 오기
            
                    if(livingEntity != null && !livingEntity.isDead)      //컴포넌트가 존재하고, LivingEvent가 살아있다면
                    {
                        m_TargetEntity = livingEntity;                  //livingEntity를 타겟으로 설정
            
                        break;                                          //반복문 종료
                    }
                }
            }
            yield return new WaitForSeconds(0.25f); //0.25초마다 한번씩 실행 (0.25초 동안 다른 곳에 있는 코드를 실행)
           
        }
    }
    
    public override void OnDamage(int damage)
    {
        m_LiveHP -= damage;           //매개변수로 들어온 값만큼 내 HP를 깎아준다.

        if (m_LiveHP <= 0 && !isDead)  //0보다 낮은데 죽지 않은 상태라면
        {
            Dead();                 //데드 함수 실행
        }
    }

    public override void Dead()
    {
        ChangeState(State.BodyShotDead);
        base.Dead();
    }
    private void OnTriggerStay(Collider other)
    {
        
    }

    public void ChangeState(State state)
    {
        if (m_State == state || isDead)                                 //현재 상태와 같은 상태가 들어온다면
            return;                                                     //함수종료

        m_State = state;                                                //잘 들어와졌으면 매개변수로 받은 값으로 바꿔주고
        m_Animator.SetInteger(m_AnimeHashKeyState, (int)m_State);       //애니메이터 파라미터 값을 정수형으로 변환해주고

        switch (m_State)                                                //상태들 중에서
        {
            case State.Idle:                                            //대기상태일 경우
                Debug.Log("상태: 아이들");
                m_PathFinder.Stop();
                break;
            case State.Run:                                             //추격상태일 경우
                Debug.Log("상태: 뛰어가기");
                m_PathFinder.destination = m_TargetTransform.position;  //타겟위치 확인
                m_PathFinder.Resume();
                break;
            case State.Fire:                                            //공격 상태일 경우
                Debug.Log("상태: 공격");
                StartCoroutine(Attack());                               //코루틴 함수 실행
                break;
            case State.HeadShotDead:
                break;
            
            case State.BodyShotDead:
                break;
        }

    }

    bool IsState(State state)               //현재상태함수
    {
        return m_State == state;            //현재상태로 만들어줌
    }

    IEnumerator Attack()
    {
        isAttack = true;
        PlayerLivingEvent player = GameObject.FindWithTag("Player").GetComponent<PlayerLivingEvent>();
        yield return new WaitForSeconds(0.2f);
        Debug.Log("플레이어를 공격했다");
        player.m_LiveHP -= m_Atk;
        player.StartCoroutine(player.OnDMG());
        UIManager.u_Instance.UpdateHPText(player.m_LiveHP);
        UIManager.u_Instance.PlayerHitImage();
        yield return new WaitForSeconds(1f);
        //m_MeleeArea.enabled = false;
        isAttack = false;
        ChangeState(State.Idle);
    }
}
