using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtention
{
    public static Transform FindByParent(this Transform transform, string name)
    {
        Transform current = transform;          //�ڽ��� Ʈ�������� current�� �̸����� �ٲ�
        while (true)
        {
            if (current == null)                //�ڽ��� Ʈ�������� ���� ��� 
                return null;                    //�ΰ� ��ȯ

            if (current.name == name)           //current.
                return current;                 //Ʈ������ ��ȯ

            current = current.parent;           //�θ� ��ü�� ã�´�.
        }
    }

    public static T FindComponentByParent<T>(this Transform transform) where T : Component
    {
        Transform current = transform;
        T result = null;                //�ڽ��� result�� �켱 null
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