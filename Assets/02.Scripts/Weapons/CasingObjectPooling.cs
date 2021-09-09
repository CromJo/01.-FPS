using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingObjectPooling : MonoBehaviour
{
    public static CasingObjectPooling Instance;     
    [SerializeField] GameObject m_Casing;           //���ӿ�����Ʈ�� �������� �� ���ϱ�
    List<GameObject> m_Casings;                     //������ �������!          
    Transform m_CasingPos;                          //��ġ ����

    [SerializeField] int m_PoolingCount;
            
    Weapons m_Weapons;
    private void Start()
    {
        Instance = this;        //�ν�ź���� �ڽ�

        m_Casings = new List<GameObject>();
        //for(int i = 0; i < m_Casings.Count; i++)
        //{
        //    GameObject obj = Instantiate(m_Casing, m_CasingPos.position, Quaternion.identity);
        //    obj.SetActive(false);                   //��Ȱ��ȭ
        //    m_Casings.Add(obj);                     //����Ʈ�� �߰�
        //}

        Initialize(m_Weapons.Bullet);          //N�� �ʱ�ȭ �� ����
    }

    private void Initialize(int initCount)      //
    {
        for (int i = 0; i < initCount; i++)          //�Ű����� ����ŭ ����
        {
            m_Casings.Add(CreateNewObject());        //����Ʈ�� �߰�
        }
    }

    //ù������ Ȱ��ȭ ���ִ� �Լ�
    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(m_Casing); //�ڷ����� �°� ����
        newObj.gameObject.SetActive(false);             //�켱 ��Ȱ��ȭ
        newObj.transform.SetParent(transform);          //
        return newObj;
    }
    
    
    public static GameObject GetObject()
    {
        if (Instance.m_Casings.Count > 0)     //ť�迭(Initialize(����)��ŭ��
        {
            //GameObject obj = Instance.m_Casings.Remove(obj);  //ť�迭���� ��������
            //obj.transform.SetParent(null);                      //���� �������� �θ�κ��� ������ ���ְ�
            //obj.gameObject.SetActive(false);                    //��Ȱ��ȭ �����ش�
            //return obj;                                         //�׸��� ��ȯ
            return null;
        }
        else                                                    //ť�迭�� 0���� �� ���� ���
        {
            GameObject newObj = Instance.CreateNewObject();            //���� ����
            newObj.gameObject.SetActive(true);                  //Ȱ��ȭ�����ְ�
            newObj.transform.SetParent(null);                   //�θ�κ��� ������ �ϰ�
            return newObj;                                      //��ȯ
        }
    }
    
    public static void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);                    //Ȱ��ȭ
        obj.transform.SetParent(Instance.transform);       //���ڸ��� ��ġ����
        Instance.m_Casings.Add(obj);         //ť�迭�� �ٽ� �������
        //if (obj.)
        //{
        //    obj.Heal();
        //} 
    }
}
