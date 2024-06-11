using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class TimeRemainCount : MonoBehaviour
{
    public Text counttxt;
    public int time = 200;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        int timeremain = (int)t;
        int thetime = time - timeremain;
        counttxt.text = thetime.ToString();
    }
}
