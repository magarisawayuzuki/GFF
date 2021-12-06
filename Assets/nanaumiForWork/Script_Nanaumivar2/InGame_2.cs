using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGame_2 : UIController_2
{
    private bool _isPause = false;

    private void Update()
    {
        if (_inputs.UI.Pause.triggered && !_isPause)
        {
            _isPause = true;
            sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), false, null);
        }

        if(SceneStateUI_2.sceneState == SceneStateUI_2.SceneState.InGame)
        {
            _isPause = false;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isPause = false;
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.InGame;
    }
}
