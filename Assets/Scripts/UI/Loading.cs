using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : BaseUI
{
    Slider slider;

    public void SetProgress(float delta)
    {
        slider.value = delta;
    }

    public override void Init()
    {
        slider = UtilHelper.Find<Slider>(transform, "Slider");
    }
}
