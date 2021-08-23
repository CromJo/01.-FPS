using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSway : MonoBehaviour
{
    float m_SwayAmount = 0.1f;        //���� ���� : �ѹ��� ������ ����
    float m_SmoothAmount = 6f;      //���� ���� :
    float m_MaxAmount = 0.6f;         //���� ���� : �ִ� ����

    Vector3 m_DefaultPosition;          //�� �⺻ ũ�ν���� ������
    // Start is called before the first frame update
    void Start()
    {
        m_DefaultPosition = transform.localPosition;            //�θ��������� ������ ���� �־��.
    }

    // Update is called once per frame
    void Update()
    {
        float PositionX = -Input.GetAxis("Mouse X") * m_SwayAmount; //���콺 �����̸� ���� �׸�ŭ �ݴ�������� �ణ �̵�
        float PositionY = -Input.GetAxis("Mouse Y") * m_SwayAmount;
        float RotationX = -Input.GetAxis("Mouse X") * m_SwayAmount;
        float RotationY = -Input.GetAxis("Mouse Y") * m_SwayAmount * 1.5f;      //�� �ٶ� �� ���� �۾ƺ����� ���� �� ���� ��.

        Mathf.Clamp(PositionX, -m_MaxAmount, m_MaxAmount);          //�ּ� ������ �ִ������ �����ؼ� �� ȭ�鿡�� ���� �Ȼ������ ���ش�.
        Mathf.Clamp(PositionY, -m_MaxAmount, m_MaxAmount);
        Mathf.Clamp(RotationX, -m_MaxAmount, m_MaxAmount);
        Mathf.Clamp(RotationY, -m_MaxAmount, m_MaxAmount);

        Vector3 SwayPosition = new Vector3(PositionX, PositionY, 0);            //�̵��� ���� ��� ���� ������.
        Quaternion SwayRotation = new Quaternion(RotationX, RotationY, 0, 1);

        //������ �ʰ� �� ���콺 �������� ���� �ʰ� ������� ��� �ϼ�
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_DefaultPosition + SwayPosition, m_SmoothAmount * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, SwayRotation, m_SmoothAmount * Time.deltaTime);
    }
}
