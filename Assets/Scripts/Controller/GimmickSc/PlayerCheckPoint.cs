using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private bool _canCheckPoint = true;

    private void Awake()
    {
        GameObject.FindWithTag("");
    }

    private void Update()
    {
        if (_canCheckPoint)
        {
            if (Physics.Raycast(transform.position, Vector3.up, 10, LayerMask.GetMask("Player")))
            {

                _canCheckPoint = false;
            }
        }
    }
}
