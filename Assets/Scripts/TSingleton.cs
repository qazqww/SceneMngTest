using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : TSingleton<T> // 상속받은 객체만 가능
{
    static T instance;

    protected TSingleton()
    {

    }    

    public static T Instance
    {
        get {
            if (instance == null)
            {
                instance = UtilHelper.CreateObject<T>(null, true);
                //GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                //instance = obj.GetComponent<T>();
                //instance.Init();

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    protected virtual void Init()
    {

    }

    public virtual void Release()
    {
        Destroy(gameObject);
        instance = null;
    }
}
