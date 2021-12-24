using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private CheckPointSystem _checkPointSystem= default;

    private bool _canCheckPoint = true;

    private void Awake()
    {
        _checkPointSystem = GameObject.FindWithTag("CheckPointSystem").GetComponent<CheckPointSystem>();
    }

    private void Update()
    {
        if (_canCheckPoint)
        {
            if (Physics.Raycast(transform.position, Vector3.up, 10, LayerMask.GetMask("Player")))
            {
                _checkPointSystem._checkPoint.position = transform.position;
                this.gameObject.SetActive(false);
            }
        }
    }
}
