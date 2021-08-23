using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] float m_Sensitivity = 250f;      //�� ���콺 ������
	float m_RotationX = 0f;     //�� ���콺 ���� ��
	float m_RotationY = 0f;

	private void Update()
	{
		MouseMove();
	}

	void MouseMove()
	{
		float x = Input.GetAxis("Mouse X");					//��ǲ�Ŵ�����
		float y = Input.GetAxis("Mouse Y");					//��ǲ�Ŵ�����
		m_RotationX += x * m_Sensitivity * Time.deltaTime;	//��ǲ�Ŵ������� �޾ƿ� ���� �������� ���Ͽ�
		m_RotationY += y * m_Sensitivity * Time.deltaTime;	//��ǲ�Ŵ������� �޾ƿ� ���� �������� ���Ͽ�

		if(m_RotationY > 89)		//30���� �Ѿ�� ����
		{
			m_RotationY = 89;
		}
		else if(m_RotationY < -89)	//-30���� �Ѿ�� ����
		{
			m_RotationY = -89;
		}
		transform.eulerAngles = new Vector3(-m_RotationY, m_RotationX, 0f); //ȸ���Լ��� x���� �����̸� y�� �����̰�, y�� �����̸� x�� �����̱� ������ ������ �ٲپ���.

	}

}
