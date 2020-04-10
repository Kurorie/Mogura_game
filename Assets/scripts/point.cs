using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class point : MonoBehaviour
{

    public TextMeshPro Point;

    // Update is called once per frame
    void Update()
    {
        string str;
        str = "point:" + mole_attack.point.ToString();
        Debug.Log(str + ":::" + mole_attack.point.ToString());
        Point.text = str;

    }
}
