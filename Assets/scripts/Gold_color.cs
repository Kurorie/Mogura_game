using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_color : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject mole = GameObject.Find("mole_attack_left_gold");
        mole.GetComponent<Renderer>().material.color = new Color(245, 209, 0, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
