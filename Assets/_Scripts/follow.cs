using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject followObj;
    Vector3 offset;
    private void Start() {
        offset = followObj.transform.position - transform.position;
    }

    private void Update() {
        Vector3 a = followObj.transform.position - offset;
        a.x = 0;
        transform.position = a;
    }
}
