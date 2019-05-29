using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        // LoadTable();
        RegisterScene();
        // GameSetting();
        Invoke("FadeOut", 1);
    }

    void RegisterScene()
    {
        SceneMng.Instance.AddScene<Title>(SceneType.Title);
        SceneMng.Instance.AddScene<Town>(SceneType.Town);
        SceneMng.Instance.AddScene<Game>(SceneType.Game);
    }

    void GameSetting()
    {

    }

    void NextScene()
    {
        SceneMng.Instance.Enable(SceneType.Title);
    }

    void FadeOut()
    {
        UIManager.Instance.FadeOut(true, 2.0f, NextScene);
    }
    
    void FadeIn()
    {

    }

    void LoadTable()
    {

    }
}
