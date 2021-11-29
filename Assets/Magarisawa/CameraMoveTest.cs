using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private GameObject playerChara = default;
    private Vector3 nowCameraPos = default;
    private Vector3 RESET_Z_POS = new Vector3(1, 1, 0);
    private const int DISTANCE_STANDARD_VALUE = 3;
    private const int DISTANCE_STOP_AREA_VALUE = 3;

    private bool _isTracking = false;
    Vector3 a;

    private void Start()
    {
        playerChara = GameObject.FindGameObjectWithTag("Player");

        RESET_Z_POS = new Vector3(0, 0, this.gameObject.transform.position.z - playerChara.gameObject.transform.position.z);
        a = this.transform.position;

    }

    private void Update()
    {
        print(target.transform.position);
        print(this.gameObject.transform.position);


        this.gameObject.transform.position = Vector3.Lerp(a, target.transform.position, 0.04f);

        //CheckDistance();
        //MoveCamera();
    }

    /// <summary>
    /// 距離の差を求めてフラグを管理
    /// </summary>
    private void CheckDistance()
    {
        //カメラの位置
        Vector3 cameraNowPos = this.gameObject.transform.position;
        //z座標を補完したカメラの位置
        Vector3 adjustmentNowPos = this.gameObject.transform.position - RESET_Z_POS;

        //二点の差
        float distance = Vector3.Distance(adjustmentNowPos, playerChara.transform.position);

        //追跡開始フラグ
        //差が追跡開始値よりも大きい
        if (distance > DISTANCE_STANDARD_VALUE && !_isTracking)
        {
            _isTracking = false;
        }

        //追跡終了フラグ
        //差が追跡終了値よりも小さい
        else if (distance < DISTANCE_STOP_AREA_VALUE && _isTracking)
        {
            _isTracking = true;
        }
    }

    /// <summary>
    /// カメラの挙動制御
    /// </summary>
    private void MoveCamera()
    {
        //開始
        if (_isTracking)
        {
            
        }

        //終了
        else
        {

        }
    }
}