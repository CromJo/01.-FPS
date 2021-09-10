using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;                  //싱글톤

    [SerializeField] GameObject m_PoolingObjectPrefabs;     //게임오브젝트를 지정
    Queue<ItemUse> m_PoolingObjectQueue = new Queue<ItemUse>();     //큐로 배열비슷한거 만들어줌

    private void Awake()    
    {
        Instance = this;        //인스탄스는 자신
        //Initialize(2);          //2개 초기화 및 생성
    }

    private void Initialize(int initCount)      //
    {
        for(int i=0; i<initCount; i++)          //매개변수 값만큼 실행
        {
            m_PoolingObjectQueue.Enqueue(CreateNewObject());        //큐에 오브젝트를 넣어준다.
        }
    }

    //첫시작후 활성화 해주는 함수
    private ItemUse CreateNewObject()
    {
        var newObj = Instantiate(m_PoolingObjectPrefabs).GetComponent<ItemUse>(); //자료형에 맞게 생성
        newObj.gameObject.SetActive(true);             //우선 활성화
        newObj.transform.SetParent(transform);          //
        return newObj;
    }


    public static ItemUse GetObject()
    {
        if(Instance.m_PoolingObjectQueue.Count > 0)     //큐배열(Initialize(갯수)만큼은
        {
            var obj = Instance.m_PoolingObjectQueue.Dequeue();  //큐배열에서 내보내고
            obj.transform.SetParent(null);                      //사용된 아이템을 부모로부터 나오게 해주고
            obj.gameObject.SetActive(false);                    //비활성화 시켜준다
            return obj;                                         //그리고 반환
        }
        else                                                    //큐배열이 0보다 더 적을 경우
        {
            var newObj = Instance.CreateNewObject();            //새로 생성
            newObj.gameObject.SetActive(true);                  //활성화시켜주고
            newObj.transform.SetParent(null);                   //부모로부터 나오게 하고
            return newObj;                                      //반환
        }
    }

    public static void ReturnObject(ItemUse obj)
    {
        obj.gameObject.SetActive(false);                    //다시 비활성화
        obj.transform.SetParent(Instance.transform);       //제자리로 위치변경
        Instance.m_PoolingObjectQueue.Enqueue(obj);         //큐배열에 다시 집어넣음
        //if (obj.)
        //{
        //    obj.Heal();
        //} 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
