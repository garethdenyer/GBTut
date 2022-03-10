using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShotClock : MonoBehaviour
{
    //attached to  Empty Controller

    public float timeElapsed;
    public bool timerIsRunning = false;  //this switches timer on/off
    public TMP_Text timeText;  //the UI element that will display the time
    float seconds;

    public Slider shottime;



    private void Start()
    {
        timeElapsed = 10f;
        DisplayTime(timeElapsed);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeElapsed -= Time.deltaTime;
            DisplayTime(timeElapsed);
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        seconds = Mathf.FloorToInt(timeToDisplay % 60);   //seconds is always mod 60 of the total seconds
        timeText.text = string.Format("{0:00}", seconds);
    }

    public void StartStop()
    {
        if (timerIsRunning)
        {
            timerIsRunning = false;
        }
        else
        {
            timerIsRunning = true;
        }
    }

    public void ResetShotclock()
    {
        timerIsRunning = false;
        timeElapsed = shottime.value;
        DisplayTime(timeElapsed);
    }
}
