using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : Scene
{
    public override void Init()
    {
        AddChannel(Channel.C1, SceneType.Town);
    }

    public override void Enter()
    {
        UIManager.Instance.FadeIn(false, 2.0f, null);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "NextScene"))
        {
            UIManager.Instance.FadeOut(true, 2.0f, delegate() { SceneMng.Instance.Event(Channel.C1); });
        }
    }
}
