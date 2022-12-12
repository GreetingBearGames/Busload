using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool _isHit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bus" && !_isHit)
        {
            _isHit = true;

            GameManager.Instance.UpdatePassengerCount(-GameManager.Instance.PassengerIncreaseRate * 2);
        }
    }
}
