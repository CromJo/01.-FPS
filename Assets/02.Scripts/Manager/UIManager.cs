using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    private static UIManager m_Instance;
    public static UIManager u_Instance
    {
        //매크로함수
        get
        {
            if(m_Instance == null)  //아무것도 없으면 UIManager찾기
            {
                m_Instance = FindObjectOfType<UIManager>();
			}
            return m_Instance;      //있으면 그냥 반환 
		}
	}

    [SerializeField] Text m_HPText;
    [SerializeField] Text m_TimeText;
    [SerializeField] Text m_AmmoText;
    [SerializeField] GameObject m_GameOver;
    [SerializeField] GameObject m_CrossHair;
    [SerializeField] Image m_CrossHairHidden;
    [SerializeField] Button m_ReStart;
    [SerializeField] Image m_PlayerHit;
    PlayerLivingEvent m_PlayerLivingEvent;

    [SerializeField] GameObject m_Player;
    
    float m_Sec = 0;
    float m_Min = 3;
    float m_Timer;

    float m_BloodTime = 1.5f;
    float m_BloodHealthTime;
    private void Start()
    {
        //m_ReStart.onClick.AddListener(ReStart);
        m_Timer = m_Sec;
        m_PlayerLivingEvent = GameObject.FindWithTag("Player").GetComponent<PlayerLivingEvent>();
    }

    private void Update()
    {

        //m_BloodHealthTime = Time.deltaTime;
        //if (m_BloodHealthTime <= m_BloodTime)
        //{
        //    PlayerHitImage(m_PlayerHit);
        //}

    }

    public void UpdateAmmoText(int ammo, int remainammo)
    {
        m_AmmoText.text = ammo + " / " + remainammo;
	}

    public void UpdateHPText(int hp)
    {
        m_HPText.text = hp.ToString();
    }

    public void UpdateTimeText()
    {
        m_Sec -= Time.deltaTime;
        if(m_Sec <= 0)
        {
            m_Min--;
            m_Sec = 60;
            if(m_Sec == 60)
            {
                m_Sec = 59;
			}
        }
        m_TimeText.text = m_Min + " : " + Mathf.RoundToInt(m_Sec);

        if(m_Min < 0)
        {
            m_PlayerLivingEvent.isDeaded = true;
            m_PlayerLivingEvent.Dead();
        }
    }

    public void UpdateCrossHairText(int hour, int min)
    {
        //m_CrossHair = hour + " : " + min;
    }

    public void UpdateText(bool active)
    {
        //m_GameOver.
    }

    public void GameOverActive(bool active)
    {
        m_GameOver.SetActive(active);
        m_Player.GetComponent<Weapons>().enabled = false;
        m_Player.GetComponent<PlayerMove>().enabled = false;
        GetComponent<MouseManager>().enabled = false;
        //m_Player.active = false;

        ReStart();
    }

    public void ReStart()
    {
        if(m_GameOver == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            
            }
        }
    }

    public void  PlayerHitImage()
    {
        Color color = m_PlayerHit.GetComponent<Image>().color;

        //color.a += 0.1f;
        color.a += (float)m_PlayerLivingEvent.m_StartHP / 2000;
        m_PlayerHit.GetComponent<Image>().color = color;
    }

    public void PlayerHealthImage(int health)
    {
        Color color = m_PlayerHit.GetComponent<Image>().color;

        //color.a += 0.1f;
        color.a -= ((float)m_PlayerLivingEvent.m_StartHP / health) / 2;

        if(color.a <= 0)                    //알파값 최소치 설정
        {
            color.a = 0;    
		} 
        m_PlayerHit.GetComponent<Image>().color = color;
    }
}
