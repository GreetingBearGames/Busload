using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    private void Awake()
    {
        SpecifyLocation();
    }


    private void SpecifyLocation()
    {
        this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        var startPos = transform.position;
        transform.position = new Vector3(Random.Range(startPos.x - 4f, startPos.x + 4f),
                                        startPos.y,
                                        Random.Range(startPos.z - 3f, startPos.z + 3f));
    }
}
