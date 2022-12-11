using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerCount : MonoBehaviour
{
    [SerializeField] private BusProps busProps;
    private float _forwardSpeed;
    [SerializeField] GameObject _bus;
    [SerializeField] follow follow;
    Vector3 Offset = Vector3.zero;

    void Start()
    {
        Offset = transform.position - follow.worldToUISpace(this.GetComponent<Canvas>(), _bus.transform.position);
        _forwardSpeed = busProps.forwardSpeed;
    }

    void Update()
    {
        Debug.Log(follow.worldToUISpace(this.GetComponent<Canvas>(), _bus.transform.position));
        transform.position = follow.worldToUISpace(this.GetComponent<Canvas>(), _bus.transform.position) + Offset;
    }



}
