using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorkDayTimer : MonoBehaviour
{
    [SerializeField] float dayDuration = 10;

    float timeRemaining;
    bool timerIsRunning;

    float timeRemainingMinutes;
    float timeRemainingSeconds;
    void Start()
    {
        StartTimer();

    }

    private void StartTimer()
    {
        timerIsRunning = true;
        timeRemaining = dayDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;



        }
        else
        {
            timeRemaining = 0;
            timerIsRunning = false;
        }


    }

    public string FormatTimeDisplay()
    {
        timeRemainingMinutes = Mathf.FloorToInt(timeRemaining / 60);
        timeRemainingSeconds = Mathf.FloorToInt(timeRemaining % 60);
        return string.Format("{0:00}:{1:00}", timeRemainingMinutes, timeRemainingSeconds);
    }
}
