using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilHelper 
{
    // 현재 씬에서 경로 대상(Transform)의 T 컴포넌트를 찾아 리턴
    public static T Find<T>(Transform t, string path, bool active = true) where T : Component
    {
        Transform fObj =  t.Find(path);
        if( fObj != null )
        {
            fObj.gameObject.SetActive(active);
            return fObj.GetComponent<T>();
        }

        return null;
    }

    // T 컴포넌트를 가진 새로운 게임오브젝트를 생성하고, T 컴포넌트를 리턴
    public static T CreateObject<T>(Transform parent, bool CallInit = false) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        obj.transform.SetParent(parent);
        T t = obj.GetComponent<T>();

        if (CallInit)
            t.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

        return t;
    }

    // 리소스의 해당 경로에 있는 객체를 로드하여 생성하고, T 컴포넌트를 리턴
    public static T Instantiate<T>(string path, Vector3 pos, Quaternion rot, bool CallInit = false, Transform parent = null) where T : Component
    {
        T t = Object.Instantiate(Resources.Load<T>(path), pos, rot, parent);
        if (CallInit)
            t.gameObject.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

        return t;
    }
}
