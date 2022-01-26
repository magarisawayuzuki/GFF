using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField] private float scrollSpeed; //背景をスクロールさせるスピード
    [SerializeField] private float startLine;//背景のスクロールを開始する位置
    [SerializeField] private float deadLine; //背景のスクロールが終了する位置


    void Update()
    {
        Scroll();
    }

    public void Scroll()
    {
        transform.Translate(scrollSpeed, 0, 0); //x座標をscrollSpeed分動かす

        if (transform.localPosition.x < deadLine) //もし背景のx座標よりdeadLineが大きくなったら
        {
            transform.localPosition = new Vector3(startLine, this.transform.localPosition.y, this.transform.localPosition.z);//背景をstartLineまで戻す
        }
    }
}