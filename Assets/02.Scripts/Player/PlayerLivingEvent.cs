using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLivingEvent : LivingEvent
{
    //[SerializeField] int m_StartHP = 100;
    //[SerializeField] int m_LiveHP;
    //[SerializeField] int 
    [SerializeField] Image m_BloodImage;
    Enemy m_Enemy;

    bool isHit;
    bool isDead;
    public bool isDeaded { get { return isDead; } set { isDead = value; } }
    [SerializeField] GameObject m_GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        OnEnable();
    }

    protected override void OnEnable()
    {
        isDead = false;
        m_StartHP = 100;
        m_LiveHP = m_StartHP;
    }

    // Update is called once per frame
    void Update()
    {
        //대미지 업데이트
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Hit(10);
        }

        //사망 업데이트
        if (m_LiveHP <= 0 || Input.GetKeyDown(KeyCode.P))
        {
            isDead = true;
            Dead();
		}

        OnDamage(1);
    }


    void Hit(int damage)
    {
        if (!Input.GetKeyDown(KeyCode.Return))             //엔터가 아니면 계속 들어오는데 최적화에 문제가 있지 않을까?
            return;

        m_LiveHP -= damage;
        UIManager.u_Instance.UpdateHPText(m_LiveHP);
        UIManager.u_Instance.PlayerHitImage(m_BloodImage);


        
    }
    public override void Dead()
    {
        if (isDead == false)
            return;

        UIManager.u_Instance.GameOverActive(true);
    }

    public override void OnDamage(int damage)
    {
        //m_Enemy.Damage = damage;
        base.OnDamage(damage);
    }

    public override void RecoveryHP(int heal)
    {
        base.RecoveryHP(heal);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "EnemyATK")
        {
            Enemy enemyDamage = other.GetComponent<Enemy>();
            m_LiveHP -= enemyDamage.Damage;
            StartCoroutine(OnDMG());
            UIManager.u_Instance.PlayerHitImage(m_BloodImage);
        }
    }

    IEnumerator OnDMG()
    {
        isHit = true;
        yield return new WaitForSeconds(1f);
    }
}
