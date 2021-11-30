using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    private GameObject playerChara = default;

    private Vector3 nowCameraPos = default;
    private Vector3 RESET_Z_POS = new Vector3(1, 1, 0);
    
    private const float DISTANCE_STANDARD_VALUE = 0.7f;
    private const float DISTANCE_STOP_AREA_VALUE = 0.5f;
    private const float CAMERA_MOVE_SPEED = 0.04f;

    private bool _inVerticalArea = false;

    private void Start()
    {
        playerChara = GameObject.FindGameObjectWithTag("Player");

        RESET_Z_POS = new Vector3(0, 0, this.gameObject.transform.position.z - playerChara.gameObject.transform.position.z);
    }

    private void Update()
    {
        CheckDistance();
    }

    /// <summary>
    /// 距離の差を求めてフラグを管理
    /// </summary>
    private void CheckDistance()
    {
        //カメラの位置
        Vector3 cameraNowPos = this.gameObject.transform.position;
        //z座標を補完したカメラの位置
        //Vector3 adjustmentNowPos = this.gameObject.transform.position - RESET_Z_POS;

        //二点の差
        float distance = Vector3.Distance(cameraNowPos, playerChara.transform.position);
        print(distance);

        //Distanceを基にカメラを動かす
        MoveCamera(distance);
    }

    /// <summary>
    /// カメラの挙動制御
    /// </summary>
    private void MoveCamera(float distance)
    {
        //メイン挙動処理
        float adjustmentSpeed = distance * CAMERA_MOVE_SPEED;
        if(adjustmentSpeed <= 0.03f)
        {
            adjustmentSpeed = 0.03f;
        }

        //上下動作無し
        if (!_inVerticalArea)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.transform.position, playerChara.transform.position, adjustmentSpeed);
        }
        //上下動作あり
        else
        {

        }
    }
}