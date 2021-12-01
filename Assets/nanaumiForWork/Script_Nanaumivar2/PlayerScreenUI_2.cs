using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScreenUI_2 : Chara_2
{
    private int nowGaugeParcent;
    [SerializeField, Tooltip("playerのタグを入れてください")] private string playerTag;

    [SerializeField] private string PauseSceneName;

    private InputController inputs;
    private UISceneManager_2 sceneMan;

    protected override void Awake()
    {
        base.Awake();
        nowGaugeParcent = 0;

        inputs = new InputController();
        sceneMan = new UISceneManager_2();
    }

    private void Update()
    {
        // 親メソッド
        //ChangeLife(_chara.life);

        // 子メソッド
        OnPause();
        //ChangeMemoryGaugeUI();
        //ChangeWeaponUI();
        //ChangeMemoryAchivementUI();
        //ChangePlayerLife();
    }

    private void OnPause()
    {
        if (inputs.UI.Pause.triggered && !ContainsScene(PauseSceneName))
        {
            sceneMan.LoadScene(PauseSceneName, false, null);
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
        ChangeLife();
    }
}