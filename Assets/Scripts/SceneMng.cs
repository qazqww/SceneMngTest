using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    None,
    Title,
    Town,
    Game
}

public enum Channel
{
    C1,
    C2
}

public class SceneMng : TSingleton<SceneMng>
{
    SceneType curScene = SceneType.Title;

    Dictionary<SceneType, Scene> sceneDic = new Dictionary<SceneType, Scene>();

    public T AddScene<T> (SceneType scene, bool state = false) where T : Scene
    {
        if(!sceneDic.ContainsKey(scene))
        {
            T t = UtilHelper.CreateObject<T>(transform, true);
            t.enabled = state;
            sceneDic.Add(scene, t);

            return t;
        }
        return null;
    }

    public void Enable(SceneType scene, bool falseLoading = false, float time = 2.0f)
    {
        if (sceneDic.ContainsKey(curScene))
            sceneDic[curScene].Exit(); // 현재 씬의 종료 함수를 호출한 뒤
        if (sceneDic.ContainsKey(scene))
        {
            ScriptEnable(scene);
            curScene = scene; // 매개변수로 받은 다음 씬을 현재 씬으로 한다.
            sceneDic[curScene].Enter(scene, falseLoading, time);
        }
    }

    public void ScriptEnable(SceneType scene)
    {
        foreach(var pair in sceneDic)
        {
            if (pair.Key != scene)
                pair.Value.enabled = false;
            else
                pair.Value.enabled = true;
        }
    }

    public void Event(Channel c, bool falseLoading = false, float time = 2.0f)
    {
        if(sceneDic.ContainsKey(curScene))
        {
            SceneType sceneType = sceneDic[curScene].GetScene(c);
            Enable(sceneType, falseLoading, time);
        }
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
