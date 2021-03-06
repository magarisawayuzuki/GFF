using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneStateUI_2
{
    public static SceneState sceneState = SceneState.Title;

    public static int SceneName(this SceneState sceneState)
    {
        switch (sceneState)
        {
            case SceneState.Title:
                return 0;
            case SceneState.Pause:
                return 1;
            case SceneState.InGame:
                return 2;
            case SceneState.Option:
                return 3;
            case SceneState.Option2:
                return 4;
            case SceneState.Main:
                return 5;
            case SceneState.GameClear:
                return 6;
            default:
                return 10;
        }
    }

    public enum SceneState
    {
        Title,
        Pause,
        InGame,
        Option,
        Option2,
        Main,
        GameClear
    }
}
