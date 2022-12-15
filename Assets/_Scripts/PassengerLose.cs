using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerLose : MonoBehaviour {
    [SerializeField] private GameObject passenger;
    public bool isThrow = false;
    public int passangerToThrow = 0;
    private float _busBoundsZ, _powerOfThrow = 10.0f, _radius = 5.0f, _upforce=1.0f;

    private void Start() {
        _busBoundsZ = GetComponent<Renderer>().bounds.size.z;
    }

    private void InstantiateAndThrowPassengers(int passengerToLose) {
        for (int i = 0; i < passengerToLose; i++) {
            Instantiate(passenger, new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-_busBoundsZ/2, _busBoundsZ/2)), Quaternion.identity);
        }
    }
    private void FixedUpdate() {
        if(isThrow){
            InstantiateAndThrowPassengers(passangerToThrow);
            Detonate();
            isThrow = false;
        }
    }

    private void Detonate(){
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach(Collider hit in colliders){
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null){
                rb.AddExplosionForce(_powerOfThrow, explosionPos, _radius, _upforce, ForceMode.Impulse);
            }
            
        }
    }
}
