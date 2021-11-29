using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    private GameObject playerChara = default;
    private Vector3 nowCameraPos = default;
    private Vector3 RESET_Z_POS = new Vector3(1, 1, 0);

    private void Start()
    {
        playerChara = GameObject.FindGameObjectWithTag("Player");
        RESET_Z_POS = new Vector3(0, 0, this.gameObject.transform.position.z - playerChara.gameObject.transform.position.z);
    }

    private void Update()
    {
        //カメラの位置
        Vector3 cameraNowPos = this.gameObject.transform.position;
        //z座標を補完したカメラの位置
        Vector3 adjustmentNowPos = this.gameObject.transform.position - RESET_Z_POS;
        
        //二点の差
        float distance = Vector3.Distance(adjustmentNowPos, playerChara.transform.position);
    }
}