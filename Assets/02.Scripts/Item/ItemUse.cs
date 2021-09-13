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

    [SerializeField] int m_Kind;// 0�� �Ƹ�, 1�� �ｺ
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
                m_Player.BulletAmmoPack += 30;                                                          //ä���ְ�
                UIManager.u_Instance.UpdateAmmoText(m_Player.CurrentAmmo, m_Player.BulletAmmoPack);     //ȭ�� ��� ������Ʈ���ְ�
                ObjectPool.ReturnObject(this);                                                          //ȹ���� ���� ��Ȱ��ȭ
            }
            else                                                                                        //
            {
                m_PlayerHealth.m_LiveHP += 25;                                                          //ü�� ȸ�����ְ�
                if (m_PlayerHealth.m_LiveHP >= 100)                                                     //100�̻� �Ѿ���ϸ�
                {
                    m_PlayerHealth.m_LiveHP = m_PlayerHealth.m_StartHP;                                 //100�Ѿ�� �ʰ� ���ְ�
                    UIManager.u_Instance.UpdateHPText(m_PlayerHealth.m_LiveHP);                         //ȭ�� ��� ������Ʈ���ְ�
                }
                UIManager.u_Instance.UpdateHPText(m_PlayerHealth.m_LiveHP);                             //100�ȳѾ�� ȸ���Ѹ�ŭ ȭ����� ������Ʈ���ְ�
                UIManager.u_Instance.PlayerHealthImage(400);
                ObjectPool.ReturnObject(this);                                                          //ȹ���� ���� ��Ȱ��ȭ
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
