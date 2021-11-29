using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    private GameObject playerChara = default;
    private bool _isOutZone = false;
    private Vector3 nowCameraPos = default;
    private Vector3 RESET_Z_POS = new Vector3(1, 1, 0);
    private float twoDistance = default;

    private void Start()
    {
        playerChara = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        nowCameraPos = this.gameObject.transform.position;
        //二点の差
        print(Vector3.Distance(this.gameObject.transform.position, playerChara.transform.position));
    }

}