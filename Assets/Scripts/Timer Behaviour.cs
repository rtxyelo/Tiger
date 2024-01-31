using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerBehaviour : MonoBehaviour
{
    public TMP_Text canvasText;

    [HideInInspector]
    public float time = 0f;
    [HideInInspector]
    public bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasText.text = "0:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            time += Time.deltaTime;
            string formattedTime = FormatTime(time);
            canvasText.text = formattedTime;
        }
    }

    string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
    }
}
