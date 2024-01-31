using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    private Vector2 _offset = Vector2.zero;
    private Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _offset = _material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        _offset.x -= _speed * Time.deltaTime;
        _material.SetTextureOffset("_MainTex", _offset);
    }
}
