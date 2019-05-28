using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void Load(SceneType scene)
    {
        SceneManager.LoadSceneAsync(scene.ToString());
    }

    protected void LoadAsync(SceneType scene, System.Action<float> func = null)
    {
        StartCoroutine(IELoad(scene, func));
    }

    IEnumerator IELoad(SceneType scene, System.Action<float> func = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.ToString());

        bool state = false;

        while (!state)
        {
            if (func != null)
                func(operation.progress);
            if (operation.isDone)
                state = true;

            yield return null;
        }
    }

    public void FalseLoading(SceneType scene, float time, System.Action<float> func = null)
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
                state = true;

            func(elapsedTime);

            yield return null;
        }
    }

    public void Enter(SceneType scene, bool falseLoading = false, float time = 2.0f)
    {
        if (!falseLoading)
            LoadAsync(scene, Progress);
        else
            FalseLoading(scene, time, Progress);
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
