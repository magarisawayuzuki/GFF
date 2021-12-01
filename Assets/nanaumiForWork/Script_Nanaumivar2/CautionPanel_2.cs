using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CautionPanel_2 : UIController_2
{
    [SerializeField] private string cautionSetence;
    [SerializeField] private Text cautionText;

    [SerializeField] private string TitleSceneName;
    [SerializeField] private string InGameSceneName;

    private void Start()
    {
        cautionText.text = cautionSetence;
    }

    private void Update()
    {
        Debug.Log("AAA");
        InputManager();

        if (_isInput[1])
        {
            CautionPanel(SceneManager.GetActiveScene().name);
        }
    }

    private void CautionPanel(string getSceneName)
    {
        switch (_nowSelectNumber)
        {
            // Yes
            case 1:
                // Exit Game
                if (getSceneName == TitleSceneName)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                }
                // Back to Title
                else if (getSceneName == InGameSceneName)
                {
                    sceneMan.LoadScene(TitleSceneName, true, getSceneName);
                }
                break;

            // No
            case 2:
                // Back to Title
                if (getSceneName == TitleSceneName)
                {
                    GetComponentInParent<TitleUI_2>().enabled = true;
                }
                // Back to Pause
                else if (getSceneName == InGameSceneName)
                {
                    GetComponentInParent<PauseUI_2>().enabled = true;
                }

                this.gameObject.SetActive(false);
                this.enabled = false;
                _isInput[1] = false;
                _nowSelectNumber = 1;
                break;
        }
    }

    protected override void OnEnable()
    {
        Debug.Log("Enable");
        base.OnEnable();
        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta;
    }
}
