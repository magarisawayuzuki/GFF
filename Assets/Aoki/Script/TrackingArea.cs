using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingArea : UnityEngine.MonoBehaviour
{
    EnemyNormal enemy;
   
    private void Start()
    {
        GameObject en = GameObject.FindGameObjectWithTag("Enemy");
        enemy = en.GetComponent<EnemyNormal>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (enemy.Return == true && enemy.anime == 2)
            {
                for (int i = 0; i < enemy.Spritetime.GetLength(0); i++)
                {
                    enemy.Spritetime[i] = 0;
                }
                enemy.Return = false;
                enemy.Tracking = true;　//再び追跡
                enemy.anime = 2;
            }

            if(enemy.Return == false && enemy.anime == 3)
            {
                for (int i = 0; i < enemy.Spritetime.GetLength(0); i++)
                {
                    enemy.Spritetime[i] = 0;
                }             
                enemy.Tracking = true;　//再び追跡
                enemy.anime = 1;
            }
            
        }
    }


}
