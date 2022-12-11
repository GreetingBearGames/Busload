using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerCount : MonoBehaviour
{
    [SerializeField] GameObject _bus;
    private float _screenWidth;

    private void Start()
    {
        _screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(_bus.transform.position.x, -_screenWidth, _screenWidth),
                                        transform.position.y,
                                        _bus.transform.position.z);
    }
}

