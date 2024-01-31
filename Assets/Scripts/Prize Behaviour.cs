using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _prizeObject;
    [SerializeField] float _speed = 5.0f;
    int _barrierNum = 0;
    float _moveDistance = 7f;
    RectTransform _prizeTransform;
    // Start is called before the first frame update
    void Start()
    {

        _prizeObject.SetActive(true);

        _prizeTransform = _prizeObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _position = _prizeTransform.transform.position;

        _prizeTransform.transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if (_position.x <= -_moveDistance)
        {
            _prizeTransform.transform.position = new Vector3(_moveDistance, _position.y, _position.z);
            _prizeObject.SetActive(false);
            _prizeTransform = _prizeObject.GetComponent<RectTransform>();
            _prizeObject.SetActive(true);
        }
    }
}
