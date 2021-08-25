using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    ItemHealth m_ItemHealth;
    [SerializeField] float m_RespawnItem;
    [SerializeField] float m_CreateItem = 15f;
    // Start is called before the first frame update
    void Start()
    {
        m_ItemHealth = GameObject.FindGameObjectWithTag("Item").GetComponent<ItemHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ItemHealth.transform.gameObject.activeSelf == false)          //ItemHealth��ũ��Ʈ�� ���� ��ü�� ��Ȱ��ȭ �Ǿ����� ���
        {
            m_RespawnItem += Time.deltaTime;
        }
        
        if(m_RespawnItem >= m_CreateItem)
        {
            m_RespawnItem = 0f;
            m_ItemHealth.transform.gameObject.SetActive(true);
        }
    }
}
