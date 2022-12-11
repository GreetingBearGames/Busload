using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    //Use this script inside of finish line GameObject that has collider.
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Bus"){
            GameManager.Instance.WinLevel(true);
        } 
    }
}
