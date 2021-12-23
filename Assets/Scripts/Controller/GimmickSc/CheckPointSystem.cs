using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public Transform _checkPoint = default;

    private GameObject player = default;

    private void Awake()
    {
        GameObject.FindWithTag("Player");
    }

    public void Respawn()
    {

    }
}
