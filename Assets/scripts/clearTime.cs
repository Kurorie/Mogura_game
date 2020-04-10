using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clearTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().text = timer.countTime.ToString("F3");
    }
}
