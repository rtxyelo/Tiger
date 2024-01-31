using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuScoreBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _scoreTextObj;
    TMP_Text _scoreText;

    private string _ScoreKey = "Score";

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = _scoreTextObj.GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey(_ScoreKey))
            _scoreText.text = PlayerPrefs.GetInt(_ScoreKey, 0).ToString();
    }
}
