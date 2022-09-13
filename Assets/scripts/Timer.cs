using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public Text text;
    public Text text2;
    public float timer;
    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            
        }
        int timerint =  (int)timer;
        int timerms = (int)((timer-timerint) * 1000);
        var span = new TimeSpan(0, 0, timerint);
        text.text = "Time:" + span.ToString(@"hh\:mm\:ss") + ":"+timerms;
        text2.text = "ClearTime\n" + span.ToString(@"hh\:mm\:ss") + ":" + timerms;
    }

    public void TimerReset()
    {
        timer = 0;
        isActive = false;
    }

    public void TimerStop()
    {
        isActive = false;
    }

    public void TimerStart()
    {
        isActive = true;
    }
}
