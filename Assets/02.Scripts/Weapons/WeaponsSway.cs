using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSway : MonoBehaviour
{
    float m_SwayAmount = 0.1f;        //변수 생성 : 한번당 움직일 범위
    float m_SmoothAmount = 6f;      //변수 생성 :
    float m_MaxAmount = 0.6f;         //변수 생성 : 최대 범위

    Vector3 m_DefaultPosition;          //내 기본 크로스헤어 포지션
    // Start is called before the first frame update
    void Start()
    {
        m_DefaultPosition = transform.localPosition;            //부모포지션의 포지션 값을 넣어둠.
    }

    // Update is called once per frame
    void Update()
    {
        float PositionX = -Input.GetAxis("Mouse X") * m_SwayAmount; //마우스 움직이면 총이 그만큼 반대방향으로 약간 이동
        float PositionY = -Input.GetAxis("Mouse Y") * m_SwayAmount;
        float RotationX = -Input.GetAxis("Mouse X") * m_SwayAmount;
        float RotationY = -Input.GetAxis("Mouse Y") * m_SwayAmount * 1.5f;      //위 바라볼 때 값이 작아보여서 조금 더 값을 줌.

        Mathf.Clamp(PositionX, -m_MaxAmount, m_MaxAmount);          //최소 범위와 최대범위를 지정해서 내 화면에서 총이 안사라지게 해준다.
        Mathf.Clamp(PositionY, -m_MaxAmount, m_MaxAmount);
        Mathf.Clamp(RotationX, -m_MaxAmount, m_MaxAmount);
        Mathf.Clamp(RotationY, -m_MaxAmount, m_MaxAmount);

        Vector3 SwayPosition = new Vector3(PositionX, PositionY, 0);            //이동한 값을 계속 저장 시켜줌.
        Quaternion SwayRotation = new Quaternion(RotationX, RotationY, 0, 1);

        //한템포 늦게 내 마우스 방향으로 조금 늦게 따라오는 기능 완성
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_DefaultPosition + SwayPosition, m_SmoothAmount * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, SwayRotation, m_SmoothAmount * Time.deltaTime);
    }
}
