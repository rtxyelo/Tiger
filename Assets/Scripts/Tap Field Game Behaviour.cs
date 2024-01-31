using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using TMPro;

public class TapFieldGameBehaviour : MonoBehaviour
{
    [SerializeField] float targetScale = 0.5f;
    [SerializeField] float _animTimeDuration = 1.0f;
    [SerializeField] GameObject _winTable;
    [SerializeField] GameObject _gameField;
    [SerializeField] GameObject _gameScore;
    [SerializeField] GameObject _scoreTextObj;
    [SerializeField] GameObject _timerObj;

    bool _animFlag = false;
    GameObject[] _targetList;
    ScoreBehaviour _scoreBehaviour;
    TMP_Text _scoreText;
    private TimerBehaviour _TimerScript;

    CanvasGroup[] _canvasGroupList;

    int _pressedBtn = -1;
    int _targetCount = 0;
    bool isCoroutineRunning = false;

    private string _ScoreKey = "Score";

    private void Start()
    {

        _TimerScript = _timerObj.GetComponent<TimerBehaviour>();
        _scoreText = _scoreTextObj.GetComponent<TMP_Text>();
        Debug.Log("SCORE ON SCREEN " + _scoreText.text);
        _scoreBehaviour = _gameScore.GetComponent<ScoreBehaviour>();
        _targetList = GameObject.FindGameObjectsWithTag("Target");
        _canvasGroupList = new CanvasGroup[_targetList.Length];
        Debug.Log("Target Count Is " +  _targetList.Length);
        for (int i = 0; i < _targetList.Length; i++)
        {
            _targetList[i].SetActive(false);
            _canvasGroupList[i] = _targetList[i].GetComponent<CanvasGroup>();
            if (_canvasGroupList[i] == null)
            {
                _canvasGroupList[i] = _targetList[i].AddComponent<CanvasGroup>();
            }
        }
        _animFlag = true;
        if (PlayerPrefs.HasKey(_ScoreKey))
            _scoreText.text = PlayerPrefs.GetInt(_ScoreKey, 0).ToString();
    }

    private void Update()
    {
        if (_animFlag)
        {
            _animFlag = false;
            _TimerScript.isStart = true;
            //_targetCount = Random.Range(1, _targetList.Length - 5);
            _targetCount = 1;

            int[] _listOfTargets = new int[_targetCount];

            for (int i = 0; i < _targetCount; i++)
            {
                _listOfTargets[i] = Random.Range(0, _targetList.Length);        // _listOfTargets[i] - Номер таргета, который анимируется
                _targetList[_listOfTargets[i]].SetActive(true);
                Debug.Log("TARGET ON " + _listOfTargets[i]);
                _pressedBtn = -1;
                //PlayAnim(_listOfTargets[i], i);
                PlayAnimation(_listOfTargets[i], i);
            }
        }
        //Debug.Log("Pressed button is " + _pressedBtn);

    }

    IEnumerator TestCoroutine()
    {
        isCoroutineRunning = true;
        _gameField.SetActive(false);
        _winTable.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _winTable.SetActive(false);
        _gameField.SetActive(true);
        isCoroutineRunning = false;
    }

    void PlayAnimation(int _targetNum, int _ind)
    {
        Vector3 _targetInitialScale = _targetList[_targetNum].transform.localScale;
        Vector3 _targetInitialPosition = _targetList[_targetNum].transform.localPosition;

        GameObject _targetFade = Instantiate(_targetList[_targetNum], 
                                            new Vector3(0f, 0f, 0f), 
                                            Quaternion.identity,
                                            _targetList[_targetNum].transform.parent);

        _targetFade.transform.SetSiblingIndex(0);

        //_targetFade.transform.localPosition = new Vector3(_targetInitialPosition.x, _targetInitialPosition.y, _targetInitialPosition.z);
        //_targetFade.transform.localScale = new Vector3(_targetInitialScale.x, _targetInitialScale.y, _targetInitialScale.z);


        CanvasGroup _targetFadeCanvas = _targetFade.GetComponent<CanvasGroup>();

        Sequence _sequence = DOTween.Sequence();
        //_sequence.AppendInterval(Random.Range(0.1f, 0.18f));
        _sequence.Append(_targetList[_targetNum].transform.DOScale(1.1f, 0.2f));

        _sequence.Append(_targetFadeCanvas.DOFade(0.5f, _animTimeDuration / 2f));
        _sequence.Join(_canvasGroupList[_targetNum].DOFade(0.8f, _animTimeDuration));
        _sequence.Join(_targetList[_targetNum].transform.DOScale(0.5f, _animTimeDuration));
        _sequence.Join(_targetFade.transform.DOScale(0.7f, _animTimeDuration));
        _sequence.OnComplete(() =>
        {
            Debug.Log($"Playing animation for target {_targetNum}, pressed button is {_pressedBtn}");

            _targetList[_targetNum].transform.localScale = _targetInitialScale;

            _canvasGroupList[_targetNum].DOFade(1.0f, _animTimeDuration).SetEase(Ease.InQuad);

            //if (_pressedBtn != -1 && _targetList[_pressedBtn] != null && _targetList[_pressedBtn].activeSelf)
            if (_pressedBtn != -1 && _targetList[_pressedBtn].activeSelf)
            {
                Debug.Log("CATCH!");
                _scoreBehaviour.IncreaseScore(1);
                if (PlayerPrefs.HasKey(_ScoreKey))
                    _scoreText.text = PlayerPrefs.GetInt(_ScoreKey, 0).ToString();
                Debug.Log("SCORE IS PLAYERPREFS " + PlayerPrefs.GetInt(_ScoreKey, 0).ToString());
                StartCoroutine(TestCoroutine());
            }

            Destroy(_targetFade);

            _targetList[_targetNum].SetActive(false);

            if (_ind == _targetCount - 1)
            {
                _animFlag = true;
            }

        });
    }

