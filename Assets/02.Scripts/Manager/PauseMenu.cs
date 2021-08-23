using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//아직 사용중이진 않음.
public class PauseMenu : MonoBehaviour
{
   
    GameObject m_PauseUI;
    bool m_Paused = false;
    // Start is called before the first frame update
    void Start()
    {
        m_PauseUI.SetActive(false);         //우선 펄스로 열어둔 상태가 아닐 것
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            m_Paused = false;
        }

        if(m_Paused == true)
        {
            m_PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
