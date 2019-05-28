using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilHelper 
{
    public static T Find<T>( Transform t, 
                 string path, bool active = true ) 
                 where T: UnityEngine.Component
    {
        Transform fObj =  t.Find(path);
        if( fObj != null )
        {
            fObj.gameObject.SetActive(active);
            return fObj.GetComponent<T>();
        }

        return null;
    }

    public static T CreateObject<T>(Transform parent, bool CallInit = false) where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        obj.transform.SetParent(parent);
        T t = obj.GetComponent<T>();

        if (CallInit)
            t.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

        return t;
    }

    public static T Instantiate<T>(string path, Vector3 pos, Quaternion rot, bool CallInit = false, Transform parent = null) where T : Component
    {
        T t = Object.Instantiate(Resources.Load<T>(path), pos, rot, parent);
        if (CallInit)
            t.gameObject.SendMessage("Init", SendMessageOptions.DontRequireReceiver);

        return t;
    }
}
