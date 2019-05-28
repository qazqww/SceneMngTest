using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UIList
{
    FadeInOut
}

public class UIManager : TSingleton<UIManager>
{
    Dictionary<UIList, BaseUI> uiDic = new Dictionary<UIList, BaseUI>();

    readonly string path = "Prefabs/UI";

    protected override void Init()
    {
        gameObject.AddComponent<StandaloneInputModule>();
        gameObject.AddComponent<EventSystem>();
        Add(UIList.FadeInOut);
    }

    public BaseUI Add(UIList ui)
    {
        if (uiDic.ContainsKey(ui))
            return uiDic[ui];

        BaseUI baseUI = UtilHelper.Instantiate<BaseUI>(path + ui, transform.position, Quaternion.identity);
        
        if (baseUI != null)
            uiDic.Add(ui, baseUI); // ui : UI 이름, baseUI : UI 오브젝트

        return baseUI;
    }

    private IEnumerator IEAdd(UIList ui, BaseUI refUI)
    {
        if(!uiDic.ContainsKey(ui))
        {
            ResourceRequest request = Resources.LoadAsync(path + ui);
            refUI = request.asset as BaseUI;

            while (request != null && !request.isDone)
            {
                yield return null;
            }

            uiDic.Add(ui, refUI);
        }
    }

    public void AddAsync(UIList ui, ref BaseUI baseUI)
    {
        StartCoroutine(IEAdd(ui, baseUI));
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

    public void CallEvent(UIList ui, string func, object obj = null)
    {
        if (uiDic.ContainsKey(ui))
        {
            uiDic[ui].SendMessage(func, obj, SendMessageOptions.DontRequireReceiver);
        }
    }
}
