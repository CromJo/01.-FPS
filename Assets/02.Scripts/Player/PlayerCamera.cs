using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] float m_Sensitivity = 250f;      //내 마우스 감도값
	float m_RotationX = 0f;     //내 마우스 감도 값
	float m_RotationY = 0f;

	private void Update()
	{
		MouseMove();
	}

	void MouseMove()
	{
		float x = Input.GetAxis("Mouse X");					//인풋매니저값
		float y = Input.GetAxis("Mouse Y");					//인풋매니저값
		m_RotationX += x * m_Sensitivity * Time.deltaTime;	//인풋매니저에서 받아온 값과 감도값을 더하여
		m_RotationY += y * m_Sensitivity * Time.deltaTime;	//인풋매니저에서 받아온 값과 감도값을 더하여

		if(m_RotationY > 89)		//30도가 넘어가면 고정
		{
			m_RotationY = 89;
		}
		else if(m_RotationY < -89)	//-30도가 넘어가면 고정
		{
			m_RotationY = -89;
		}
		transform.eulerAngles = new Vector3(-m_RotationY, m_RotationX, 0f); //회전함수로 x값을 움직이면 y가 움직이고, y가 움직이면 x가 움직이기 때문에 순서를 바꾸었다.

	}

}
