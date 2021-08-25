using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] GameObject m_Heal;
    [SerializeField] GameObject m_Ammo;
    [SerializeField] Transform[] m_ItemSpawnTransform;
    ItemUse m_ItemUse;
    //float m_RespawnTime;            //
    float m_RespawnRate = 15f;            //������ð�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)                 //�΋H�ƴٸ�
    {
        if(other.gameObject.tag == "Player")                    //���࿡ �÷��̾��� �±װ� �����Ŷ�
        {

            ObjectPool.ReturnObject(this);
        }

        if(ObjectPool.ReturnObject(m_ItemUse.m_Ammo.GetComponent<ItemUse>().))
    }
}
