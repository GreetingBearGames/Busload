using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMultiplier : MonoBehaviour
{
    [SerializeField] private GameObject _moneyToUIPrefab;
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
}
