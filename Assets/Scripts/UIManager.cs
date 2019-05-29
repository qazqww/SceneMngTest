using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UIList
{
    FadeInOut,
    Loading
}

public class UIManager : TSingleton<UIManager>
{
    Dictionary<UIList, BaseUI> uiDic = new Dictionary<UIList, BaseUI>();

    readonly string path = "Prefabs/UI/";

    protected override void Init()
    {
        gameObject.AddComponent<StandaloneInputModule>();
        Add(UIList.FadeInOut);
        Add(UIList.Loading, false);
    }

    public BaseUI Add(UIList ui, bool activeState = true)
    {
        if (uiDic.ContainsKey(ui))
            return uiDic[ui];

        BaseUI baseUI = UtilHelper.Instantiate<BaseUI>(path + ui, transform.position, Quaternion.identity, true, transform);

        if (baseUI != null)
        {
            baseUI.gameObject.SetActive(activeState);
            uiDic.Add(ui, baseUI); // ui : UI 이름, baseUI : UI 오브젝트
        }

        return baseUI;
    }

    private IEnumerator IEAdd(UIList ui, bool activeState, BaseUI refUI)
    {
        if(!uiDic.ContainsKey(ui))
        {
            ResourceRequest request = Resources.LoadAsync(path + ui);
            refUI = request.asset as BaseUI;

            while (request != null && !request.isDone)
            {
                yield return null;
            }

            if(refUI != null)
            {
                refUI.gameObject.SetActive(activeState);
                uiDic.Add(ui, refUI);
            }
        }
    }

    public void AddAsync(UIList ui, bool activeState, ref BaseUI baseUI)
    {
        StartCoroutine(IEAdd(ui, activeState, baseUI));
    }

    public T Get<T>(UIList ui) where T : BaseUI
    {
        if (uiDic.ContainsKey(ui))
            return uiDic[ui].GetComponent<T>();
        return null;
    }

    public void Delete(UIList ui)
    {
        if(uiDic.ContainsKey(ui))
        {
            BaseUI baseUI = uiDic[ui];
            uiDic.Remove(ui);
            Destroy(baseUI.gameObject);
        }
    }

    public void FadeIn(bool activeState, float time, System.Action action)
    {
        FadeInOut fade = Get<FadeInOut>(UIList.FadeInOut);
        if (fade != null)
            fade.FadeIn(activeState, time, action);
    }

    public void FadeOut(bool activeState, float time, System.Action action)
    {
        FadeInOut fade = Get<FadeInOut>(UIList.FadeInOut);
        if (fade != null)
            fade.FadeOut(activeState, time, action);
    }

    public void CallEvent(UIList ui, string func, object obj = null)
    {
        if (uiDic.ContainsKey(ui))
        {
            uiDic[ui].SendMessage(func, obj, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void SetActive(UIList ui, bool state)
    {
        if (uiDic.ContainsKey(ui))
            uiDic[ui].SetActive(state);
    }
}
