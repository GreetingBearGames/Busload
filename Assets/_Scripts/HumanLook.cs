using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLook : MonoBehaviour
{
    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindWithTag("Bus").gameObject;
        //InvokeRepeating("RotateTowardsBus", 0.2f, 0.05f);
    }


    private void RotateTowardsBus()
    {
        transform.LookAt(_target.transform);
    }
}

