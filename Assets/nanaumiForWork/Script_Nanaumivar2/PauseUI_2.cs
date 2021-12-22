using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI_2 : UIController_2
{
    [SerializeField] private GameObject cautionPanel;
    [SerializeField] private CautionPanel_2 caucau;

    private void Start()
    {
        _nowSelectNumber = 1;

        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude;
    }

    private void Update()
    {
        InputManager();

        if (_isInput[1])
        {
            TitleSelector();
        }
    }

    private void TitleSelector()
    {
        switch (_nowSelectNumber)
        {
            // Back to InGame
            case 1:
                SceneManager.UnloadSceneAsync(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause));
                break;
            // Back to Title to CautionPanel
            case 2:
                cautionPanel.SetActive(true);
                caucau.enabled = true;
                this.enabled = false;
                break;
            // to Option
            case 3:
                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause));
                break;
        }

        _isInput[1] = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.Pause;
    }
}
