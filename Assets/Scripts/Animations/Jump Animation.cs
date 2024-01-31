using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JumpAnimation : MonoBehaviour
{
    [SerializeField] float moveDistance = 500.0f;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _playerSwipesBehaviourObj;

    ScreenSwipesBehaviour _playerSwipesBehaviour;

    
    RunAnimation _runAnimation;
    Rigidbody2D _playerRigidbody;
    Sequence jumpSequence;
    Animator _animator;

    public bool JumpFlag;

    void Start()
    {
        _animator = _player.GetComponent<Animator>();
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        _playerSwipesBehaviour = _playerSwipesBehaviourObj.GetComponent<ScreenSwipesBehaviour>();
        _runAnimation = _player.GetComponent<RunAnimation>();
        JumpFlag = false;
    }

    private void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || _playerSwipesBehaviour.GetGlobalSwipesStatus() == 4) && !JumpFlag && !_runAnimation.GetGlobalInfo()["swish"]) 
        {
            Debug.Log("JUMP");
            JumpFlag = true;
            Debug.Log("JUMP FLAG " + JumpFlag);
            _animator.Play("Jump");
            PlayJumpAnimation();
        }
    }

    public bool GetJumpStatus()
    {
        return JumpFlag;
    }

    public void SetJumpStatus(bool _jmp)
    {
        JumpFlag = _jmp;
    }

    public void PlayJumpAnimation()
    {
        // ну это шляпа полная, проще без DoTween сделать
        //jumpSequence = DOTween.Sequence();
        //jumpSequence.AppendCallback(() => _playerRigidbody.AddForce(new Vector2(0.17f, 1.4f) * moveDistance, ForceMode2D.Force));

        _playerRigidbody.AddForce(new Vector2(8f, 80f) * moveDistance, ForceMode2D.Impulse);
    }

    public void StopJumpAnimation()
    {
        jumpSequence.Kill();
    }
}
