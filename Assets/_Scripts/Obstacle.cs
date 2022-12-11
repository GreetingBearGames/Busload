using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool _isHit = false;
    [SerializeField] private BusProps busProps;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Bus" && !_isHit){
            Debug.Log("pass count : " + GameManager.Instance.Passenger);
            _isHit = true;
            float passengersDied = (GameManager.Instance.Passenger -  GameManager.Instance.Passenger/busProps.passengerLostRateByHit);
            if(passengersDied < 1){
                passengersDied = 1;
            }
            GameManager.Instance.UpdatePassengerCount(-(int)passengersDied);
            Debug.Log(GameManager.Instance.Passenger);
        }
    }
}
