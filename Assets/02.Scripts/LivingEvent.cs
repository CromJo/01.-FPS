using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class LivingEvent : MonoBehaviour, IDamageable
{
    public int m_StartHP;
    public int m_LiveHP;
    public int LiveHP { get; protected set; }   
    public bool isDead { get; protected set; }
    public event Action onDeath;

    protected abstract void OnEnable();   //상속받은 생명체가 활성화될때 실행됨

    //대미지 입을때 실행될 기능
    public virtual void OnDamage(int damage/*, Vector3 hitPoint, Vector3 hitNormal*/)
    {
        LiveHP -= damage;           //매개변수로 들어온 값만큼 내 HP를 깎아준다.
        
        if(LiveHP <= 0 && !isDead)  //0보다 낮은데 죽지 않은 상태라면
        {
            Dead();                 //데드 함수 실행
        }
    }

    public virtual void RecoveryHP(int heal)
    {
        if (isDead)     //죽은상태면
            return;     //함수 종료해주고 아니면

        LiveHP += heal; //내 현재 HP 매개변수 값만큼 회복
    }

    public virtual void Dead()
    {
        if (onDeath != null)
            onDeath();

        isDead = true;
        //UIManager.u_Instance.GameOverActive(true);
	}

}

internal interface IDamageable
{
}