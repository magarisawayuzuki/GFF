using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneStateUI_2
{
    public static SceneState sceneState = SceneState.Title;

    public static string SceneName(this SceneState sceneState)
    {
        switch (sceneState)
        {
            case SceneState.Title:
                return "Title";
            case SceneState.Pause:
                return "Pause";
            case SceneState.InGame:
                return "InGame";
            case SceneState.Option:
                return "Option";
            default:
                return "";
        }
    }

    public enum SceneState
    {
        Title,
        Pause,
        InGame,
        Option
    }
}
