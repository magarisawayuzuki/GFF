using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI_2 : MonoBehaviour
{
    private InputController _inputs = default;

    private bool _isFade = true;
    private bool _isBlackFade = false;
    private float _fadeTime = 1;

    [SerializeField] private Image _fadeImage = default;
    private Color _fadeImageColor = new Color(0, 0, 0, 1);
    private float _fadeAlpha = default;

    private UISceneManager_2 sceneMan = default;

    private MemoryAchievementController memoAchi = default;
    [SerializeField] private Text textAchi = default;

    [SerializeField] private Image[] omoide = default;

    private void Awake()
    {
        StartCoroutine(LoadScreenshot());

        _inputs = new InputController();
        sceneMan = this.gameObject.AddComponent<UISceneManager_2>();
        memoAchi = GameObject.FindWithTag("EventSystem").GetComponent<MemoryAchievementController>();
    }

    private void Start()
    {
        textAchi.text = memoAchi._nowMemoryToral.ToString() + " %";
    }

    private void Update()
    {
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

            _fadeAlpha = _fadeTime;
            _fadeImage.color = _fadeImageColor * _fadeAlpha;
        }

        if (!_isFade && _inputs.UI.Decide.triggered)
        {
            sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.GameClear));
            _isFade = true;
            _isBlackFade = true;
        }
    }


    private IEnumerator LoadScreenshot()
    {
        //Unityのレンダリングを待つ
        yield return new WaitForEndOfFrame();

        //画面サイズと同じテクスチャ変数を用意する
        var texture = new Texture2D(Screen.width, Screen.height);
        //画面のピクセルデータを用意したテクスチャ変数に書き写す
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //テクスチャの変更分を反映する
        texture.Apply();
        //扱いやすいようにTexture2D型からSprite型に変換する
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        //sprite <--- この変数にスクショした画像データが入っている

        omoide[0].sprite = sprite;
        omoide[1].sprite = sprite;
        omoide[2].sprite = sprite;
        omoide[3].sprite = sprite;
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }
}
