using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingObjectPooling : MonoBehaviour
{
    public static CasingObjectPooling Instance;     
    [SerializeField] GameObject m_Casing;           //���ӿ�����Ʈ�� �������� �� ���ϱ�
    Queue<GameObject> m_Casings;                     //������ �������!
    //GameObject[] gameObjects = new GameObject[100];
    [SerializeField] Transform m_CasingPos;                          //��ġ ����

    //[SerializeField] int m_PoolingCount;
            
    Weapons m_Weapons;
    private void Start()
    {
        Instance = this;        //�ν�ź���� �ڽ�
        m_Weapons = GameObject.FindWithTag("Player").GetComponent<Weapons>();
        m_Casings = new Queue<GameObject>();
        //for(int i = 0; i < m_Casings.Count; i++)
        //{
        //    GameObject obj = Instantiate(m_Casing, m_CasingPos.position, Quaternion.identity);
        //    obj.SetActive(false);                   //��Ȱ��ȭ
        //    m_Casings.Enqueue(obj);                     //����Ʈ�� �߰�
        //}

        Initialize(30);          //N�� �ʱ�ȭ �� ����
    }


	private void Initialize(int initCount)          //�ʱ�ȭ
    {
        for (int i = 0; i < initCount; i++)          //�Ű����� ����ŭ ����
        {
            m_Casings.Enqueue(CreateNewObject());        //����Ʈ�� �߰�
        }
    }
    
    //����
    private GameObject CreateNewObject()
    {
        GameObject casing = Instantiate(m_Casing, m_CasingPos); //�ڷ����� �°� ����
        casing.gameObject.SetActive(false);             //�켱 ��Ȱ��ȭ
        casing.transform.SetParent(transform);          //
        return casing;
    }


    public static GameObject GetObject()
    {
        if (Instance.m_Casings.Count > 0)                       //ť�� ũ�� ��ŭ
        {
            var obj = Instance.m_Casings.Dequeue();             //ť���� �������ְ�
            //obj.transform.SetParent(null);                      //���� �������� �θ�κ��� ������ ���ְ�
            obj.gameObject.SetActive(true);                    //Ȱ��ȭ �����ش�
            return obj;                                         //�׸��� ��ȯ
        }
        else                                                    //ť�迭�� 0���� �� ���� ���
        {
            var newObj = Instance.CreateNewObject();            //���� ����
            newObj.gameObject.SetActive(true);                  //Ȱ��ȭ�����ְ�
            //newObj.transform.SetParent(null);                   //�θ�κ��� ������ �ϰ�
            return newObj;                                      //��ȯ
        }
        
    }
    
    public static void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);                   //Ȱ��ȭ
        //obj.gameObject.transform.position = 
        Instance.m_Casings.Enqueue(obj);                   //ť�迭�� �ٽ� �������
        
        //if (obj.)
        //{
        //    obj.Heal();
        //} 
    }
}
