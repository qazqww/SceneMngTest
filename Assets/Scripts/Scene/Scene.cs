using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    Dictionary<Channel, SceneType> channel = new Dictionary<Channel, SceneType>();

    private void Start()
    {
        
    }

    protected void AddChannel(Channel c, SceneType sceneType)
    {
        if (!channel.ContainsKey(c))
            channel.Add(c, sceneType);
    }

    public SceneType GetScene(Channel c)
    {
        if (channel.ContainsKey(c))
            return channel[c];

        return SceneType.None;
    }

    public void Load(SceneType scene)
    {
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    protected void LoadAsync(SceneType scene, System.Action<float> func = null)
    {
        StartCoroutine(IELoadAsync(scene, func));
    }

    IEnumerator IELoadAsync(SceneType scene, System.Action<float> func = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.ToString());

        bool state = false;

        while (!state)
        {
            if (func != null)
                func(operation.progress);
            if (operation.isDone)
            {
                state = true;
                Enter();
            }

            yield return null;
        }
        yield return null;
    }

    public void FalseLoading(SceneType scene, float time = 2.0f, System.Action<float> func = null)
    {
        StartCoroutine(IEFalseAsync(scene, time, func));
    }

    IEnumerator IEFalseAsync(SceneType scene, float time, System.Action<float> func = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.ToString());

        bool state = false;
        float elapsedTime = 0;

        while (!state)
        {
            elapsedTime = Time.deltaTime / time;
            elapsedTime = Mathf.Clamp01(elapsedTime);
            if (elapsedTime >= 1.0f)
            {
                state = true;
                Enter();
            }

            func(elapsedTime);

            yield return null;
        }
        yield return null;
    }

    public void Enter(SceneType scene, bool falseLoading = false, float time = 2.0f)
    {
        if (!falseLoading)
            LoadAsync(scene, Progress);
        else
            FalseLoading(scene, time, Progress);
    }

    public virtual void Init()
    {

    }

    // 로드가 완료된 신에서 호출될 함수
    public virtual void Enter()
    {

    }

    // 종료된 이전 신에서 호출될 함수
    public virtual void Exit()
    {

    }

    public virtual void Progress (float delta)
    {

    }
}
