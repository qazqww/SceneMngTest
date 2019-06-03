// SceneMng.cs : 씬의 추가와 씬의 전환을 다루는 매니저 클래스

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 어떤 씬인지 나타냄
public enum SceneType
{
    None,
    Title,
    Town,
    Game
}

// 다른 씬으로 통하는 통로
public enum Channel
{
    C1,
    C2
}

public class SceneMng : TSingleton<SceneMng>
{
    SceneType curScene = SceneType.Title;

    Dictionary<SceneType, Scene> sceneDic = new Dictionary<SceneType, Scene>();
    
    // scene이 sceneDic에 없으면, 오브젝트를 새로 만들어서 추가한 뒤 T 컴포넌트 리턴
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

    // 현재 씬을 종료시키고 scene으로 넘어감
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

    // sceneDic을 순회하며 scene의 스크립트만 활성화
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

    // 해당 채널의 SceneType을 받아 Enable시킴
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
