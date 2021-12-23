using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI_2 : UIController_2
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

        if(_isInput[1])
        {
            TitleSelector();
        }
    }

    private void TitleSelector()
    {
        switch (_nowSelectNumber)
        {
            // to InGame
            case 1:
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    bookimage.material.SetFloat("_Flip", -1f);

                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Main), false, 10);
                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title));
                }

                if (_imageFlipTime >= 0)
                {
                    bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
                    _imageFlipTime -= Time.deltaTime;
                }
                else
                {
                    _isInput[1] = false;
                }
                break;
            // to Option
            case 2:
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    bookimage.material.SetFloat("_Flip", -1f);

                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title));
                }

                if (_imageFlipTime >= 0)
                {
                    bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
                    _imageFlipTime += Time.deltaTime;
                }
                else
                {
                    _isInput[1] = false;
                }
                break;
            // Exit Game to CautionPanel
            case 3:
                cautionPanel.SetActive(true);
                caucau.enabled = true;
                this.enabled = false;

                _isInput[1] = false;
                _isFlip = false;
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isFlip = false;
        _isLoaded = false;
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.Title;
    }
}
