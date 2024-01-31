using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    private string _ScoreKey = "Score";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(_ScoreKey))
        {
            PlayerPrefs.SetInt(_ScoreKey, 0);
        }


    }

    public void IncreaseScore(int score)
    {
        if (PlayerPrefs.HasKey(_ScoreKey)) 
        {
            int currentScore = PlayerPrefs.GetInt(_ScoreKey, 0);

            currentScore += score;

            PlayerPrefs.SetInt(_ScoreKey, currentScore);

            Debug.Log("Current Score Is " + currentScore);
        }
    }
}
