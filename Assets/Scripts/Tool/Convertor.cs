using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class Convertor : MonoBehaviour
{
    [SerializeField] private Texture2D texture;
    [SerializeField] private List<Color> colors;

    [SerializeField] private List<List<int>> result;

    [ContextMenu("出力開始")]
    public async void GetPixel()
    {
        //配列定義
        int[,] mapInfo = new int[texture.height, texture.width];

        Debug.Log("変換開始");
        string debug = "要素数(" + texture.height + "|" + texture.width + ")" + "\n";
        result = new List<List<int>>();

        for (int i = 0; i < texture.height; i++)
        {
            //行頭
            //
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
                else
                {
                    debug += "-1";

                    mapInfo[i, l] = -1;
                    Debug.Log("ColorError");
                }
            }
            result.Add(temp);

            //次の行へ
            debug += "} \n";
            await Task.Delay(1);
        }
        //for (int i = 0; i < 5; i++)
        //{
        //    for (int l = 0; l < 5; l++)
        //    {
        //        Debug.Log(mapInfo[i,l]);
        //    }
        //    print("a");
        //}
        Debug.Log(debug);

        StreamWriter sw = new StreamWriter("../MapData.txt", false);
        sw.WriteLine(debug);
        sw.Flush();
        sw.Close();
    }
}
