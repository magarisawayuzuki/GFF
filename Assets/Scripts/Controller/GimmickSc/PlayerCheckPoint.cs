using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private CheckPointSystem _checkPointSystem= default;

    private bool _canCheckPoint = true;

    private int _checkPointRange = 10;

    private void Awake()
    {
        _checkPointSystem = GameObject.FindWithTag("EventSystem").GetComponent<CheckPointSystem>();
    }

    private void Update()
    {
        if (_canCheckPoint)
        {
            if (Physics.Raycast(transform.position, Vector3.up, _checkPointRange, LayerMask.GetMask("Player")))
            {
                _checkPointSystem._checkPoint = transform.position;
                this.gameObject.SetActive(false);
            }
        }
    }
}
