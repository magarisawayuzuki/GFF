using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : UIController
{
    [SerializeField] private GameObject _soundOptionUI;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        InputManager();
        GetVolume();
        HowtoUI();
    }

    protected override void InputManager()
    {
        base.InputManager();
    }

    private void GetVolume()
    {
        // soundOptionUIを静的に生成するか、動的に生成するか

    }

    /// <summary>
    /// UIの有効化
    /// </summary>
    private void HowtoUI()
    {

    }
}
