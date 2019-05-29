using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Scene
{
    public override void Enter()
    {
        UIManager.Instance.SetActive(UIList.Loading, false);
        UIManager.Instance.FadeIn(false, 2.0f, null);
    }

    public override void Progress(float delta)
    {
        UIManager.Instance.CallEvent(UIList.Loading, "SetProgress", delta);
    }
}
