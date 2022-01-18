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

    private AudioManager audios = default;

    private void Awake()
    {
        audios = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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

    private void OnEnable()
    {
        _inputs.Enable();

        // Audioの設定
        audios.bgm = (AudioManager.BGM)2;
        audios.AudioChanger("BGM");
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }
}
