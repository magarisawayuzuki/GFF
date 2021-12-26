using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public Vector3 _checkPoint = default;

    [SerializeField]
    private GameObject player = default;

    private void Awake()
    {
        GameObject.FindWithTag("Player");
    }

    public void Respawn()
    {
        _checkPoint.y += 3;
        player.transform.position = _checkPoint;
    }
}