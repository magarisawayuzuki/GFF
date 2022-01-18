using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

/*
 * 元の配列と同じように入ってます
 * 
 * 使い方
 * stageArrayData[0][0]
 *               行 列
 */

public class ImportCSV : MonoBehaviour
{
    private TextAsset MapCSV = default;
    [SerializeField]
    private string title = default;
    private List<int[]> list = new List<int[]>();
    public int[][] stageArrayData ;

    private void Start()
    {
        //CSV読み込み
        MapCSV = Resources.Load("MapCSVFile/" + title) as TextAsset;

        StringReader reader = new StringReader(MapCSV.text);

        while(reader.Peek() > -1)
        {
            //一列ずつ読み込み
            string line = reader.ReadLine();
            //,で区切って文字列を読み込む
            string[] x = line.Split (',');

            //string配列からint配列に
            int [] intArray = x.Select(int.Parse).ToArray();
            //listに格納
            list.Add(intArray);
        }

        stageArrayData = list.ToArray();
        print(stageArrayData[0][1]);
    }
}