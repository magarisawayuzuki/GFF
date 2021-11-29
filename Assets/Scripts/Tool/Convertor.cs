using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class Convertor : MonoBehaviour
{
    [Header("変換元画像")]
    [SerializeField] private Texture2D texture;

    [Header("識別色")]
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<List<int>> result;

    [Header("出力形式")]
    [SerializeField] private bool EnableExportTextFile = true;
    [SerializeField] private bool EnableExportConsole = false;



    [ContextMenu("出力開始")]
    public async void GetPixel()
    {
        //配列定義
        int[,] mapInfo = new int[texture.height, texture.width];

        Debug.Log("変換開始");

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

        Debug.Log("変換終了");
    }



    private void ExportText(string debug)
    {
        StreamWriter sw = new StreamWriter("../MapData.txt", false);
        sw.WriteLine(debug);
        sw.Flush();
        sw.Close();
    }
}
