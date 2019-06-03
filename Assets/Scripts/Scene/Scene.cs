using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // 다른 씬으로 이동할 수 있는 채널 목록
    Dictionary<Channel, SceneType> channel = new Dictionary<Channel, SceneType>();

    private void Start()
    {
        
    }

    // channel에 (enum)Channel값과 SceneType 추가
    protected void AddChannel(Channel c, SceneType sceneType)
    {
        if (!channel.ContainsKey(c))
            channel.Add(c, sceneType);
    }

    // channel에서 c값 Channel을 얻어옴
    public SceneType GetScene(Channel c)
    {
        if (channel.ContainsKey(c))
            return channel[c];

        return SceneType.None;
    }

    //public void Load(SceneType sceneType)
    //{
    //    SceneManager.LoadSceneAsync(sceneType.ToString());
    //}

    // 여기서 <float>는 System.Action이 받는 매개변수이다
    protected void LoadAsync(SceneType sceneType, System.Action<float> func = null)
    {
        StartCoroutine(IELoadAsync(sceneType, func));
    }

    // sceneType 씬을 비동기 로딩, func에 Progress함수를 받아 로딩 진행상황 표시
    IEnumerator IELoadAsync(SceneType sceneType, System.Action<float> func = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneType.ToString());

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

    public void FalseLoading(SceneType sceneType, float time = 2.0f, System.Action<float> func = null)
    {
        StartCoroutine(IEFalseAsync(sceneType, time, func));
    }

    // sceneType 씬을 비동기 로딩, 미리 설정한 시간대로 로딩이 진행되는 상황 표시
    IEnumerator IEFalseAsync(SceneType sceneType, float time, System.Action<float> func = null)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneType.ToString());

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

    // 로딩 상황에 진입시키는 함수 (SceneMng의 Enable 함수에서 새로운 씬 활성화 시 사용)
    public void Enter(SceneType sceneType, bool falseLoading = false, float time = 2.0f)
    {
        if (!falseLoading)
            LoadAsync(sceneType, Progress);
        else
            FalseLoading(sceneType, time, Progress);
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
