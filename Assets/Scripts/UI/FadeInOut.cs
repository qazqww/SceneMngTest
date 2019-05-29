using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : BaseUI
{
    Image fadeImage;
    Color start;
    Color end;

    float time = 0;
    float elapsedTime = 0;
    bool update = false; // 코루틴 제어 변수

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        fadeImage = UtilHelper.Find<Image>(transform, "Image");
    }

    //private void OnGUI()
    //{
    //    if(GUI.Button(new Rect(0,0,100,100), "FadeIn"))
    //    {
    //        FadeIn(false, 1.0f);
    //    }
    //    if (GUI.Button(new Rect(100, 0, 100, 100), "FadeOut"))
    //    {
    //        FadeOut(true, 1.0f);
    //    }
    //}

    // showState : 페이드 처리 이후에 계속 보이도록 남겨둘지 여부
    // action : 페이드가 완료될 시 호출할 함수
    private void Fade(Color start, Color end, bool showState, float time, System.Action action = null)
    {
        if (update)
        {
            Debug.Log("Fade가 이미 진행 중입니다.");
            return;
        }
        update = true;
        fadeImage.color = start;

        StartCoroutine(IEFade(start, end, showState, time, action));
    }    

    IEnumerator IEFade(Color start, Color end, bool showState, float time, System.Action action)
    {
        while (update)
        {
            elapsedTime += Time.deltaTime / time;
            elapsedTime = Mathf.Clamp01(elapsedTime);
            fadeImage.color = Color.Lerp(start, end, elapsedTime);

            if (elapsedTime >= 1.0f)
            {
                update = false;
                elapsedTime = 0;
                if (action != null) action();
            }

            yield return null;
        }
        yield return null;
    }

    public void FadeIn(bool showState, float time, System.Action action = null)
    {
        Color start = Color.black;
        Color end = Color.black;
        end.a = 0;

        Fade(start, end, showState, time, action);
    }

    public void FadeOut(bool showState, float time, System.Action action = null)
    {
        Color start = Color.black;
        Color end = Color.black;
        start.a = 0;

        Fade(start, end, showState, time, action);
    }
}
