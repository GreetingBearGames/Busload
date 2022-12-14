using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishMoneyToUI : MonoBehaviour
{
    private GameObject _moneyUI;
    [SerializeField] private float _speed;
    private Vector3 _screenPoint, _targetPos;


    void Start()
    {
        var startPos = transform.position;
        transform.position = new Vector3(startPos.x + Random.Range(-1f, 1f), startPos.y + Random.Range(-1f, 1f), startPos.z + Random.Range(-1f, 1f));

        _moneyUI = GameObject.FindWithTag("MoneyCount").gameObject;
        Debug.Log(_moneyUI);
        _screenPoint = _moneyUI.transform.position + new Vector3(-200, 0, 5);  //the "+ new Vector3(0,0,5)" ensures that the object is so close to the camera you dont see it
        _targetPos = Camera.main.ScreenToWorldPoint(_screenPoint);
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);

        if ((transform.position - _targetPos).magnitude < 0.25f)
        {
            Destroy(this.GetComponent<SpriteRenderer>());
            var explosionParticle = this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            explosionParticle.gameObject.SetActive(true);
            if (!explosionParticle.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
