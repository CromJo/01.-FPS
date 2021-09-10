using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingObjectPooling : MonoBehaviour
{
    public static CasingObjectPooling Instance;     
    [SerializeField] GameObject m_Casing;           //게임오브젝트를 생성해줄 놈 구하기
    Queue<GameObject> m_Casings;                     //여러개 만들거임!
    //GameObject[] gameObjects = new GameObject[100];
    [SerializeField] Transform m_CasingPos;                          //위치 지정

    //[SerializeField] int m_PoolingCount;
            
    Weapons m_Weapons;
    private void Start()
    {
        Instance = this;        //인스탄스는 자신
        m_Weapons = GameObject.FindWithTag("Player").GetComponent<Weapons>();
        m_Casings = new Queue<GameObject>();
        //for(int i = 0; i < m_Casings.Count; i++)
        //{
        //    GameObject obj = Instantiate(m_Casing, m_CasingPos.position, Quaternion.identity);
        //    obj.SetActive(false);                   //비활성화
        //    m_Casings.Enqueue(obj);                     //리스트에 추가
        //}

        Initialize(30);          //N개 초기화 및 생성
    }


	private void Initialize(int initCount)          //초기화
    {
        for (int i = 0; i < initCount; i++)          //매개변수 값만큼 실행
        {
            m_Casings.Enqueue(CreateNewObject());        //리스트에 추가
        }
    }
    
    //생성
    private GameObject CreateNewObject()
    {
        GameObject casing = Instantiate(m_Casing, m_CasingPos); //자료형에 맞게 생성
        casing.gameObject.SetActive(false);             //우선 비활성화
        casing.transform.SetParent(transform);          //
        return casing;
    }


    public static GameObject GetObject()
    {
        if (Instance.m_Casings.Count > 0)                       //큐의 크기 만큼
        {
            var obj = Instance.m_Casings.Dequeue();             //큐에서 삭제해주고
            //obj.transform.SetParent(null);                      //사용된 아이템을 부모로부터 나오게 해주고
            obj.gameObject.SetActive(true);                    //활성화 시켜준다
            return obj;                                         //그리고 반환
        }
        else                                                    //큐배열이 0보다 더 적을 경우
        {
            var newObj = Instance.CreateNewObject();            //새로 생성
            newObj.gameObject.SetActive(true);                  //활성화시켜주고
            //newObj.transform.SetParent(null);                   //부모로부터 나오게 하고
            return newObj;                                      //반환
        }
        
    }
    
    public static void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);                   //활성화
        //obj.gameObject.transform.position = 
        Instance.m_Casings.Enqueue(obj);                   //큐배열에 다시 집어넣음
        
        //if (obj.)
        //{
        //    obj.Heal();
        //} 
    }
}
