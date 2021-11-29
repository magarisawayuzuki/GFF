using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    InputSystem input;
    /// <summary>
    /// この変数にしきい値を入れる
    /// </summary>
    const float TIME = 0.7f; 

    float pressTime = 0;

    bool x = false;


    private void Update()
    {
        //Testにバインドされたボタンが押されている間呼び出す　～～Performedは押されてから離されるまでの間Trueを返す～～
        if (input.Player.Test.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            pressTime += Time.deltaTime;

            if (!x)
            {
                x = !x;
            }
        }

        //タイム初期化
        else
        {
            if (x)
            {
                //強攻撃
                if(pressTime > TIME)
                {

                }
                //弱攻撃
                else
                {

                }

                pressTime = 0;
                x = !x;
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

}
