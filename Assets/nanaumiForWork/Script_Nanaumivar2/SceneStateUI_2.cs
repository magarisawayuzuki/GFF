using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneStateUI_2
{
        public static string Name(this SceneStateUI_2.SceneState musicType)
        {
            switch (musicType)
            {
                case SceneState.Title:
                    return "ぽっぷ";
                case SceneState.Pause:
                    return "じゃず";
                case SceneState.InGame:
                    return "ろっく";
                default:
                    return "";
            }
        }
    public enum SceneState
    {
        Title,
        Pause,
        InGame
    }
}
