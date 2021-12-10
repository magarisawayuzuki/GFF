using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : UIController
{
    [SerializeField] private Text pauseText;
    [SerializeField] private Text nowselectText;

    private PlayerScreenUI inGameUI;

    protected override void Awake()
    {
        base.Awake();
        inGameUI = this.GetComponent<PlayerScreenUI>();
    }

    protected override void Start()
    {
        base.Start();

        _isPause = true;
    }

    private void OnEnable()
    {

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

    int i = 0;
    private void Update()
    {
        if(i == 0)
        {
            Debug.Log("AAA");
            if (!ContainsScene("Option"))
            {
                SceneManager.LoadSceneAsync("Option", LoadSceneMode.Additive).allowSceneActivation = true;
                i++;
            }
        }

        InputManager();
    }

    protected override void InputManager()
    {
        if (_isPause)
        {
            pauseText.text = "Pause";
            base.InputManager();
        }
        else
        {
            pauseText.text = "unPause";
            this.enabled = false;
        }

        if (_inputs.UI.Pause.triggered)
        {
            _isPause = !_isPause;
        }
    }
}
