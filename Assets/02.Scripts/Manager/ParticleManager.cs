using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    Dictionary<string, GameObject> m_PrefabList = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        AddEffectPrefab("MoneyNotes");
    }

    public void AddEffectPrefab(string key)
    {
        GameObject MoneyNotesPrefab = Resources.Load("FX/" + key) as GameObject;
        m_PrefabList.Add(key, MoneyNotesPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
