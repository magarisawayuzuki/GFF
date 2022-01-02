using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : MonoBehaviour
{
   
    SpriteRenderer EnemySprite;　//  エネミー

    [SerializeField]
    public BossData mono; // spriteデータ
    [SerializeField]
    private GameObject SummonEnemy;
    [SerializeField]
    private float AnimeSpeed; //spriteアニメのスピード
    [SerializeField]
    private float[] Spritetime;   // spriteループ用の変数
    [SerializeField]
    private int[] MaxLeng;

    [SerializeField]
    private float[] EnemyY;

    private bool Attack;
   
    Boss boss;
    Maping map;
   
    // Start is called before the first frame update
    void Start()
    {       
        GameObject script = GameObject.FindGameObjectWithTag("BossObj");
        boss = script.GetComponent<Boss>();

        //------------二次元配列のスクリプト取得------------------
        GameObject en = GameObject.FindGameObjectWithTag("Map");
        map = en.GetComponent<Maping>();

        EnemySprite = GetComponent<SpriteRenderer>();
        MaxLeng[0] = mono.Attack.GetLength(0);
        MaxLeng[1] = mono.Spell.GetLength(0);

        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (boss._IsSpell == false)
        {
            EnemySprite.sprite = mono.Attack[(int)Spritetime[0]];
            Spritetime[0] += Time.deltaTime * AnimeSpeed;

            if(Spritetime[0] >= MaxLeng[0])
            {
                Spritetime[0] = 0;                 
            }
        }
        else if(boss._IsSpell == true)
        {
            //////////////////////////////magic

            EnemySprite.sprite = mono.Spell[(int)Spritetime[1]];
            Spritetime[1] += Time.deltaTime * AnimeSpeed;
           
            if (transform.position.x >= map.PlayerPositionX && transform.position.x - 3 <= map.PlayerPositionX)
            {
                boss._IsHitSpell = true;
            }
           
            if (Spritetime[1] >= MaxLeng[1])
            {
                boss._IsNext = true;
                boss._IsCount = true;
                Destroy(this.gameObject);
            }
        }
        if(boss._IsSummonSpell == true)
        {
            Instantiate(SummonEnemy, new Vector2(this.transform.position.x, this.transform.position.y + EnemyY[0]), Quaternion.identity);           
            Destroy(this.gameObject);
        }
    }
}
