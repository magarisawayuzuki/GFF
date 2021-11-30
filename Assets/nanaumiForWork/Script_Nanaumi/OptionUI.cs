using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : UIController
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        InputManager();
        //GetVolume();
        HowtoUI();
    }

    protected override void InputManager()
    {
        base.InputManager();
    }

    private void GetVolume()
    {

    }

    private void HowtoUI()
    {

    }
}
