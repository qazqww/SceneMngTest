using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper 
{
    public static void BindBtnFunc(Transform t, string path, UnityEngine.Events.UnityAction action)
    {
        Button btn = UtilHelper.Find<Button>(t, path);
        if (btn != null)
            btn.onClick.AddListener(action);
    }
}
