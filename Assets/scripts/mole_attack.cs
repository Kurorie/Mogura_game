using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

enum eState
{
    down,
    up
}

public class mole_attack : MonoBehaviour
{
    eState[] state = new eState[5];
    GameObject[] mole = new GameObject[5];
    float preTime = 0;
    public float UpDistance;
    public static int point = 0;
    public int firststage_point = 0;

    public double actionTime = 1.5;
    public TextMeshPro pointText;

    // Start is called before the first frame update
    void Start()
    {
        mole[0] = GameObject.Find("mole_attack");
        mole[1] = GameObject.Find("mole_attack1");
        mole[2] = GameObject.Find("mole_attack2");
        mole[3] = GameObject.Find("mole_attack3");
        mole[4] = GameObject.Find("mole_attack4");

        for (int i = 0; i < 5; i++)
        {
            state[i] = eState.down;
        }
    }


    void check(int id)
    {
        Vector3 oldPos = mole[id].transform.localPosition;

        if (state[id] == eState.up)
        {
            firststage_point += 1;
            mole[id].transform.localPosition = new Vector3(oldPos.x, 0, oldPos.z);
            state[id] = eState.down;

        }
        else
        {
            firststage_point -= 1;
        }
    }

    void Rewrite()
    {
        point = firststage_point;
    }

    void Update_Text()
    {
        string str;
        str = "point:" + point.ToString();
        pointText.text = str;
    }



    // Update is called once per frame
    void Update()
    {
        if( (timer.countTime - preTime) >= actionTime)
        {
            float random = Random.value * 100;
            int id = (int)random % 5;
            Vector3 oldPos = mole[id].transform.localPosition;

            if (state[id] == eState.down)
            {
               mole[id].transform.localPosition = new Vector3(oldPos.x, UpDistance, oldPos.z);
               state[id] = eState.up;
            }
            else
            {
                mole[id].transform.localPosition = new Vector3(oldPos.x, 0, oldPos.z);
                state[id] = eState.down;
            }
            preTime = timer.countTime;
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                check(0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                check(1);
            }

            if (Input.GetKey(KeyCode.E))
            {
                check(2);
            }

            if (Input.GetKey(KeyCode.R))
            {
                check(3);
            }

            if (Input.GetKey(KeyCode.T))
            {
                check(4);
            }

        }



        Rewrite();



        if (point > 5 && point <= 10)
        {
            actionTime = 1.0;
        }

        else if (point > 10 && point <= 15)
        {
            actionTime = 0.7;
        }

        else if (point > 15 && point <= 20)
        {
            actionTime = 0.5;
        }

        else if (point > 20 && point <= 30)
        {
            actionTime = 0.3;
        }

        else if (point > 30 && point <= 40)
        {
            actionTime = 0.2;
        }

        else if (point > 40 && point <= 50)
        {
            actionTime = 0.1;
        }

        else
        {
            actionTime = 1.5;
        }


        Update_Text();

        if(point >= 50)
        {
            SceneManager.LoadScene("second game");
        }

    }

}
