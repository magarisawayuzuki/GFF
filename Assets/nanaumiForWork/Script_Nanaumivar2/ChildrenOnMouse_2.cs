using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChildrenOnMouse_2 : MonoBehaviour
{
    [CalledFromSendMessage]
    private void OnMouseChoose()
    {
        transform.parent.gameObject.SendMessage("MouseMove",this.gameObject.name);
    }
}
