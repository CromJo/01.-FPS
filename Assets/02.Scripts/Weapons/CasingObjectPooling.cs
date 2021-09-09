using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingObjectPooling : MonoBehaviour
{
    public static CasingObjectPooling Instance;     
    [SerializeField] GameObject m_Casing;           //게임오브젝트를 생성해줄 놈 구하기
    List<GameObject> m_Casings;                     //여러개 만들거임!          
    Transform m_CasingPos;                          //위치 지정

    [SerializeField] int m_PoolingCount;
            
    Weapons m_Weapons;
    private void Start()
    {
        Instance = this;        //인스탄스는 자신

        m_Casings = new List<GameObject>();
        //for(int i = 0; i < m_Casings.Count; i++)
        //{
        //    GameObject obj = Instantiate(m_Casing, m_CasingPos.position, Quaternion.identity);
        //    obj.SetActive(false);                   //비활성화
        //    m_Casings.Add(obj);                     //리스트에 추가
        //}

        Initialize(m_Weapons.Bullet);          //N개 초기화 및 생성
    }

    private void Initialize(int initCount)      //
    {
        for (int i = 0; i < initCount; i++)          //매개변수 값만큼 실행
        {
            m_Casings.Add(CreateNewObject());        //리스트에 추가
        }
    }

    //첫시작후 활성화 해주는 함수
    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(m_Casing); //자료형에 맞게 생성
        newObj.gameObject.SetActive(false);             //우선 비활성화
        newObj.transform.SetParent(transform);          //
        return newObj;
    }
    
    
    public static GameObject GetObject()
    {
        if (Instance.m_Casings.Count > 0)     //큐배열(Initialize(갯수)만큼은
        {
            //GameObject obj = Instance.m_Casings.Remove(obj);  //큐배열에서 내보내고
            //obj.transform.SetParent(null);                      //사용된 아이템을 부모로부터 나오게 해주고
            //obj.gameObject.SetActive(false);                    //비활성화 시켜준다
            //return obj;                                         //그리고 반환
            return null;
        }
        else                                                    //큐배열이 0보다 더 적을 경우
        {
            GameObject newObj = Instance.CreateNewObject();            //새로 생성
            newObj.gameObject.SetActive(true);                  //활성화시켜주고
            newObj.transform.SetParent(null);                   //부모로부터 나오게 하고
            return newObj;                                      //반환
        }
    }
    
    public static void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);                    //활성화
        obj.transform.SetParent(Instance.transform);       //제자리로 위치변경
        Instance.m_Casings.Add(obj);         //큐배열에 다시 집어넣음
        //if (obj.)
        //{
        //    obj.Heal();
        //} 
    }
}
