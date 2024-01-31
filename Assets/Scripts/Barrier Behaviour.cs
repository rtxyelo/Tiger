using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] _gameObjects;
    [SerializeField] float _speed = 5.0f;
    int _barrierNum = 0;
    float _moveDistance = 10f;
    RectTransform _barrierTransform;
    RectTransform[] _barriersTransforms;

    // Start is called before the first frame update
    void Start()
    {
        _barriersTransforms = new RectTransform[_gameObjects.Length];

        //_barrierNum = Random.Range(0, _gameObjects.Length);
        _barrierNum = Random.Range(0, 100) % _gameObjects.Length;

        for (int i = 0; i < _gameObjects.Length; i++)
        {
            _gameObjects[i].SetActive(true);
            _barriersTransforms[i] = _gameObjects[i].GetComponent<RectTransform>();
            if (i != _barrierNum)
                _gameObjects[i].SetActive(false);

        }
        _barrierTransform = _barriersTransforms[_barrierNum];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _position = _barrierTransform.transform.position; 

        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (_position.x <= -_moveDistance)
        {
            _barrierTransform.transform.position = new Vector3(_moveDistance, _position.y, _position.z);
            _gameObjects[_barrierNum].SetActive(false);
            _barrierNum = Random.Range(0, 100) % _gameObjects.Length;
            _barrierTransform = _barriersTransforms[_barrierNum];
            _gameObjects[_barrierNum].SetActive(true);
        }
    }


}
