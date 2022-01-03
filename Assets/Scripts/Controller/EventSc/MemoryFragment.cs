using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;

    private Vector3 _tracking = new Vector3(0, 0);

    [SerializeField] private float _TRACKING_SPEED = 5;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Tracking();
    }

    private void Tracking()
    {
        _tracking = _player.transform.position - this.transform.position;
        transform.position += _tracking * Time.deltaTime * _TRACKING_SPEED;
    }
}
