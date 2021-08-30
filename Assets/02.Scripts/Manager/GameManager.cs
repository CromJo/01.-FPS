using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;           
    [SerializeField] GameObject m_GameOver;         //게임오버
    [SerializeField] Text m_GameTime;               //게임시간
    [SerializeField] AudioSource m_AudioSource;     
    [SerializeField] AudioClip m_StartGameSound;    

    public bool isGameOver { get; set; }

    public static GameManager g_Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
            }
            return m_Instance;
        }
    }


    private void Awake()
    {
        if(g_Instance != this)
        {
            Destroy(m_GameOver.gameObject);
        }
        m_AudioSource.PlayOneShot(m_StartGameSound);
    }

    private void Start()
    {
        //FindObjectOfType<Player>().onDeath += EndGame;
    }

    private void Update()
    {
        ReStart();
        UIManager.u_Instance.UpdateTimeText();
    }

    public void ReStart()
    {
        if(isGameOver == false)
        {
            return;
        }
        else if(isGameOver == true && Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(0);
        }
    }
}
