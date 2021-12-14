using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideHp : MonoBehaviour
{
  
    public Slider slider;		// シーンに配置したSlider格納用

    Boss boss;
    // Start is called before the first frame update
    void Start()
    {
      
        GameObject en = GameObject.FindGameObjectWithTag("BossObj");
        boss = en.GetComponent<Boss>();

        slider = GetComponent<Slider>();

        float maxHp = 210f;
        float nowHp = 210f;


        //スライダーの最大値の設定
        slider.maxValue = maxHp;

        //スライダーの現在値の設定
        slider.value = nowHp;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (slider.value <= 180 && slider.value >= 121)
        {
            boss.HpState = 2;
        }
        if (slider.value <= 120 && slider.value >= 61)
        {
            boss.HpState = 3;
        }
        if (slider.value <= 60)
        {
            boss.HpState = 4;
        }
    }
}
