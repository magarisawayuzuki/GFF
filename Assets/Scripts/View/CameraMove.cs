using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*　～～使用方法～～
Rigidbody(Is KinematicをTrueに)とColliderが付いたCameraにアタッチして使う
平行移動だけしたいAreaにCollider(Is TriggerをTrueに)がついたオブジェクトを配置しTagを"HorizontalArea"に変更
    ※このオブジェクトのTransform.Position.yの値に平行移動時の、カメラのy座標の値を入れておく※
垂直移動をしたいAreaにも同様に配置しTagを"VerticulArea"に変更
*/

public class CameraMove : MonoBehaviour
{
    private GameObject playerChara = default;

    //タグの名前
    private const string HORIZONTAL_AREA_TAG_NAME = "HorizontalArea";
    private const string VERTICUL_AREA_TAG_NAME = "VerticulArea";
    
    [Header("カメラの基本の追従速度"),SerializeField]
    private float CAMERA_MOVE_SPEED = 0.04f;

    [Header("カメラの最大速度"),SerializeField]
    private float MAX_CAMERA_MOVE_SPEED = 0.03f;

    //上下にカメラが動く場所か
    [SerializeField]
    private bool _inVerticalArea = false;
    private bool _BossArea = false;

    /// <summary>
    /// 平行移動時に参照するカメラのY座標
    /// </summary>
    private float AreaYpos;

    /// <summary>
    /// Z座標退避用
    /// </summary>
    private float defaultCameraPos_z = default;

    private Vector3 bossStartPos = default;
    private Vector3 bossEndPos = new Vector3(986, 52, -10);
    private float distance = default;
    private new Camera camera;
    [SerializeField]
    private float bossCameraSizeSpeed = 0.1f;
    private float bossCameraMoveSpeed = 0.2f;
    [SerializeField]
    private GameObject kabe = default;

    [SerializeField] private Image _flashImage = default;
    private float _flashAlpha = default;

    private void Start()
    {
        camera = this.GetComponent<Camera>();
        playerChara = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(bossStartPos, bossEndPos);
        defaultCameraPos_z = this.gameObject.transform.position.z;

        _flashAlpha = 0;
        _flashImage.color = Color.white * _flashAlpha;
    }

    private void FixedUpdate()
    {
        if (_BossArea )
        {
            if (this.transform.position != bossEndPos || camera.orthographicSize != 18)
            {
                this.gameObject.transform.position = Vector3.MoveTowards(this.transform.position,bossEndPos,bossCameraMoveSpeed);
                if(camera.orthographicSize >= 18) { return; }
                camera.orthographicSize += bossCameraSizeSpeed * 0.03f;

                if (_flashAlpha == 0)
                {
                    Invoke("SceneClear", 2f);
                }

                _flashAlpha += Time.deltaTime;
                _flashImage.color = Color.white * _flashAlpha;
            }

            return;
        }
        CheckDistance();
    }

    private void SceneClear()
    {
        SceneManager.LoadSceneAsync(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.GameClear));
    }

    /// <summary>
    /// 距離の差を求めてフラグを管理
    /// </summary>
    private void CheckDistance()
    {
        //カメラの位置
        Vector3 cameraNowPos = this.gameObject.transform.position;
        //z座標を補完したカメラの位置
        Vector3 adjustmentCameraPos = new Vector3(cameraNowPos.x, cameraNowPos.y, cameraNowPos.z * 0);

        //二点の差
        float distance = Vector3.Distance(adjustmentCameraPos, playerChara.transform.position);

        //Distanceを基にカメラを動かす
        MoveCamera(distance,adjustmentCameraPos);
    }

    /// <summary>
    /// カメラの挙動制御
    /// </summary>
    private void MoveCamera(float distance, Vector3 adjustmentCameraPos)
    {
        //現在のプレイヤーの座標
        Vector3 playerDefaultPos = playerChara.transform.position;
        //Y座標を固定値に変換したプレイヤーの座標
        Vector3 playerHorizontalPos = new Vector3(playerChara.transform.position.x, AreaYpos , playerChara.transform.position.z);
        //今回使う座標
        Vector3 usePlayerPos = default;

        float adjustmentSpeed = distance * CAMERA_MOVE_SPEED;

        //移動速度の抑制
        if(adjustmentSpeed <=　MAX_CAMERA_MOVE_SPEED)
        {
            adjustmentSpeed = MAX_CAMERA_MOVE_SPEED;
        }

        //カメラの上下動作無し
        if (!_inVerticalArea)
        {
            usePlayerPos = playerHorizontalPos;
        }
        //カメラの上下動作あり
        else
        {
            usePlayerPos = playerDefaultPos;
        }

        //メイン挙動処理
        //Lerp処理後にZ座標を戻す
        this.gameObject.transform.position = Vector3.Lerp(adjustmentCameraPos, usePlayerPos, adjustmentSpeed)
                                                 + (Vector3.forward * defaultCameraPos_z);
    }


    //Area判定
    private void OnTriggerStay(Collider other)
    {
        //平行移動エリア
        if (other.gameObject.tag == HORIZONTAL_AREA_TAG_NAME)
        {
            _inVerticalArea = false;
            //Areaのコライダーに設定してあるY座標値を取ってくる
            AreaYpos = other.transform.position.y;
        }

        //垂直移動エリア
        else if (other.gameObject.tag == VERTICUL_AREA_TAG_NAME)
        {
            _inVerticalArea = true;
        }

        else if(other.gameObject.tag == "BossArea")
        {
            bossStartPos = playerChara.transform.position;
            _BossArea = true;
            kabe.gameObject.SetActive(true);
        }
    }
}