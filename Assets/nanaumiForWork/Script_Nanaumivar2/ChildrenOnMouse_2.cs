using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChildrenOnMouse_2 : MonoBehaviour
{
    private bool _onButton = false;

    public void OnMouseChoose()
    {
        if (!_onButton)
        {
            Debug.Log("MouseOver");
            transform.parent.gameObject.SendMessage("MouseMove", this.gameObject.name);
            _onButton = true;
        }
    }

    public void OnMouseExitHere()
    {
        if (_onButton)
        {
            Debug.Log("MouseExit");
            _onButton = false;
        }
    }

    public void OnMouseDecide()
    {
        if (_onButton)
        {
            Debug.Log("MouseDecide");
            transform.parent.gameObject.SendMessage("MouseDecide", this.gameObject.name);
        }
    }
}
