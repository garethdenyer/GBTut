using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //attached to  Empty Controller

    public float timeElapsed; //in seconds
    public bool timerIsRunning = false;  //this switches timer on/off
    public TMP_Text timeText;  //the UI element that will display the time

    int minutes;  //to display the minutes
    float seconds; //to display the 0-59 seconds part ... the remainder from minutes

    public Slider gametime;  //controlling the gametime
    public TMP_Text selectedgametime;




    private void Start()
    {
        timeElapsed = 1800f;
        DisplayTime(timeElapsed);
        selectedgametime.text = "30 min";
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
        minutes = Mathf.FloorToInt((timeToDisplay / 60) % 60); //minutes is seconds divided by 60 but mod 60 because 60 minutes make an hour
        seconds = Mathf.FloorToInt(timeToDisplay % 60);   //seconds is always mod 60 of the total seconds


        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TogglePause()
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

    public void SetGameTime()
    {
        timerIsRunning = false;
        timeElapsed = gametime.value * 60f;
        DisplayTime(timeElapsed);
    }

    public void ShowSelectedGameTime()
    {
        selectedgametime.text = gametime.value.ToString("N1") + " min";
    }
}
