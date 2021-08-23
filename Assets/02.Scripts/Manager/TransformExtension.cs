using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtention
{
    public static Transform FindByParent(this Transform transform, string name)
    {
        Transform current = transform;          //자신의 트랜스폼을 current란 이름으로 바꿈
        while (true)
        {
            if (current == null)                //자신의 트랜스폼이 널일 경우 
                return null;                    //널값 반환

            if (current.name == name)           //current.
                return current;                 //트랜스폼 반환

            current = current.parent;           //부모 객체를 찾는다.
        }
    }

    public static T FindComponentByParent<T>(this Transform transform) where T : Component
    {
        Transform current = transform;
        T result = null;                //자식의 result는 우선 null
        while (true)
        {
            if (current == null)
                return null;

            result = current.GetComponent<T>();
            if (result)
                return result;

            current = current.parent;
        }
    }
}