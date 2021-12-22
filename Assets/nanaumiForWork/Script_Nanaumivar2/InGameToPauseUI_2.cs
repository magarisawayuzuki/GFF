using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameToPauseUI_2 : UIController_2
{
    private bool _isPause = false;

    private void Update()
    {
        if (_inputs.UI.Pause.triggered)
        {
            _isPause = true;
        }

        if (!ContainsScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause)))
        {
            _isPause = false;
        }

        ToPause();
    }

    private void ToPause()
    {
        if (_isPause && !ContainsScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause)))
        {
            sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), false, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame));
        }
    }

    private bool ContainsScene(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
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
