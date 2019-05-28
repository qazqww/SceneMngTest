using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    Title,
    Town,
    Game
}

public class SceneMng : TSingleton<SceneMng>
{
    Dictionary<SceneType, Scene> sceneDic = new Dictionary<SceneType, Scene>();

    public T AddScene<T> (SceneType scene) where T : Scene
    {
        if(!sceneDic.ContainsKey(scene))
        {
            T t = UtilHelper.CreateObject<T>(transform, true);
            sceneDic.Add(scene, t);

            return t;
        }
        return null;
    }

    #region
    // 어플리케이션에 포커스가 맞춰질 경우 (윈도우 창을 클릭하여 활성화하는 경우)
    private void OnApplicationFocus(bool focus)
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        UIManager.Instance.Release();
        Release();
    }
    #endregion
}
