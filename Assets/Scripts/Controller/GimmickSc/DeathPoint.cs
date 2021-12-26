using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoint : MonoBehaviour
{
    private GameObject player = default;

    private CheckPointSystem checkPointSystem = default;

    private Vector3 scale = new Vector3(1,1500);
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        checkPointSystem = GameObject.FindWithTag("EventSystem").GetComponent<CheckPointSystem>();
    }
    private void Update()
    {
        if (Physics.BoxCast(transform.position, scale, Vector3.right, Quaternion.identity, 0, LayerMask.GetMask("Player")))
        {
            checkPointSystem.Respawn();
        }
    }
}
