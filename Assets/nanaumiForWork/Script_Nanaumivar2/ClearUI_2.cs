using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI_2 : MonoBehaviour
{
    private InputController _inputs = default;

    /// <summary>
    /// 遷移するフラグ
    /// </summary>
    private bool _isFade = true;
    /// <summary>
    /// ロードされるのかタイトルをロードするのかフラグ trueでタイトルのロード
    /// </summary>
    private bool _isBlackFade = false;
    /// <summary>
    /// フェードする時間
    /// </summary>
    private float _fadeTime = 1;

    [SerializeField] private Image _fadeImage = default;
    /// <summary>
    /// フェードするときのImageに黒＋α値を設定する
    /// </summary>
    private Color _fadeImageColor = new Color(0, 0, 0, 1);
    /// <summary>
    /// フェードするときのα値
    /// </summary>
    private float _fadeAlpha = default;

    private UISceneManager_2 sceneMan = default;

    /// <summary>
    /// MemoryAchivementイベントの取得
    /// </summary>
    private MemoryAchievementController memoAchi = default;
    [SerializeField] private Text textAchi = default;

    /// <summary>
    /// ScreenShotを表示するImage
    /// </summary>
    [SerializeField] private Image[] omoide = default;

    private AudioManager audios = default;

    private void Awake()
    {
        // Audioの取得
        audios = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // ScreenShotをランダムで取得、設定
        omoide[0].sprite = audios.sprite[Random.Range(0, audios.sprite.Count)];
        omoide[1].sprite = audios.sprite[Random.Range(0, audios.sprite.Count)];
        omoide[2].sprite = audios.sprite[Random.Range(0, audios.sprite.Count)];
        omoide[3].sprite = audios.sprite[Random.Range(0, audios.sprite.Count)];

        _inputs = new InputController();
        sceneMan = this.gameObject.AddComponent<UISceneManager_2>();
        memoAchi = GameObject.FindWithTag("EventSystem").GetComponent<MemoryAchievementController>();
    }

    private void Start()
    {
        // 記憶達成度をTextに設定
        textAchi.text = memoAchi._nowMemoryToral.ToString() + " %";
    }

    private void Update()
    {
        // 遷移するフラグ
        if (_isFade)
        {
            // 透明から黒
            if (_isBlackFade)
            {
                _fadeTime += Time.deltaTime * 4;
                if (_fadeTime >= 1)
                {
                    _isFade = false;
                }
            }
            // 黒から透明
            else if (!_isBlackFade)
            {
                _fadeTime -= Time.deltaTime * 4;
                if(_fadeTime <= 0)
                {
                    _isFade = false;
                }
            }

            // FadeImageの設定
            _fadeAlpha = _fadeTime;
            _fadeImage.color = _fadeImageColor * _fadeAlpha;
        }

        // 決定押されたとき
        if (!_isFade && _inputs.UI.Decide.triggered)
        {
            // タイトルのロード
            sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.GameClear));
            _isFade = true;
            _isBlackFade = true;
        }
    }

    private void OnEnable()
    {
        _inputs.Enable();

        // Audio
        audios.bgm = (AudioManager.BGM)2;
        audios.AudioChanger("BGM");
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }
}
