using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI_2 : UIController_2
{
    [SerializeField] private string InGameSceneName;
    [SerializeField] private string OptionSceneName;

    [SerializeField] private GameObject cautionPanel;
    [SerializeField] private CautionPanel_2 caucau;
    [SerializeField] private Image bookimage;
    private float _imageFlipTime = default;

    private void Start()
    {
        bookimage.material.SetFloat("_Flip", 1f);

        _nowSelectNumber = 1;

        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude;
    }

    private void Update()
    {
        if (_imageFlipTime <= 2)
        {
            bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") - Time.deltaTime);
            _imageFlipTime += Time.deltaTime;
        }
        else
        {
            InputManager();
        }

        if (_isInput[1])
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
                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title));
                break;
            // to Option
            case 2:
                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title));
                break;
            // Exit Game to CautionPanel
            case 3:
                cautionPanel.SetActive(true);
                caucau.enabled = true;
                this.enabled = false;
                break;
        }

        _isInput[1] = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.Title;
    }
}
