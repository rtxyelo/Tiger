using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwipeAnimation : MonoBehaviour
{
    [SerializeField] GameObject _jumpObj;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _playerSwipesBehaviourObj;

    [SerializeField] Sprite swipeSprite;

    ScreenSwipesBehaviour _playerSwipesBehaviour;
    Image _imgRenderer;
    RunAnimation _runAnimation;
    JumpAnimation _jumpAnimation;
    Rigidbody2D _playerRigidbody;

    public bool SwipeFlag = false;
    [SerializeField] float _forseMagnitude = 100.0f;
    void Start()
    {
        _playerSwipesBehaviour = _playerSwipesBehaviourObj.GetComponent<ScreenSwipesBehaviour>();
        _imgRenderer = _player.GetComponent<Image>();
        _runAnimation = _player.GetComponent<RunAnimation>();
        _jumpAnimation = _jumpObj.GetComponent<JumpAnimation>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if ((Input.GetKeyUp(KeyCode.S) || _playerSwipesBehaviour.GetGlobalSwipesStatus() == 3) && !SwipeFlag && _runAnimation.GetGlobalInfo()["jump"])
        {
            Debug.Log("SWIPE");
            SwipeFlag = true;
            PlaySwipeAnimation();
        }
    }

    public void PlaySwipeAnimation()
    {
        _jumpAnimation.StopJumpAnimation();

        _imgRenderer.sprite = swipeSprite;

        Sequence swishSequence = DOTween.Sequence();

        //swishSequence.AppendInterval(0.3f);

        swishSequence.OnComplete(() =>
        {
            _playerRigidbody.AddForce(new Vector2(0f, -1f) * _forseMagnitude, ForceMode2D.Impulse);
            SwipeFlag = false;
            _runAnimation.ContinueRunAnimation();
        });
        
    }

    public bool GetSwipeStatus()
    { return SwipeFlag; }

}
