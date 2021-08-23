using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingState : StateMachineBehaviour
{
    [SerializeField] float m_ReloadTime = 0.9f;    //장전시간
    public bool isReload = false;              //현재 장전중인지
    

    //업데이트문 시작프레임1과 끝프레임1을 제외한 모든 업데이트를 여기서 관리.
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.R))    //장전중이면
        {
            return;     //함수 종료
        }
        animator.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Weapons>().isReLoading = true;        //프로퍼티값을 변경
        //animator.GetComponent<Weapons>().isReLoading = true;
        if (stateInfo.normalizedTime >= m_ReloadTime && isReload == false)                                                       //0.9초가 지나간다면
        {
            animator.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Weapons>().Reload();      //웨폰스크립트안에 있는 리로드함수 불러오기
            isReload = true;                                //장전중 
            //animator.transform.GetComponent<Weapons>().Reload();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Weapons>().isReLoading = false;
        //animator.GetComponent<Weapons>().isReLoading = false;
        isReload = false;       //장전 끝
    }
}
