using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] GameObject m_Heal;
    [SerializeField] GameObject m_Ammo;
    [SerializeField] Transform[] m_ItemSpawnTransform;
    Weapons m_Player;
    UIManager m_PlayerAmmo;
    PlayerLivingEvent m_PlayerHealth;

    [SerializeField] int m_Kind;// 0은 아머, 1은 헬스
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player").GetComponent<Weapons>();
        m_PlayerAmmo = GameObject.FindWithTag("UI").GetComponent<UIManager>();
        m_PlayerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerLivingEvent>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "Player")
        {

            if(m_Kind == 0)
            {
                m_Player.BulletAmmoPack += 30;                                                          //채워주고
                UIManager.u_Instance.UpdateAmmoText(m_Player.CurrentAmmo, m_Player.BulletAmmoPack);     //화면 출력 업데이트해주고
                ObjectPool.ReturnObject(this);                                                          //획득한 상자 비활성화
            }
            else                                                                                        //
            {
                m_PlayerHealth.m_LiveHP += 25;                                                          //체력 회복해주고
                if (m_PlayerHealth.m_LiveHP >= 100)                                                     //100이상 넘어갈려하면
                {
                    m_PlayerHealth.m_LiveHP = m_PlayerHealth.m_StartHP;                                 //100넘어가지 않게 해주고
                    UIManager.u_Instance.UpdateHPText(m_PlayerHealth.m_LiveHP);                         //화면 출력 업데이트해주고
                }
                UIManager.u_Instance.UpdateHPText(m_PlayerHealth.m_LiveHP);                             //100안넘어가면 회복한만큼 화면출력 업데이트해주고
                UIManager.u_Instance.PlayerHealthImage(400);
                ObjectPool.ReturnObject(this);                                                          //획득한 상자 비활성화
            }
            /*for(int i = 1; i < 3; i++)
            {
                if (GameObject.Find("AmmoSpawner" + i) == null)
                    continue;


                if(this == GameObject.Find("AmmoSpawner" + i).GetComponent<ItemUse>())
                {
                    m_Player.BulletAmmoPack += 30;
                    UIManager.u_Instance.UpdateAmmoText(m_Player.CurrentAmmo, m_Player.BulletAmmoPack);
                    ObjectPool.ReturnObject(this);
                }
              
                if (GameObject.Find("HealthSpawner" + i) == null)
                    continue;
                
                if (this == GameObject.Find("HealthSpawner" + i).GetComponent<ItemUse>())
                {
                    m_PlayerHealth.m_LiveHP += 25;
                    if(m_PlayerHealth.m_LiveHP >= 100)
                    {
                        m_PlayerHealth.m_LiveHP = m_PlayerHealth.m_StartHP;
                        UIManager.u_Instance.UpdateHPText(m_PlayerHealth.m_LiveHP);
					}
                    UIManager.u_Instance.UpdateHPText(m_PlayerHealth.m_LiveHP);
                    ObjectPool.ReturnObject(this);
                }
			}*/
        }
    }
}
