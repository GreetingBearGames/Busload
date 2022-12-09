using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLook : MonoBehaviour
{
    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").gameObject;
    }

    void Update()
    {
        StartCoroutine("RotateTowardsBus");
    }

    IEnumerator RotateTowardsBus()
    {
        yield return new WaitForSeconds(0.5f);
        transform.LookAt(_target.transform);
    }
}

