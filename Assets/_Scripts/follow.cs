using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject followObj;
    Vector3 _offset;
    [SerializeField][Range(0f, 1f)] private float _lerpTime;
    private void Start()
    {
        _offset = new Vector3(0, -7.85f, 5.9f);
    }

    private void LateUpdate()
    {
        Vector3 a = followObj.transform.position - _offset;
        a.x = 0;
        transform.position = Vector3.Lerp(transform.position, a, _lerpTime);
    }
}