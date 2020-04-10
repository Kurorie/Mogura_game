using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.Find("mole_attack_right_gold");
        Instantiate(origin, new Vector3(0.0f, 30.0f, 0.0f), Quaternion.identity);
        Instantiate(origin, new Vector3(15.0f, 30.0f, 0.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
