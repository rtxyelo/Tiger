using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwishAnimation : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _playerSwipesBehaviourObj;

    ScreenSwipesBehaviour _playerSwipesBehaviour;
    
    RunAnimation _runAnimation;
    Rigidbody2D _playerRigidbody;
    
    BoxCollider2D _playerCollider;

    Vector2 _newColliderSize = Vector2.zero;
    Vector2 _oldColliderSize = Vector2.zero;
    Vector2 _newOffsetSize = Vector2.zero;
    Vector2 _oldOffsetSize = Vector2.zero;

    [SerializeField] private float moveDistance = 500f;

    Animator _animator;

    public bool SwishFlag = false;

    void Start()
    {
        _animator = _player.GetComponent<Animator>();
        _playerSwipesBehaviour = _playerSwipesBehaviourObj.GetComponent<ScreenSwipesBehaviour>();
        _runAnimation = _player.GetComponent<RunAnimation>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        _playerCollider = _player.GetComponent<BoxCollider2D>();

        _newColliderSize = new Vector2(_playerCollider.size.x, _playerCollider.size.y / 2f);
        _newOffsetSize = new Vector2(_playerCollider.offset.x, -100f);
        _oldColliderSize = _playerCollider.size;
        _oldOffsetSize = _playerCollider.offset;
    }

    private void Update()
    {

        if ((Input.GetKeyUp(KeyCode.Q) || _playerSwipesBehaviour.GetGlobalSwipesStatus() == 0) && 
            !SwishFlag && 
            !_runAnimation.GetGlobalInfo()["jump"] && 
            !_runAnimation.GetGlobalInfo()["swipe"])
        {
            Debug.Log("SWISH");
            _playerCollider.size = _newColliderSize;
            _playerCollider.offset = _newOffsetSize;
            SwishFlag = true;
            _animator.Play("Swich");
            PlaySwishAnimation();
        }
    }

    public void PlaySwishAnimation()
    {
        Sequence swishSequence = DOTween.Sequence();

        swishSequence.AppendCallback(() => _playerRigidbody.AddForce(new Vector2(6f, 0f) * moveDistance, ForceMode2D.Impulse));

        swishSequence.AppendInterval(0.6f);

        swishSequence.OnComplete(() =>
        {
            SetOldOffsetAndSize();
            SwishFlag = false;
        });
    }

    public void SetOldOffsetAndSize()
    {
        _playerCollider.size = _oldColliderSize;
        _playerCollider.offset = _oldOffsetSize;
    }

    public bool GetSwishStatus()
    { return SwishFlag; }

    public void SetSwishStatus(bool swishFlag)
    { SwishFlag = swishFlag;}
}
