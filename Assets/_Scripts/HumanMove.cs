using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HumanMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private AllPassengerDie _allPassengerDieScript;
    private TextMeshProUGUI _passengerCount = null;
    private GameObject _target;
    private Vector3 _targetPos;
    [HideInInspector] public bool isMoving = false;

    void Start()
    {
        _target = GameObject.FindWithTag("Bus").gameObject;
        _passengerCount = GameObject.FindWithTag("PassengerCount").gameObject.GetComponent<TextMeshProUGUI>();
        _allPassengerDieScript = this.transform.parent.GetComponentInChildren<AllPassengerDie>();
    }


    void Update()
    {
        if (isMoving)
        {
            MoveHumantoBus();
        }
        _passengerCount.text = ((int)GameManager.Instance.Passenger).ToString();
    }


    public void MoveHumantoBus()
    {
        _targetPos = new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, _moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BusCrashArea")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Bus")
        {
            if (!_allPassengerDieScript.isBusFullyCrashed)
            {
                GameManager.Instance.UpdatePassengerCount(GameManager.Instance.PassengerIncreaseRate);
                _passengerCount.text = ((int)GameManager.Instance.Passenger).ToString();
            }
            Destroy(this.gameObject);
        }
    }
}
