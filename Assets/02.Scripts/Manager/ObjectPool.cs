using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;                  //�̱���

    [SerializeField] GameObject m_PoolingObjectPrefabs;     //���ӿ�����Ʈ�� ����
    Queue<ItemUse> m_PoolingObjectQueue = new Queue<ItemUse>();     //ť�� �迭����Ѱ� �������

    private void Awake()    
    {
        Instance = this;        //�ν�ź���� �ڽ�
        //Initialize(2);          //2�� �ʱ�ȭ �� ����
    }

    private void Initialize(int initCount)      //
    {
        for(int i=0; i<initCount; i++)          //�Ű����� ����ŭ ����
        {
            m_PoolingObjectQueue.Enqueue(CreateNewObject());        //ť�� ������Ʈ�� �־��ش�.
        }
    }

    //ù������ Ȱ��ȭ ���ִ� �Լ�
    private ItemUse CreateNewObject()
    {
        var newObj = Instantiate(m_PoolingObjectPrefabs).GetComponent<ItemUse>(); //�ڷ����� �°� ����
        newObj.gameObject.SetActive(true);             //�켱 Ȱ��ȭ
        newObj.transform.SetParent(transform);          //
        return newObj;
    }


    public static ItemUse GetObject()
    {
        if(Instance.m_PoolingObjectQueue.Count > 0)     //ť�迭(Initialize(����)��ŭ��
        {
            var obj = Instance.m_PoolingObjectQueue.Dequeue();  //ť�迭���� ��������
            obj.transform.SetParent(null);                      //���� �������� �θ�κ��� ������ ���ְ�
            obj.gameObject.SetActive(false);                    //��Ȱ��ȭ �����ش�
            return obj;                                         //�׸��� ��ȯ
        }
        else                                                    //ť�迭�� 0���� �� ���� ���
        {
            var newObj = Instance.CreateNewObject();            //���� ����
            newObj.gameObject.SetActive(true);                  //Ȱ��ȭ�����ְ�
            newObj.transform.SetParent(null);                   //�θ�κ��� ������ �ϰ�
            return newObj;                                      //��ȯ
        }
    }

    public static void ReturnObject(ItemUse obj)
    {
        obj.gameObject.SetActive(false);                    //�ٽ� ��Ȱ��ȭ
        obj.transform.SetParent(Instance.transform);       //���ڸ��� ��ġ����
        Instance.m_PoolingObjectQueue.Enqueue(obj);         //ť�迭�� �ٽ� �������
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
