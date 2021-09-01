using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    //ItemUse m_ItemUse;
    GameObject[] m_Item2;                               //�迭�� �����
    List<ItemUse> m_ItemUse2;                           //����Ʈ������ ������ ��ũ��Ʈ�� ����

    [SerializeField] float[] m_RespawnItem;
    [SerializeField] float m_CreateItem = 15f;
    // Start is called before the first frame update
    void Start()
    {
        m_ItemUse2 = new List<ItemUse>();                               //�����Ҵ� ���ְ�
        m_Item2 = GameObject.FindGameObjectsWithTag("Item");            //Item�±װ� �� ��� ���ӿ�����Ʈ���� �־��ְ�.
        //Debug.Log(m_Item2.Length);
        for(int i = 0;  i < m_Item2.Length; ++i)                        //���ӿ�����Ʈ�� ������ŭ ������
        {
            m_ItemUse2.Add(m_Item2[i].GetComponent<ItemUse>());         //����Ʈ�� �߰����ش�.
        }

        //m_ItemUse = GameObject.FindWithTag("Item").GetComponent<ItemUse>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_Item2.Length; i++)
        {
            if (m_ItemUse2[i].transform.gameObject.activeSelf == false)          //ItemUse��ũ��Ʈ�� ���� ��ü�� ��Ȱ��ȭ �Ǿ����� ���
            {
                m_RespawnItem[i] += Time.deltaTime;                              //�ð� ����
            }

            if (m_RespawnItem[i] >= m_CreateItem)                                //���� Ư�� �ð��� ���� ���� ���
            {
                m_RespawnItem[i] = 0f;                                           //�ð��� �ʱ�ȭ
                m_ItemUse2[i].transform.gameObject.SetActive(true);              //��Ȱ��ȭ�� �������� Ȱ��ȭ
                m_ItemUse2[i].transform.SetParent(this.transform);               //�θ��� �������� �ڸ��̵�
            }
        }
    }
}