    //void PlayAnim(int _targetNum, int _ind)
    //{
    //    Vector3 _targetInitialSize = _targetList[_targetNum].transform.localScale;

    //    Sequence _sequence = DOTween.Sequence();

    //    _sequence.AppendInterval(Random.Range(0.1f, 0.18f));

    //    _sequence.Append(_targetList[_targetNum].transform.DOScale(new Vector3(targetScale, targetScale, 1f), _animTimeDuration).SetEase(Ease.OutQuad));





    //    //_sequence.Join(_canvasGroupList[_targetNum].DOFade(0.2f, _animTimeDuration * 1.3f).SetEase(Ease.OutQuad));
    //    //_sequence.Join(_canvasGroupList[_targetNum].DOFade(0.2f, _animTimeDuration * 1.3f).SetEase(Ease.));



    //    //// Добавляем изменение цвета и прозрачности
    //    //_sequence.Join(_targetList[_targetNum].GetComponent<Renderer>().material.DOColor(_fadeColor, "_Color").SetEase(Ease.OutQuad));
    //    //_sequence.Join(_targetList[_targetNum].GetComponent<Renderer>().material.DOFade(0.5f, _fadeDuration).SetEase(Ease.OutQuad));

    //    //_sequence.AppendCallback(() =>
    //    //{
    //    //    //============================

    //    //    // Создаем копию объекта и размещаем ее на том же месте
    //    //    GameObject trailObject = Instantiate(_targetList[_targetNum]);
    //    //    trailObject.transform.position = _targetList[_targetNum].transform.position;

    //    //    // Производим анимацию для следа (например, изменение прозрачности)
    //    //    Sequence trailSequence = DOTween.Sequence();
    //    //    trailSequence.Append(trailObject.transform.DOScale(Vector3.zero, _animTimeDuration)
    //    //                         .SetEase(Ease.OutQuad));
    //    //    trailSequence.Join(trailObject.GetComponent<CanvasGroup>().DOFade(0.5f, _animTimeDuration * 2f).SetEase(Ease.OutQuad));

    //    //    // Уничтожаем след после окончания анимации
    //    //    trailSequence.OnComplete(() =>
    //    //    {
    //    //        Destroy(trailObject);
    //    //    });

    //    //    //============================
    //    //});

    //    _sequence.OnComplete(() =>
    //    {
    //        Debug.Log($"Playing animation for target {_targetNum}, pressed button is {_pressedBtn}");

    //        _targetList[_targetNum].transform.localScale = _targetInitialSize;

    //        _canvasGroupList[_targetNum].DOFade(1.0f, _animTimeDuration).SetEase(Ease.InQuad);

    //        //if (_pressedBtn != -1 && _targetList[_pressedBtn] != null && _targetList[_pressedBtn].activeSelf)
    //        if (_pressedBtn != -1 && _targetList[_pressedBtn].activeSelf)
    //        {
    //            Debug.Log("CATCH!");
    //            _scoreBehaviour.IncreaseScore(1);
    //            if (PlayerPrefs.HasKey(_ScoreKey))
    //                _scoreText.text = PlayerPrefs.GetInt(_ScoreKey, 0).ToString();
    //            Debug.Log("SCORE IS PLAYERPREFS " + PlayerPrefs.GetInt(_ScoreKey, 0).ToString());
    //            StartCoroutine(TestCoroutine());
    //        }

    //        _targetList[_targetNum].SetActive(false);
            
    //        if (_ind == _targetCount - 1)
    //        {
    //            _animFlag = true;
    //        }

    //    });
    //}

    public void CheckPressedButton(int _btnNum)
    {
        Debug.Log("Pressed button is " + _btnNum);
        _pressedBtn = _btnNum;
    }
}
