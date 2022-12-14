using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerLose : MonoBehaviour {
    [SerializeField] private GameObject passenger;
    private float _busBoundsY, _powerOfThrow = 10.0f, _radius;

    private void Start() {
        _busBoundsY = GetComponent<Renderer>().bounds.size.y;
    }

    private void InstantiateAndThrowPassengers(int passengerToLose) {
        for (int i = 0; i < passengerToLose; i++) {
            Instantiate(passenger, new Vector3(transform.position.x, Random.Range(-_busBoundsY, _busBoundsY), transform.position.z), Quaternion.identity);

        }
    }
}
