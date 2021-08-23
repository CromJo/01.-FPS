using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] Vector3 m_AimPosition;
    private Vector3 m_DefaultPosition;
    private bool isAiming;
    private float m_AimSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void AimZoom()
    {
        if (Input.GetButton("Fire2"))
        {
            //transform.localPosition = Vector3.Lerp(transform.localPosition, m_AimPosition, Time.deltaTime * 8f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40f, Time.deltaTime * 8f);
            isAiming = true;
        }
        else
        {
            //transform.localPosition = Vector3.Lerp(transform.localPosition, m_RecoilReturn, Time.deltaTime * 5f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 8f);
            isAiming = false;
        }
    }
}
