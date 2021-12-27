using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    private CheckPointSystem _checkPointSystem= default;

    private GameObject player = default;

    private bool _canCheckPoint = true;

    private int _checkPointRange = 10;

    private void Awake()
    {
        _checkPointSystem = GameObject.FindWithTag("EventSystem").GetComponent<CheckPointSystem>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (_canCheckPoint)
        {
            if (this.transform.position.x <= player.transform.position.x)
            {
                _checkPointSystem._checkPoint = transform.position;
                _checkPointSystem._checkPoint.y += 1.3f;
                this.gameObject.SetActive(false);
            }
        }
    }
}
