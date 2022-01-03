using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Threading.Tasks;


public class CreateStageEditor : MonoBehaviour
{
    [Header("変換元画像")]
    [SerializeField] private Texture2D texture;

    [Header("生成オブジェクト")]
    [SerializeField] private GameObject instanceObject = default;
    [SerializeField] private GameObject checkPointObject = default;

    [Header("識別色")]
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<List<int>> result;

    [Header("出力形式")]
    [SerializeField] private bool EnableExportTextFile = true;
    [SerializeField] private bool EnableExportConsole = false;
    [SerializeField] private bool EnableGenerateStage = false;

    [Header("出力設定")]
    [SerializeField] private bool ReverseArrayHorizontal = false;



    [ContextMenu("Generate")]
    public async void GetPixel()
    {
        //Map配列定義
        int[,] mapInfo = new int[texture.height, texture.width];

        Debug.Log("変換開始");

        //Textに出力する値を格納する変数
        //今回の要素数を出力する
        string debug = "要素数(" + texture.height + "|" + texture.width + ")" + "\n";
        result = new List<List<int>>();

        for (int i = 0; i < texture.height; i++)
        {
            //行頭
            debug += "{ ";

            List<int> temp = new List<int>();

            //行の判定開始
            for (int l = 0; l < texture.width; l++)
            {
                if (l != 0)
                {
                    debug += ", ";
                }

                Color color = texture.GetPixel(l, i);
                if (colors.Contains(color))
                {
                    int index = colors.IndexOf(color);
                    temp.Add(index);
                    debug += index.ToString();

                    //indexをi列,l行に格納
                    mapInfo[i, l] = index;
                }

                //画像の色が設定されたカラーに当てはまらなかった
                else
                {
                    debug += "-1";

                    mapInfo[i, l] = -1;
                    Debug.LogWarning("ColorError");
                }
            }
            result.Add(temp);

            //次の行へ
            debug += "} \n";
            await Task.Delay(1);
        }

        //コンソールに出力する
        if (EnableExportConsole)
        {
            Debug.Log(debug);
        }

        //テキストに出力する
        if (EnableExportTextFile)
        {
            ExportText(debug);
        }
        print(mapInfo.Length);

        //ステージを生成する
        if (EnableGenerateStage)
        {
            //上下反転
            if (ReverseArrayHorizontal)
            {
                GenerateObject(mapInfo);
            }
            //そのまま
            else
            {
                GenerateObject(mapInfo.ReverseArray());
            }
        }
        Debug.Log("生成終了");
    }


    /// <summary>
    /// テキスト形式で出力する
    /// </summary>
    /// <param name="debug"></param>
    private void ExportText(string debug)
    {
        StreamWriter sw = new StreamWriter("../MapData.txt", false);
        sw.WriteLine(debug);
        sw.Flush();
        sw.Close();
    }


    /// <summary>
    /// ステージを生成
    /// </summary>
    /// <param name="mapInfo"></param>
    private void GenerateObject(int[,] mapInfo)
    {

        //親オブジェクト生成
        GameObject parentObj = new GameObject("Stage");
        GameObject checkParentObj = new GameObject("CheckPoint");

        Quaternion QUATERNION = new Quaternion(0,0,0,0);

        int yConsecutiveLength = 0;
        int xConsecutiveLength = 0;

        int min_xConsecutiveLength = mapInfo.GetLength(1) - 1;

        bool _isSpaceOneSide = false;
        bool _isInstanced = false;

        //↑
        //→→

        //縦方向
        for (int i = mapInfo.GetLength(0) - 1; i >= 0; i--)
        {
            GameObject nowCreateObject = default;
            //横方向
            for (int j = 0; j < mapInfo.GetLength(1); j++)
            {
                //今の位置にブロックがあるなら
                if (mapInfo[i, j] == 1 || mapInfo[i,j] == 2)
                {
                    //初期生成
                    if(nowCreateObject == default)
                    {
                        nowCreateObject = Instantiate(instanceObject,new Vector3(j, (mapInfo.GetLength(0) - 1) - i, 0), QUATERNION, parentObj.transform);
                    }

                    //生成済みなら
                    else
                    {
                        //xを1大きくする
                        nowCreateObject.transform.localScale += Vector3.right;

                        //位置調整
                        nowCreateObject.transform.position += Vector3.right * 0.5f;
                    }
                }
                else
                {
                    //ブロックの更新を終了
                    nowCreateObject = default;
                }

                //チェックポイント生成
                if(mapInfo[i,j] == 11)
                {
                    Instantiate(checkPointObject, new Vector3(j, (mapInfo.GetLength(0) - 1) - i, 0), QUATERNION, checkParentObj.transform);
                }
            }
            nowCreateObject = default;
        }




        //シーンを保存
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    }
}



static class ArrayRotation
{
    /// <summary>
    /// 配列の要素を上下反転させる
    /// </summary>
    public static int[,] ReverseArray(this int[,] array)
    {
        int[,] work = array.Clone() as int[,];
        int xLeng = work.GetLength(0);
        
        //入れ替えなので半分まで
        for (int i = 0; i < xLeng / 2; i++)
        {
            for (int l = 0; l < work.GetLength(1); l++)
            {
                //入れ替え処理
                int temp = work[i, l];
                work[i, l] = work[xLeng - i - 1, l];
                work[xLeng - i - 1, l] = temp;
            }
        }
        return work;
    }
}
