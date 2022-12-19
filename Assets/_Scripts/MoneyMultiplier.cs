using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyMultiplier : MonoBehaviour
{
    [SerializeField] private GameObject _moneyToUIPrefab;
    [SerializeField] private TextMeshProUGUI _passengerCountUI;
    public int _showDuration;
    private Vector3 _creationPos;

    void Start()
    {

    }

    void Update()
    {

    }

    public void CreateMoney(float count)
    {
        StartCoroutine(Wait(count));
        PassengerCountToZero();
    }

    IEnumerator Wait(float count)
    {
        _creationPos = GameObject.FindWithTag("Bus").gameObject.transform.position;
        for (int i = 0; i < count; i++)
        {
            Instantiate(_moneyToUIPrefab, _creationPos, Quaternion.Euler(40f, 0f, 0f));
            yield return new WaitForSeconds(_showDuration / count);
        }
    }

    public void PassengerCountToZero()
    {
        StartCoroutine("PassengerCountToZeroTimer");
    }

    IEnumerator PassengerCountToZeroTimer()
    {
        //finish hediye para prefabı doğduğu an yolcu sayısını düşür

        var totalPassenger = GameManager.Instance.Passenger;
        for (int i = 0; i <= totalPassenger; i++)
        {
            GameManager.Instance.UpdatePassengerCount(-1);
            _passengerCountUI.text = ((int)GameManager.Instance.Passenger).ToString();
            yield return new WaitForSeconds(_showDuration / totalPassenger);
        }
    }
}
