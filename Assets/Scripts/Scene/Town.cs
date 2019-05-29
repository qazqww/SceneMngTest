using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : Scene
{
    public override void Init()
    {
        AddChannel(Channel.C1, SceneType.Game);
    }

    public override void Enter()
    {
        UIManager.Instance.FadeIn(false, 2.0f, null);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "NextScene"))
        {
            UIManager.Instance.FadeOut(false, 2.0f,
                delegate () {
                UIManager.Instance.SetActive(UIList.Loading, true);
                SceneMng.Instance.Event(Channel.C1, true);
                }
            );
        }
    }
}