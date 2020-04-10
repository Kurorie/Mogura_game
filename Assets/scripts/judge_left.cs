using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum Bool_left //当たり判定
{
    on,
    hit,
    off
}

enum Rarity_left //normalは１点、rareは3点、モグラが出現していない時nothing
{
    normal,
    rare,
    nothing
}

public class judge_left : MonoBehaviour
{

    Bool_left[] mole_judge = new Bool_left[10];
    Rarity_left[] mole_rare = new Rarity_left[10];

    public GameObject[] mole_left = new GameObject[10];
    private GameObject mole_normal;
    private GameObject mole_gold;
    private int id = -1; //モグラの紐付け

    public float preTime = 0;
    public int left_point = 0; 
    public float speed = 1;
    public float actionTime = 0;
    public TextMeshPro pointText;

    // Start is called before the first frame update
    void Start()
    {
        mole_normal = GameObject.Find("mole_attack_left");
        mole_gold = GameObject.Find("mole_attack_left_gold");

        for (int i = 0; i < 10; i++)　//初期化
        {
            mole_judge[i] = Bool_left.off;
            mole_rare[i] = Rarity_left.nothing;
        }
    }



    void Rewrite()
    {
        mole_attack.point = mole_attack.point + left_point;
        left_point = 0;
    }


    void Update_Text()
    {
        //int score;
        string str;
        //score = mole_attack.point;
        str = "point:" + mole_attack.point.ToString();
        pointText.text = str;
    }


    void Change_speed()
    {
        if (mole_attack.point > 55 && mole_attack.point <= 60)
        {
            speed = 12;
            actionTime = 1.6f;
        }

        else if (mole_attack.point > 60 && mole_attack.point <= 65)
        {
            speed = 17;
            actionTime = 0.9f;
        }

        else if (mole_attack.point > 65 && mole_attack.point <= 70)
        {
            speed = 28;
            actionTime = 0.6f;
        }

        else if (mole_attack.point > 70)
        {
            speed = 45;
            actionTime = 0.4f;
        }

        else
        {
            speed = 8;
            actionTime = 2.2f;
        }

    }






    // Update is called once per frame
    void Update()
    {

        if ((timer.countTime - preTime) >= actionTime)　//一定時間ごとに発生
        {
            float random = Random.value * 100;
            int number = (int)random % 5;
            id++;//モグラ生成のid

            if (number == 0)//20％の確率で金モグラ
            {
                mole_rare[id] = Rarity_left.rare;
                mole_judge[id] = Bool_left.on;
                //モグラのクローン生成
                mole_left[id] = Instantiate(mole_gold);
            }

            else
            {
                mole_rare[id] = Rarity_left.normal;
                mole_judge[id] = Bool_left.on;
                //モグラのクローン生成
                mole_left[id] = Instantiate(mole_normal);
            }

  
            preTime = timer.countTime;


        }


        for (int i = 0; i < id + 1; i++)
        {

        
            Vector3 oldPos = mole_left[i].transform.localPosition;
            mole_left[i].transform.localPosition = new Vector3(oldPos.x + (0.05f * speed), oldPos.y, oldPos.z);

            if (mole_left[i].transform.localPosition.x > 1.0f && mole_left[i].transform.localPosition.x < 8.5f)
            {
                //この範囲内で押すとポイント入る
                mole_judge[i] = Bool_left.hit;
            }

            else if (mole_left[i].transform.localPosition.x >= 46)//モグラのメモリ解放処理
            {
                mole_judge[i] = Bool_left.off;
                mole_rare[i] = Rarity_left.nothing;
                //モグラ[i]の削除
                Destroy(mole_left[i]);
                for(int k = i; k  < id; k++)
                {
                    mole_left[k] = mole_left[k+1];
                    mole_judge[k] = mole_judge[k+1];
                    mole_rare[k] = mole_rare[k+1];
                }

                id--;
                i--;
            }

            else
            {
                mole_judge[i] = Bool_left.on;
            }

        }


        if (Input.GetKeyDown(KeyCode.U))
        {
            int flag = 0; //当たり判定内にキーボードを押したかどうか

            for(int i = 0; i < id + 1; i++)
            {
                if(mole_judge[i] == Bool_left.hit)
                {
                    flag= 1;
                    if (mole_rare[i] == Rarity_left.rare)
                    {
                        left_point = left_point + 3;
                    }

                    else if(mole_rare[i] == Rarity_left.normal)
                    {
                        left_point++;
                    }

                    mole_judge[i] = Bool_left.off;
                    mole_rare[i] = Rarity_left.nothing;
                    Destroy(mole_left[i]);

                    for (int k = i; k < id; k++)
                    {
                        mole_left[k] = mole_left[k+1];
                        mole_judge[k] = mole_judge[k+1];
                        mole_rare[k] = mole_rare[k+1];    
                    }

                    id--;
                    i--;
                }
                
            }

            if(flag == 0)
            {
                if (mole_attack.point >= 51)
                {
                    left_point--;
                }
            }

          
        }


        Rewrite();

        Update_Text();

        Change_speed();


        if (mole_attack.point >=80)
        {
            SceneManager.LoadScene("finish");
        }

        else if(mole_attack.point < 50)
        {
            SceneManager.LoadScene("Mini game");
        }
    }
}
