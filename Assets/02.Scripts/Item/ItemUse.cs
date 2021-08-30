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

    public void Heal()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "Player")
        {
            for(int i = 1; i < 3; i++)
            {
                if(this == GameObject.Find("AmmoSpawner" + i).GetComponent<ItemUse>())
                {
                    m_Player.BulletAmmoPack += 30;
                    m_PlayerAmmo.UpdateAmmoText(m_Player.CurrentAmmo, m_Player.BulletAmmoPack);
                    ObjectPool.ReturnObject(this);
                }
                else if(this == GameObject.Find("HealthSpawner" + i).GetComponent<ItemUse>())
                {
                    m_PlayerHealth.m_LiveHP += 25;
                    if(m_PlayerHealth.m_LiveHP >= 100)
                    {
                        m_PlayerHealth.m_LiveHP = m_PlayerHealth.m_StartHP;
                        m_PlayerAmmo.UpdateHPText(m_PlayerHealth.m_StartHP);
					}
                    m_PlayerAmmo.UpdateHPText(m_PlayerHealth.m_LiveHP);
                    ObjectPool.ReturnObject(this);
                }
			}
        }
    }
}
