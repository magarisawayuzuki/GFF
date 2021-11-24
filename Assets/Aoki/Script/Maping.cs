using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maping : UnityEngine.MonoBehaviour
{
    //0空気　1床　2壁　3ジャンプエリア 4行けないエリア　8エネミー
    public int[,] stageArray = new int[10, 30]{
        //0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,
        { 1,1,1,1,1,1,1,4,4,4, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1,0,0,0,0,0,0,4,4,4, 2, 2, 3, 0, 0, 0, 0, 0, 8, 0, 0, 0, 3, 2, 2, 2, 2, 2, 2, 4 },
        { 1,0,0,0,0,0,0,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,3 ,2 ,2 ,2 ,4 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,4 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,4 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 },
        { 1,0,0,0,0,0,0,0,0,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 },
    };



    public GameObject floorBlock;
    public GameObject wallBlock;


    /// <summary>
    /// 開始時ステージ生成
    /// </summary>
    void Start()
    {

        for (int i = 0; i < stageArray.GetLength(0); i++)
        {
            for (int j = 0; j < stageArray.GetLength(1); j++)
            {
                if (stageArray[i, j] == 1)
                {
                    Instantiate(floorBlock, new Vector3(j, i, 0), Quaternion.identity);
                }
                if (stageArray[i, j] == 2)
                {
                    Instantiate(wallBlock, new Vector3(j, i, 0), Quaternion.identity);

                }
            }
        }
    }
}
