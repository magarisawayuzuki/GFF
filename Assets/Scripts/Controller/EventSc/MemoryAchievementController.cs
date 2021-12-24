using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 記憶の達成度を管理
/// </summary>
public class MemoryAchievementController : EventController
{
    private const int MEMORY_LEVEL_CHENGE_CONSTANCE_VALUE = 25;

    private float nowMemoryTotal = default;
    /// <summary>
    /// 記憶の累計度数のプロパティ
    /// </summary>
    public float _nowMemoryToral
    {
        set { nowMemoryTotal = value; }
        get { return nowMemoryTotal; }
    }

    private int nowMemoryLevel = default;
    /// <summary>
    /// 現在の記憶達成レベル
    /// </summary>
    public int _nowMemoryLevel
    {
        get { return nowMemoryLevel; }
    }


    /// <summary>
    /// 記憶達成度の判定を行う
    /// </summary>
    public void MemoryRateJudgment()
    {
        int judge = default;

        judge = Mathf.FloorToInt(nowMemoryTotal / MEMORY_LEVEL_CHENGE_CONSTANCE_VALUE);

        //達成度判定
        switch (judge)
        {
            //0～24%
            case 0:
                nowMemoryLevel = 0;
                return;

            //25～49%
            case 1:
                nowMemoryLevel = 1;
                return;

            //50～74%
            case 2:
                nowMemoryLevel = 2;
                return;

            //75～99%
            case 3:
                nowMemoryLevel = 3;
                return;

            //100%
            case 4:
                nowMemoryLevel = 4;
                return;
        }
    }
}
