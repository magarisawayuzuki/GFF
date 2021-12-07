using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScreenUI_2
{

    // ② SpoonやForkからSporkに変換するメソッド
    public static PlayerScreenUI_2 FromInGameUI_2(InGameUI_2 ingame)
    {
        return (ingame as InGameUI_2)?.plsc;
    }

    public static PlayerScreenUI_2 FromtoPause(PlayerInputToPause_2 topause)
    {
        return (topause as PlayerInputToPause_2)?.plsc;
    }

    public class InGameUI_2 : Chara_2
    {
        public PlayerScreenUI_2 plsc;

        private int nowGaugeParcent;
        [SerializeField, Tooltip("playerのタグを入れてください")] private string playerTag;

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
            ChangeLife();
        }
    }

    public class PlayerInputToPause_2 : UIController_2
    {
        public PlayerScreenUI_2 plsc;

        private bool _isPause = false;

        protected void OnPause()
        {
            if (_inputs.UI.Pause.triggered && !_isPause && !ContainsScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause)))
            {
                _isPause = true;
                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), false, null);
            }

            if (SceneStateUI_2.sceneState == SceneStateUI_2.SceneState.InGame)
            {
                _isPause = false;
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

        protected override void OnEnable()
        {
            base.OnEnable();
            _isPause = false;
            SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.InGame;
        }
    }


    private FromInGameUI_2 a = new InGameUI_2());

    protected override void Awake()
    {

    }

    private void Start()
    {
        // ingameMethod
        nowGaugeParcent = 0;

        // toPauseMethod
        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta;
    }

    private void Update()
    {


        // 親メソッド
        //ChangeLife(_chara.life);

        // 子メソッド
        //ChangeMemoryGaugeUI();
        //ChangeWeaponUI();
        //ChangeMemoryAchivementUI();
        //ChangePlayerLife();
    }
}