using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CautionPanel_2 : UIController_2
{
    [SerializeField] private string cautionSetence;
    [SerializeField] private Text cautionText;

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
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                }
                // Back to Title
                else /*if (getSceneName == SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame))*/
                {
                    SceneManager.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title),LoadSceneMode.Single);
                }
                break;

            // No
            case 2:
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
