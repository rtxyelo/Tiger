using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class RunAnimation : MonoBehaviour
{
    [SerializeField] GameObject _jumpObj;
    [SerializeField] GameObject _swipeObj;
    [SerializeField] GameObject _swishObj;

    [SerializeField] GameObject _gameScore;
    [SerializeField] GameObject _scoreTextObj;

    SwishAnimation _swishAnim;
    SwipeAnimation _swipeAnim;
    JumpAnimation _jumpAnim;

    TMP_Text _scoreText;
    ScoreBehaviour _scoreBehaviour;

    Animator _animator;

    private string _ScoreKey = "Score";

    bool _swishStatus, _swipeStatus, _jumpStatus = false;

    Rigidbody2D _playerRigidbody;

    Dictionary<string, bool> _dictInfo = new Dictionary<string, bool>();


    void Start()
    {
        _animator = GetComponent<Animator>();
        _scoreBehaviour = _gameScore.GetComponent<ScoreBehaviour>();
        _scoreText = _scoreTextObj.GetComponent<TMP_Text>();
        _swishAnim = _swishObj.GetComponent<SwishAnimation>();
        _swipeAnim = _swipeObj.GetComponent<SwipeAnimation>();
        _jumpAnim = _jumpObj.GetComponent<JumpAnimation>();
        if (PlayerPrefs.HasKey(_ScoreKey))
            _scoreText.text = PlayerPrefs.GetInt(_ScoreKey, 0).ToString();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        //PlayRunAnimation();
        
    }
    private void Update()
    {
        _dictInfo = GetGlobalInfo();
        bool _globalSwishStatus = _dictInfo["swish"];
        bool _globalSwipeStatus = _dictInfo["swipe"];
        bool _globalJumpStatus = _dictInfo["jump"];

        if (!_globalJumpStatus && !_globalSwishStatus && !_globalSwipeStatus)
        {
            _playerRigidbody.AddForce(Vector2.left * Time.deltaTime * 1000f);
            //Debug.Log("Current velocity " + _playerRigidbody.velocity);
        }

    }
    public Dictionary<string, bool> GetGlobalInfo()
    {
        _swishStatus = _swishAnim.GetSwishStatus();
        _dictInfo["swish"] = _swishStatus;
        _swipeStatus = _swipeAnim.GetSwipeStatus();
        _dictInfo["swipe"] = _swipeStatus;
        _jumpStatus = _jumpAnim.GetJumpStatus();
        _dictInfo["jump"] = _jumpStatus;

        return _dictInfo;
    }

    private void PlayRunAnimation()
    {
        Debug.Log("RUN");
        _animator.Play("Idle");
    }

    public void ContinueRunAnimation()
    {
        PlayRunAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("IS TRIGGERED ENTER!!");

        if (other.gameObject.CompareTag("Respawn") || 
            other.gameObject.CompareTag("GroundBarrier") || 
            (other.gameObject.CompareTag("PandaBarrier") && !_swishAnim.GetSwishStatus())) 
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.CompareTag("Prize"))
        {
            _scoreBehaviour.IncreaseScore(1);
            if (PlayerPrefs.HasKey(_ScoreKey))
                _scoreText.text = PlayerPrefs.GetInt(_ScoreKey, 0).ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            _playerRigidbody.AddForce(new Vector2(-10f, 0f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ground collision detected!");
        if (collision.gameObject.CompareTag("Ground"))
        {
            _swishAnim.SetSwishStatus(false);
            _jumpAnim.SetJumpStatus(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //_jumpAnim.SetJumpStatus(false);
        }
    }
}
