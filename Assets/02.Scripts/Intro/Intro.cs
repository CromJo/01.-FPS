using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
	[SerializeField] string m_StartGame;
	[SerializeField] AudioSource m_AudioSource;
	[SerializeField] AudioClip m_IntroBGM;

	private void Start()
	{
		m_AudioSource.PlayOneShot(m_IntroBGM);
	}

	private void Update()
	{
		if(Input.anyKeyDown)	//아무키나 누를경우
		{
			//m_AudioSource.PlayOneShot(m_StartGameSound);
			SceneManager.LoadScene(m_StartGame);
		}
	}

	public void GameStart()
	{
		SceneManager.LoadScene(m_StartGame);
	}
}
