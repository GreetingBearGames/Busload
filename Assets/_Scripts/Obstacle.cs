using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool _isHit = false;

    public PassengerLose passengerLose;

    private void Start() {
        passengerLose = GameObject.FindGameObjectWithTag("Bus").GetComponent<PassengerLose>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bus" && !_isHit)
        {
            _isHit = true;
            GameManager.Instance.UpdatePassengerCount(-GameManager.Instance.PassengerIncreaseRate * 2);
            if(passengerLose != null){
                passengerLose.passangerToThrow = (int)GameManager.Instance.PassengerIncreaseRate * 2;
                passengerLose.isThrow = true;
            }
            
        }
    }
}
