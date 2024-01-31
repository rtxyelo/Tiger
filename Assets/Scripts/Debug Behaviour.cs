using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{
    public bool isReset = false;

    private string _ScoreKey = "Score";

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(_ScoreKey))
        {
            PlayerPrefs.SetInt(_ScoreKey, 0);
        }


        if (isReset)
        {
            PlayerPrefs.SetInt(_ScoreKey, 0);
        }

        Debug.Log("Current Score " + PlayerPrefs.GetInt(_ScoreKey, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
