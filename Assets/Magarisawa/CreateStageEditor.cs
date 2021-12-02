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
    [SerializeField] private GameObject instanceObject;

    [Header("識別色")]
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<List<int>> result;

    [Header("出力形式")]
    [SerializeField] private bool EnableExportTextFile = true;
    [SerializeField] private bool EnableExportConsole = false;
    [SerializeField] private bool EnableGenerateStage = false;

    [Header("出力設定")]
    [SerializeField] private bool ReverseArrayHorizontal = false;

    [ContextMenu("test")]
    public void test()
    {
        
    }


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
                GenerateObject(mapInfo.ReverseArray());
            }
            //そのまま
            else
            {
                GenerateObject(mapInfo);
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

        int yConsecutiveLength = 0;
        int xConsecutiveLength = 0;

        int min_xConsecutiveLength = mapInfo.GetLength(1) - 1;

        bool _isSpaceOneSide = false;
        bool _isInstanced = false;

        //メイン縦方向
        for (int i = mapInfo.GetLength(0) - 1; i >= 0; i--)
        {
            //メイン横方向
            for (int j = 0; j < mapInfo.GetLength(1); j++)
            {
                yConsecutiveLength = 0;
                xConsecutiveLength = 0;

                min_xConsecutiveLength = mapInfo.GetLength(1) - 1;

                _isSpaceOneSide = false;
                _isInstanced = false;

                //今の位置にブロックがあるなら
                if (mapInfo[i, j] == 1)
                {
                    //print("mapInfo[" +i+","+ j+"]" + "true");

                    //連続判定縦方向
                    for (int k = i; k >= 0; k--)
                    {
                        //生成済みなら変数を初期化し終了
                        if (_isInstanced)
                        {

                            break;
                        }

                        //縦ブロック数加算
                        yConsecutiveLength++;

                        //下が空白もしくは要素の最後なら次の横の空白で生成する
                        if (k == 0 ||　mapInfo[k-1, j] == 0)
                        {
                            _isSpaceOneSide = true;
                        }

                        //連続判定横方向
                        for (int l = j; l < mapInfo.GetLength(1); l++)
                        {
                            //横ブロック数宇加算
                            xConsecutiveLength++;

                            //右が空白もしくは要素の最後なら
                            if (l == mapInfo.GetLength(1) - 1 || mapInfo[k,l+1] == 0)
                            {
                                //最小値を保存しておく
                                if (min_xConsecutiveLength > xConsecutiveLength)
                                {
                                    min_xConsecutiveLength = xConsecutiveLength;
                                }


                                //まだ下方向に続いている
                                if (!_isSpaceOneSide)
                                {
                                    break;
                                }

                                //下と右の限界まで探索した
                                else
                                {
                                    //print("gene");

                                    //生成
                                    GameObject newGameObject = Instantiate(instanceObject, parentObj.transform);

                                    //サイズをブロックの連続数に変更
                                    newGameObject.transform.localScale = new Vector3(min_xConsecutiveLength, yConsecutiveLength, 1);

                                    //print(yConsecutiveLength);
                                    //print(i);
                                    //print(i - ((yConsecutiveLength - 1) / 2));

                                    //print(j);
                                    //print(i);

                                    //座標をX=(j + ((Scale - 1) /2 )),Y=(i - ((Scale - 1) /2 ))に変更
                                    newGameObject.transform.position = new Vector3(j + ((min_xConsecutiveLength - 1) / 2), Mathf.Abs(i - mapInfo.GetLength(0)) - ((yConsecutiveLength - 1) / 2), 0);

                                    //生成終了フラグ
                                    _isInstanced = true;
                                    //今回x座標上で進んだ分探索を飛ばす
                                    j += min_xConsecutiveLength - 1;

                                    


                                    //メイン探索位置から今回ブロックをまとめて生成する位置に探索済みの印をつける
                                    for (int m = 0; m < yConsecutiveLength - 1; m++)
                                    {
                                        for (int n = 0; n < min_xConsecutiveLength - 1; n++)
                                        {
                                            //探索済みの印
                                            mapInfo[i - m,j + n] = -1;
                                        }
                                    }
                                    mapInfo[i, j] = -1;

                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //print("mapInfo[" + i + "," + j + "]" + "false");
                }
            }
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
