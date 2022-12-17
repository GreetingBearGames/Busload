using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMultiplier : MonoBehaviour
{
    [SerializeField] private GameObject _moneyToUIPrefab;
    [SerializeField] private int _showDuration;
    private Vector3 _creationPos;

    void Start()
    {
        _creationPos = GameObject.FindWithTag("Bus").gameObject.transform.position;
    }

    void Update()
    {

    }

    public void CreateMoney(float count)
    {
        SoundManager.instance.Play("Game Win Money Collect");
        StartCoroutine(Wait(count));
    }

    IEnumerator Wait(float count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(_moneyToUIPrefab, _creationPos, Quaternion.Euler(40f, 0f, 0f));
            SoundManager.instance.Play("MoneyCollect");
            yield return new WaitForSeconds(_showDuration / count);
        }
    }
}
