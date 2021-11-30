using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScreenUI : Chara
{
    private int nowGaugeParcent;
    [SerializeField, Tooltip("playerのタグを入れてください")] private string playerTag;

    [SerializeField] private Text pauseText;
    [SerializeField] private Text nowselectText;

    private PauseUI pauseUI;

    protected override void Awake()
    {
        base.Awake();
        nowGaugeParcent = 0;
        //_chara = GameObject.FindGameObjectWithTag(playerTag).GetComponent<CharaParameter>();
        //_beforeLife = _chara.life;

        pauseUI = this.GetComponent<PauseUI>();
    }

    private void Update()
    {
        // 親メソッド
        //ChangeLife(_chara.life);

        // 子メソッド
        InputManager();
        //ChangeMemoryGaugeUI();
        //ChangeWeaponUI();
        //ChangeMemoryAchivementUI();
        //ChangePlayerLife();
    }

    protected override void InputManager()
    {
        if (_isPause)
        {
            pauseText.text = "Pause";
            if (!ContainsScene("Pause"))
            {
                SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
            }
            this.enabled = false;
            base.InputManager();
        }
        else
        {
            pauseText.text = "unPause";
        }

        if (_inputs.UI.Pause.triggered)
        {
            _isPause = !_isPause;
        }
    }

    private bool ContainsScene(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount - 1; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeMemoryGaugeUI()
    {

    }

    private void ChangeWeaponUI()
    {

    }

    private void ChangeMemoryAchivementUI()
    {

    }

    private void ChangePlayerLife()
    {

    }
}