using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HumanMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private TextMeshProUGUI _passengerCount;
    private GameObject _target;
    private Vector3 _targetPos;
    [HideInInspector] public bool isMoving = false;

    void Start()
    {
        _target = GameObject.FindWithTag("Bus").gameObject;
        _passengerCount = GameObject.FindWithTag("PassengerCount").gameObject.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (isMoving)
        {
            MoveHumantoBus();
        }
    }


    public void MoveHumantoBus()
    {
        _targetPos = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bus")
        {
            _passengerCount.text = (int.Parse(_passengerCount.text) + 1).ToString();
            Destroy(this.gameObject);
        }
    }
}
