using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    //ItemUse m_ItemUse;
    GameObject[] m_Item2;                               //배열로 만들고
    List<ItemUse> m_ItemUse2;                           //리스트형식의 아이템 스크립트를 선언

    [SerializeField] float[] m_RespawnItem;
    [SerializeField] float m_CreateItem = 15f;
    // Start is called before the first frame update
    void Start()
    {
        m_ItemUse2 = new List<ItemUse>();                               //동적할당 해주고
        m_Item2 = GameObject.FindGameObjectsWithTag("Item");            //Item태그가 들어간 모든 게임오브젝트들을 넣어주고.
        //Debug.Log(m_Item2.Length);
        for(int i = 0;  i < m_Item2.Length; ++i)                        //게임오브젝트의 갯수만큼 돌려서
        {
            m_ItemUse2.Add(m_Item2[i].GetComponent<ItemUse>());         //리스트에 추가해준다.
        }

        //m_ItemUse = GameObject.FindWithTag("Item").GetComponent<ItemUse>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_Item2.Length; i++)
        {
            if (m_ItemUse2[i].transform.gameObject.activeSelf == false)          //ItemUse스크립트를 가진 개체가 비활성화 되어있을 경우
            {
                m_RespawnItem[i] += Time.deltaTime;                              //시간 증가
            }

            if (m_RespawnItem[i] >= m_CreateItem)                                //만약 특정 시간을 도달 했을 경우
            {
                m_RespawnItem[i] = 0f;                                           //시간을 초기화
                m_ItemUse2[i].transform.gameObject.SetActive(true);              //비활성화된 아이템을 활성화
                m_ItemUse2[i].transform.SetParent(this.transform);               //부모의 몸속으로 자리이동
            }
        }
    }
}
