using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        Run();
    }

    public virtual void Init()
    {

    }

    protected virtual void Run()
    {

    }

    // UI 활성화 여부를 설정하는 함수
    public virtual void SetActive (bool state)
    {
        gameObject.SetActive(state);
    }

    // UI에서 특정 기능들을 막아둘 수 있게 하기 위한 함수
    public virtual void SetEnable (bool state)
    {

    }

    // UI를 보여주기 전에 설정할 정보값을 처리하는 함수 
    public virtual void Open()
    {

    }

    // UI를 닫기 전에 필요한 정보를 처리하는 함수
    public virtual void Close()
    {

    }
}
