using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : UIController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        _isPause = true;
    }

    private void Update()
    {
        InputManager();
    }

    protected override void InputManager()
    {
        base.InputManager();
    }
}
