using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    ItemUse m_ItemUse;
    [SerializeField] float m_RespawnItem;
    [SerializeField] float m_CreateItem = 15f;
    // Start is called before the first frame update
    void Start()
    {
        m_ItemUse = GameObject.FindWithTag("Item").GetComponent<ItemUse>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ItemUse.transform.gameObject.activeSelf == false)          //ItemUse��ũ��Ʈ�� ���� ��ü�� ��Ȱ��ȭ �Ǿ����� ���
        {
            m_RespawnItem += Time.deltaTime;
        }
        
        if(m_RespawnItem >= m_CreateItem)
        {
            m_RespawnItem = 0f;
            m_ItemUse.transform.gameObject.SetActive(true);
        }
    }
}
