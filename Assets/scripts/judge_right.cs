using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum Bool_right //当たり判定
{
    on,
    hit,
    off
}

enum Rarity_right //normalは１点、rareは3点、モグラが出現していない時nothing
{
    normal,
    rare,
    nothing
}

public class judge_right : MonoBehaviour
{

    Bool_right[] mole_judge = new Bool_right[10];
    Rarity_right[] mole_rare = new Rarity_right[10];

    public GameObject[] mole_right = new GameObject[10];
    private GameObject mole_normal;
    private GameObject mole_gold;
    private int id = -1; //モグラの紐付け

    public float preTime = 0;
    public int right_point = 0;
    public float speed = 1;
    public float actionTime = 0;
    public TextMeshPro pointText;

    // Start is called before the first frame update
    void Start()
    {
        mole_normal = GameObject.Find("mole_attack_right");
        mole_gold = GameObject.Find("mole_attack_right_gold");

        for (int i = 0; i < 10; i++)　//初期化
        {
            mole_judge[i] = Bool_right.off;
            mole_rare[i] = Rarity_right.nothing;
        }
    }



    void Rewrite()
    {
        mole_attack.point = mole_attack.point + right_point;
        right_point = 0;
    }


    void Update_Text()
    {
        string str;
        str = "point:" + mole_attack.point.ToString();
        pointText.text = str;
    }

    void Change_speed()
    {
        if (mole_attack.point > 55 && mole_attack.point <= 60)
        {
            speed = 10;
            actionTime = 1.5f;
        }

        else if (mole_attack.point > 60 && mole_attack.point <= 65)
        {
            speed = 15;
            actionTime = 1.0f;
        }

        else if (mole_attack.point > 65 && mole_attack.point <= 70)
        {
            speed = 25;
            actionTime = 0.7f;
        }

        else if (mole_attack.point > 70)
        {
            speed = 40;
            actionTime = 0.5f;
        }

        else
        {
            speed = 5;
            actionTime = 2.0f;
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
                mole_rare[id] = Rarity_right.rare;
                mole_judge[id] = Bool_right.on;
                //モグラのクローン生成
                mole_right[id] = Instantiate(mole_gold);
            }

            else
            {
                mole_rare[id] = Rarity_right.normal;
                mole_judge[id] = Bool_right.on;
                //モグラのクローン生成
                mole_right[id] = Instantiate(mole_normal);
            }


            preTime = timer.countTime;
   
        }


        for (int i = 0; i < id + 1; i++)
        {


            Vector3 oldPos = mole_right[i].transform.localPosition;
            mole_right[i].transform.localPosition = new Vector3(oldPos.x - (0.05f * speed), oldPos.y, oldPos.z);

            if (mole_right[i].transform.localPosition.x > -7.5f && mole_right[i].transform.localPosition.x < 0.0f)
            {
                //この範囲内で押すとポイント入る
                mole_judge[i] = Bool_right.hit;
            }

            else if (mole_right[i].transform.localPosition.x <= -46)//モグラのメモリ解放処理
            {
                mole_judge[i] = Bool_right.off;
                mole_rare[i] = Rarity_right.nothing;
                //モグラ[i]の削除
                Destroy(mole_right[i]);
                for (int k = i; k < id; k++)
                {
                    mole_right[k] = mole_right[k + 1];
                    mole_judge[k] = mole_judge[k + 1];
                    mole_rare[k] = mole_rare[k + 1];
                }

                id--;
                i--;
            }

            else
            {
                mole_judge[i] = Bool_right.on;
            }

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            int flag = 0; //当たり判定内にキーボードを押したかどうか

            for (int i = 0; i < id + 1; i++)
            {
                if (mole_judge[i] == Bool_right.hit)
                {
                    flag = 1;
                    if (mole_rare[i] == Rarity_right.rare)
                    {
                        right_point = right_point + 3;
                    }

                    else if (mole_rare[i] == Rarity_right.normal)
                    {
                        right_point++;
                    }

                    mole_judge[i] = Bool_right.off;
                    mole_rare[i] = Rarity_right.nothing;
                    Destroy(mole_right[i]);

                    for (int k = i; k < id; k++)
                    {
                        mole_right[k] = mole_right[k + 1];
                        mole_judge[k] = mole_judge[k + 1];
                        mole_rare[k] = mole_rare[k + 1];
                    }

                    id--;
                    i--;
                }

            }

            if (flag == 0)
            {
                if (mole_attack.point >= 51)
                {
                    right_point--;
                }
            }


        }
        


        Rewrite();

        Update_Text();

        Change_speed();


        if (mole_attack.point >= 80)
        {
            SceneManager.LoadScene("finish");
        }

        else if (mole_attack.point < 50)
        {
            SceneManager.LoadScene("Mini game");
        }
    }
}