using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerLose : MonoBehaviour {
    [SerializeField] private GameObject passenger;
    private List<GameObject> _passengers;
    public bool isThrow = false;
    public int passangerToThrow = 0;
    private float _busBoundsZ, _powerOfThrow = 30.0f, _radius = 5.0f, _upforce = 5.0f;

    private void Start() {
        _busBoundsZ = GetComponent<BoxCollider>().bounds.size.z;
    }

    private void InstantiateAndThrowPassengers(int passengerToLose) {
        for (int i = 0; i < passengerToLose; i++) {
            Instantiate(passenger, new Vector3(transform.position.x, transform.position.y, transform.position.z + _busBoundsZ / 2), Quaternion.identity);
        }
    }
    private void FixedUpdate() {
        if (isThrow) {
            isThrow = false;
            SoundManager.instance.Play("Drop Passenger");
            InstantiateAndThrowPassengers(passangerToThrow);
            Detonate();
        }
    }

    private void Detonate() {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(_powerOfThrow, explosionPos, _radius, _upforce, ForceMode.Impulse);
            }
        }
    }
}
