using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] GameObject m_Heal;
    [SerializeField] GameObject m_Ammo;
    [SerializeField] Transform[] m_ItemSpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Heal()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            ObjectPool.ReturnObject(this);
        }
    }
}
