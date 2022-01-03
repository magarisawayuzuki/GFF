using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CautionPanel_2 : UIController_2
{
    [SerializeField] private string cautionSetence = default;
    [SerializeField] private Text cautionText = default;

    private void Start()
    {
        cautionText.text = cautionSetence;

        _isFlip = true;
        bookimage.material.SetFloat("_Flip", -1f);
    }

    private void Update()
    {
        InputManager();

        if (_isInput[1])
        {
            CautionPanel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CautionPanel(int getSceneName)
    {
        switch (_nowSelectNumber)
        {
            // Yes
            case 1:
                // Exit Game
                if (getSceneName == SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title))
                {
                    audios.uiSE = (AudioManager.UISE)1;
                    audios.AudioChanger("UI");
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                }
                // Back to Title
                else /*if (getSceneName == SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame))*/
                {
                    InGameToPauseUI_2._isStaticPause = false;
                    InGameToPauseUI_2._isPause = false;
                    audios.uiSE = (AudioManager.UISE)4;
                    audios.AudioChanger("UI");
                    SceneManager.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title),LoadSceneMode.Single);
                }
                break;

            // No
            case 2:
                audios.uiSE = (AudioManager.UISE)2;
                audios.AudioChanger("UI");
                // Back to Title
                if (getSceneName == SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title))
                {
                    GetComponentInParent<TitleUI_2>().enabled = true;
                }
                // Back to Pause
                else /*if (getSceneName == PauseSceneName)*/
                {
                    GetComponentInParent<PauseUI_2>().enabled = true;
                }

                _isInput[1] = false;
                this.gameObject.SetActive(false);
                this.enabled = false;
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _nowSelectNumber = 1;
        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude;
    }
}
