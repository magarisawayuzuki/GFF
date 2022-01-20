using UnityEngine;
using UnityEngine.UI;

public class Chara_2 : MonoBehaviour
{
    /// <summary>
    /// 最大Life
    /// </summary>
    private float _maxLife = default;
    /// <summary>
    /// ダメージ受ける前のLife保持
    /// </summary>
    private float _beforeLife = default;
    /// <summary>
    /// ダメージ受けた後のLife保持
    /// </summary>
    private float _afterLife = default;

    /// <summary>
    /// HPbarのダメージ分とダメージ差分を減らす2本のゲージ
    /// </summary>
    [SerializeField] protected RectMask2D[] _HPScroll = default;

    /// <summary>
    /// Lifeの減少するLerpの速度
    /// </summary>
    private const float _LIFECHANGEMAGNITUDE = 2f;
    /// <summary>
    /// Lerp時間
    /// </summary>
    private float _lerpTime = default;
    /// <summary>
    /// Lerp倍率
    /// </summary>
    private const float _LERPMAGNITUDE = 2.53f;

    /// <summary>
    /// HPのMaskPadding用のVector4
    /// </summary>
    protected Vector4 _vectorHP = new Vector4(0, 0, 1, 0);

    /// <summary>
    /// キャラの初期パラメータを設定する処理
    /// </summary>
    /// <param name="charaPara">CharacterControllerを設定</param>
    protected void SetChara(CharacterController charaPara)
    {
        _beforeLife = 0;
        _maxLife = charaPara.GetLife;
    }

    /// <summary>
    /// ダメージ時のLifeBarの変更
    /// </summary>
    /// <param name="charaPara">CharacterControllerの設定</param>
    /// <param name="isDamage">ダメージを受けているか</param>
    protected virtual void ChangeLife(CharacterController charaPara, bool isDamage)
    {
        // afterLifeの更新
        _afterLife = _maxLife - charaPara.GetLife;
        // すぐ減らす方のHPBarを減らす
        _HPScroll[0].padding = _vectorHP * _afterLife * _LERPMAGNITUDE;

        // ダメージを受けてないときにダメージ差分をゆっくり減らす
        if (!isDamage)
        {
            if (_lerpTime <= 1f)
            {
                _lerpTime += Time.deltaTime * _LIFECHANGEMAGNITUDE * 2;
                _HPScroll[1].padding = Vector4.Lerp(_vectorHP * _beforeLife * _LERPMAGNITUDE, _vectorHP * _afterLife * _LERPMAGNITUDE, _lerpTime);
            }
            else
            {
                // beforeLifeの更新
                _beforeLife = _afterLife;
            }
        }
        else if(isDamage)
        {
            // Lerpを動かなくする
            _lerpTime = 0;
        }
    }
}
